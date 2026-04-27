# Wafer Transfer System

> 반도체 장비 제어 구조를 구현한 HMI + 자동운전 + 통신 시뮬레이터 (C# WinForms)


시연 영상 : [https://youtu.be/7FDzhD7U8io](https://youtu.be/POu01F-f4Z8)

---

## 📺 동작 영상

<img width="600" height="500" alt="시퀀스동작" src="https://github.com/user-attachments/assets/9a671a23-967c-40d4-817b-0c59ef2c6092" />

---

## 📌 프로젝트 소개

실제 장비 없이 반도체 장비의 기본 제어 구조를 소프트웨어로 구현한 포트폴리오 프로젝트입니다.  
HMI 화면, 상태머신 기반 자동운전 시퀀스, 알람 처리, 로그 저장, TCP 통신까지 장비 제어 SW의 핵심 기능을 포함합니다.

---

## 🛠 기술 스택

| 항목 | 내용 |
|---|---|
| Language | C# |
| Framework | .NET 6.0 WinForms |
| 통신 | TCP Socket |
| 설정 | XML |
| 로그 | CSV |
| IDE | Visual Studio 2022 |

---

## 🖥 HMI 화면


| HNI 메인 화면 | 알람발생 |
|---|---|
| <img width="600" height="500" alt="시퀀스 메인" src="https://github.com/user-attachments/assets/3bf07ca6-0a83-4338-82c1-b9b99088ba4a" /> | <img width="600" height="500" alt="알람발생" src="https://github.com/user-attachments/assets/a4348b57-6db3-4045-b23a-44e7e76f2105" /> |




---

## ✅ 핵심 기능

- **장비 상태 표시** — IDLE / RUN / ALARM / COMPLETE
- **자동운전 시퀀스** — 8스텝 상태머신 기반 자동 제어
- **Timeout 알람 처리** — 스텝별 제한 시간 초과 시 ALARM 전환
- **알람 코드 관리** — E001 ~ E005 알람 코드 체계
- **로그 저장** — 운전 로그 / 알람 로그 CSV 저장
- **TCP Socket 통신** — 장비 상태 실시간 외부 전송
- **XML 설정 파일** — IP, Port, Timeout, Recipe 설정 분리

---

## 🔄 자동운전 시퀀스

```
Door Check → Wafer Detect → Door Lock → Vacuum ON
→ Motor Forward → Process Run → Return Home → Complete
```

각 스텝마다 Timeout을 적용하여 이상 상황 발생 시 자동으로 ALARM 상태로 전환됩니다.

---

## 🚨 알람 코드

| 코드 | 내용 |
|---|---|
| E001 | Door Check Timeout |
| E002 | Wafer Detect Timeout |
| E003 | Door Lock Timeout |
| E004 | Vacuum Timeout |
| E005 | Motor Forward Timeout |

---

## ⚙ 설정 파일 (config.xml)

```xml
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
</Config>
```

코드 수정 없이 XML 파일만 변경하여 현장 대응이 가능하도록 설계했습니다.

| 수정 전 | 수정 후 |
|---|---|
| <img width="600" height="500" alt="xml timeout 수정전" src="https://github.com/user-attachments/assets/0213986e-b1ff-4239-8140-50f90048457b" /> | <img width="600" height="500" alt="xml 수정 timeout 수정후" src="https://github.com/user-attachments/assets/fcf0a981-d598-476f-a33d-578e8c3b5891" /> |
---

## 📁 로그 파일

| 파일 | 내용 |
|---|---|
| run_log.csv | 운전 시작 / 완료 이력 |
| alarm_log.csv | 알람 발생 이력 |





| run csv | 파일생성확인 | alram csv |
|---|---|---|
| <img width="600" height="500" alt="run csv 확인" src="https://github.com/user-attachments/assets/1a07dee4-9be6-4484-9d5a-537b278b957f" /> |<img width="600" height="500" alt="xml,csv 파일생성확인" src="https://github.com/user-attachments/assets/740b054c-0827-4f53-9c16-7a9410b6564d" /> | <img width="600" height="500" alt="error alram csv 확인" src="https://github.com/user-attachments/assets/aeb11a64-0fa1-4035-8a97-9c2a0986ec9f" />|





---

## 🗓 개발 기간

1일 (1인 개발)

| 시간 | 내용 |
|---|---|
| 1일차 | UI / HMI 제작 |
| 1일차 | 시퀀스 제어 |
| 2일차 | 알람 / 로그 |
| 2일차 | TCP 통신 / XML 설정 / 마무리 |
