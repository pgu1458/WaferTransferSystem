using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;


using System.Xml;


namespace WaferTransferSystem
{
    

    public enum EquipmentState
    {
        IDLE,
        RUN,
        ALARM,
        COMPLETE
    }

    public enum SequenceStep
    {
        NONE,
        DOOR_CHECK,
        WAFER_DETECT,
        DOOR_LOCK,
        VACUUM_ON,
        MOTOR_FORWARD,
        PROCESS_RUN,
        RETURN_HOME,
        COMPLETE
    }

    public partial class Form1 : Form
    {
        private string _logPath = "run_log.csv";
        private string _alarmLogPath = "alarm_log.csv";


        private EquipmentState _state = EquipmentState.IDLE;
        private SequenceStep _step = SequenceStep.NONE;
        private System.Windows.Forms.Timer _seqTimer;

        private int _stepTimeout = 0;      // 현재 스텝 경과 시간
        private int _timeoutSec = 2; // 5초 안에 안 되면 알람
        private AlarmCode _alarmCode = AlarmCode.NONE;

        private TcpListener _server;
        private Thread _serverThread;
        private bool _serverRunning = false;


        private string _configPath = "config.xml";
        private string _ip = "127.0.0.1";
        private int _port = 5000;
        private string _recipeName = "Recipe_A";
        private int _processTime = 3;


        public Form1()
        {
            InitializeComponent();
            InitConfig();
            InitTimer();
            InitLog();
            StartServer();
            UpdateUI();
        }

        private void InitTimer()
        {
            _seqTimer = new System.Windows.Forms.Timer();
            _seqTimer.Interval = 1000; // 1초마다
            _seqTimer.Tick += SeqTimer_Tick;
        }

        private void UpdateUI()
        {
            lblStatus.Text = $"장비 상태 : {_state}";
            lblStep.Text = $"현재 STEP : {_step}";

            // 상태별 색상
            lblStatus.ForeColor = _state switch
            {
                EquipmentState.IDLE => Color.Lime,
                EquipmentState.RUN => Color.Yellow,
                EquipmentState.ALARM => Color.Red,
                EquipmentState.COMPLETE => Color.Cyan,
                _ => Color.White
            };
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_state != EquipmentState.IDLE) return;

            _state = EquipmentState.RUN;
            _step = SequenceStep.DOOR_CHECK;
            _stepTimeout = 0;        // ← 추가
            _alarmCode = AlarmCode.NONE; // ← 추가
            _seqTimer.Start();
            AddAlarmLog("▶ 자동운전 시작");
            SaveRunLog("자동운전 시작");
            SendStatus();
            UpdateUI();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _seqTimer.Stop();
            _state = EquipmentState.IDLE;
            _step = SequenceStep.NONE;
            _stepTimeout = 0;
            _alarmCode = AlarmCode.NONE;
            ResetOutputs(); // ← 이거 추가
            AddAlarmLog("■ 정지");
            UpdateUI();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _seqTimer.Stop();
            _state = EquipmentState.IDLE;
            _step = SequenceStep.NONE;
            _stepTimeout = 0; // ← 이거 추가
            _alarmCode = AlarmCode.NONE; // ← 이것도 추가
            ResetOutputs();
            AddAlarmLog("↺ 리셋");
            UpdateUI();
        }

        private void AddAlarmLog(string msg)
        {
            Color color = Color.White;

            if (msg.Contains("ALARM") || msg.Contains("🚨"))
                color = Color.Red;
            else if (msg.Contains("완료") || msg.Contains("✔"))
                color = Color.Lime;
            else if (msg.Contains("📡") || msg.Contains("📤") || msg.Contains("📨"))
                color = Color.Cyan;

            string line = $"[{DateTime.Now:HH:mm:ss}] {msg}\n";

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = color;
            rtbLog.AppendText(line);
            rtbLog.ScrollToCaret();
        }

        private void ResetOutputs()
        {
            chkVacuum.Checked = false;
            chkMotor.Checked = false;
            chkLock.Checked = false;
            chkDoor.Checked = false;
            chkWafer.Checked = false;
            chkDoorLock.Checked = false;
        }

        public enum AlarmCode
        {
            NONE,
            E001_DOOR_CHECK_TIMEOUT,
            E002_WAFER_DETECT_TIMEOUT,
            E003_DOOR_LOCK_TIMEOUT,
            E004_VACUUM_TIMEOUT,
            E005_MOTOR_TIMEOUT
        }


        //csv 초기화함수
        private void InitLog()
        {
            if (!File.Exists(_logPath))
                File.WriteAllText(_logPath, "시간,상태,스텝,내용\n",
                    System.Text.Encoding.UTF8);  // ← 이걸로 변경

            if (!File.Exists(_alarmLogPath))
                File.WriteAllText(_alarmLogPath, "시간,알람코드,내용\n",
                    System.Text.Encoding.UTF8);  // ← 이걸로 변경
        }



        //로그저장
        private void SaveRunLog(string content)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{_state},{_step},{content}\n";
            File.AppendAllText(_logPath, line, System.Text.Encoding.UTF8);
        }

        private void SaveAlarmLog(string alarmCode, string content)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{alarmCode},{content}\n";
            File.AppendAllText(_alarmLogPath, line, System.Text.Encoding.UTF8);
        }



        private void SeqTimer_Tick(object sender, EventArgs e)
        {
            _stepTimeout++;

            // Timeout 체크
            if (_stepTimeout >= _timeoutSec && _step != SequenceStep.NONE)
            {
                TriggerAlarm(_step);
                return;
            }

            switch (_step)
            {
                case SequenceStep.DOOR_CHECK:
                    AddAlarmLog("STEP 1 : Door Check");
                    chkDoor.Checked = true;
                    NextStep(SequenceStep.WAFER_DETECT);
                    break;

                case SequenceStep.WAFER_DETECT:
                    AddAlarmLog("STEP 2 : Wafer Detect");
                    chkWafer.Checked = true;
                    NextStep(SequenceStep.DOOR_LOCK);
                    break;

                case SequenceStep.DOOR_LOCK:
                    AddAlarmLog("STEP 3 : Door Lock");
                    chkDoorLock.Checked = true;
                    chkLock.Checked = true;
                    NextStep(SequenceStep.VACUUM_ON);
                    break;

                case SequenceStep.VACUUM_ON:
                    AddAlarmLog("STEP 4 : Vacuum ON");
                    chkVacuum.Checked = true;
                    NextStep(SequenceStep.MOTOR_FORWARD);
                    break;

                case SequenceStep.MOTOR_FORWARD:
                    AddAlarmLog("STEP 5 : Motor Forward");
                    chkMotor.Checked = true;
                    NextStep(SequenceStep.PROCESS_RUN);
                    break;

                case SequenceStep.PROCESS_RUN:
                    AddAlarmLog("STEP 6 : Process Run...");
                    NextStep(SequenceStep.RETURN_HOME);
                    break;

                case SequenceStep.RETURN_HOME:
                    AddAlarmLog("STEP 7 : Return Home");
                    chkMotor.Checked = false;
                    chkVacuum.Checked = false;
                    NextStep(SequenceStep.COMPLETE);
                    break;

                case SequenceStep.COMPLETE:
                    AddAlarmLog("✔ 자동운전 완료!");
                    SaveRunLog("자동운전 완료");
                    SendStatus(); // ← 추가
                    _seqTimer.Stop();
                    _state = EquipmentState.COMPLETE;
                    _step = SequenceStep.NONE;
                    break;
            }

            UpdateUI();
        }

        private void NextStep(SequenceStep next)
        {
            _step = next;
            _stepTimeout = 0; // 스텝 넘어갈 때마다 타이머 리셋
        }


        private void TriggerAlarm(SequenceStep failedStep)
        {
            _seqTimer.Stop();
            _state = EquipmentState.ALARM;

            _alarmCode = failedStep switch
            {
                SequenceStep.DOOR_CHECK => AlarmCode.E001_DOOR_CHECK_TIMEOUT,
                SequenceStep.WAFER_DETECT => AlarmCode.E002_WAFER_DETECT_TIMEOUT,
                SequenceStep.DOOR_LOCK => AlarmCode.E003_DOOR_LOCK_TIMEOUT,
                SequenceStep.VACUUM_ON => AlarmCode.E004_VACUUM_TIMEOUT,
                SequenceStep.MOTOR_FORWARD => AlarmCode.E005_MOTOR_TIMEOUT,
                _ => AlarmCode.NONE
            };

            AddAlarmLog($"🚨 ALARM [{_alarmCode}]");
            AddAlarmLog("RESET 버튼으로 해제하세요");
            SaveAlarmLog(_alarmCode.ToString(), "Timeout 알람 발생"); // ← 추가
            UpdateUI();
        }

        //서버함수추가
        private void StartServer()
        {
            _serverRunning = true;
            _server = new TcpListener(IPAddress.Any, _port); // 5000 → _port
            _server.Start();
            AddAlarmLog("📡 TCP 서버 시작 (Port 5000)");

            _serverThread = new Thread(() =>
            {
                while (_serverRunning)
                {
                    try
                    {
                        TcpClient client = _server.AcceptTcpClient();
                        NetworkStream stream = client.GetStream();
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string received = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        this.Invoke((Action)(() =>
                        {
                            AddAlarmLog($"📨 수신: {received}");
                        }));

                        client.Close();
                    }
                    catch { break; }
                }
            });
            _serverThread.IsBackground = true;
            _serverThread.Start();
        }



        //클라이언트함수추가

        private void SendStatus()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 5000);
                NetworkStream stream = client.GetStream();
                string msg = $"STATE:{_state} STEP:{_step} TIME:{DateTime.Now:HH:mm:ss}";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
                stream.Write(data, 0, data.Length);
                client.Close();
                AddAlarmLog($"📤 전송: {msg}");
            }
            catch
            {
                AddAlarmLog("❌ 전송 실패");
            }
        }

        //form닫을때 종료

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _serverRunning = false;
            _server?.Stop();
            base.OnFormClosing(e);
        }


        private void InitConfig()
        {
            // 파일 없으면 기본값으로 생성
            if (!File.Exists(_configPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(@"<?xml version='1.0' encoding='utf-8'?>
                <Config>
                  <Network>
                    <IP>127.0.0.1</IP>
                    <Port>5000</Port>
                  </Network>
                  <Sequence>
                    <TimeoutSec>5</TimeoutSec>
                  </Sequence>
                  <Recipe>
                    <Name>Recipe_A</Name>
                    <ProcessTime>3</ProcessTime>
                  </Recipe>
                </Config>");
                doc.Save(_configPath);
                AddAlarmLog("⚙ config.xml 기본값으로 생성됨");
            }

            // XML 읽기
            LoadConfig();
        }

        private void LoadConfig()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_configPath);

            _ip = doc.SelectSingleNode("//Network/IP").InnerText;
            _port = int.Parse(doc.SelectSingleNode("//Network/Port").InnerText);
            _stepTimeout = 0;
            // TIMEOUT_SEC는 const라 XML값은 별도 변수로
            _timeoutSec = int.Parse(doc.SelectSingleNode("//Sequence/TimeoutSec").InnerText);
            _recipeName = doc.SelectSingleNode("//Recipe/Name").InnerText;
            _processTime = int.Parse(doc.SelectSingleNode("//Recipe/ProcessTime").InnerText);

            AddAlarmLog($"⚙ 설정 로드 완료 | {_recipeName} | Timeout {_timeoutSec}s");
        }


    }
}
