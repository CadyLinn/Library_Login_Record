# 📚 Library_Login_Record (圖書館使用登記系統)

---

## 內容目錄 (Table of Contents)

### 中文
* [專案簡介](#專案簡介-project-overview)
* [核心功能](#核心功能-key-features)
* [專案架構與技術](#專案架構與技術-architecture--technology)
* [部署與安裝](#部署與安裝-deployment-and-installation)
    * [必要條件](#必要條件-prerequisites)
    * [設定步驟](#設定步驟-setup-steps)

### English
* [Project Overview](#project-overview)
* [Key Features](#key-features)
* [Architecture & Technology](#architecture--technology)
* [Deployment and Installation](#deployment-and-installation)
    * [Prerequisites](#prerequisites)
    * [Setup Steps](#setup-steps)

---

## 專案簡介 (Project Overview)

`Library_Login_Record` 是一個基於 **Windows Forms (.NET Framework)** 開發的應用程式，用於管理圖書館電腦的使用登記。

本系統是在**亞東紀念醫院教學部圖書館**實際部署上線執行的登記系統。經主管同意，本專案現已開源，供其他有類似需求的單位參考或使用。

此應用程式旨在提供一個簡單、高效的方式，追蹤醫院員工（或其他使用者）使用圖書館電腦的記錄，並實現資料的自動化集中管理。

---

## 核心功能 (Key Features)

* **員工 ID 登記：** 快速記錄使用者的員工編號。
* **電腦編號追蹤：** 自動或手動記錄電腦的具體編號。
* **本地 CSV 備份：** 所有的登入登出記錄會即時儲存於本地端的 CSV 檔案中，作為資料備份。
* **Google Sheets 整合：** 透過 Google Sheets API，將登入記錄同步上傳至指定的雲端試算表，實現資料的即時集中管理與分析。
* **單機部署：** 作為 Windows Forms 應用程式，可單獨部署於各圖書館電腦上。

---

## 專案架構與技術 (Architecture & Technology)

| 項目 (Item) | 描述 (Description) |
| :--- | :--- |
| **類型** | Windows Forms 應用程式 (WinForms) |
| **框架** | .NET Framework 4.7.2 (或更高版本) |
| **主要語言** | C\# |
| **資料庫/儲存** | 本地端 CSV 檔案 / Google Sheets API |
| **NuGet 依賴** | Google.Apis.Sheets.v4, Newtonsoft.Json 等 |

---

## 部署與安裝 (Deployment and Installation)

### 必要條件 (Prerequisites)

1.  **Visual Studio 2022** (建議使用)
2.  **.NET Framework 4.7.2 SDK** 或更高版本。
3.  **Google Sheets API 金鑰：** 必須在 Google Cloud Platform 上啟用 Sheets API 並下載相應的服務帳戶金鑰 (`client_secret.json` 或其他憑證檔案)。

### 設定步驟 (Setup Steps)

1.  **克隆專案：**
    ```bash
    git clone [Your Repository URL]
    ```
2.  **還原 NuGet 套件：** 在 Visual Studio 中，開啟 `Library_Login_Record.sln` 檔案，確保所有 NuGet 套件被成功還原。
3.  **配置憑證：** 將 Google Sheets API 的憑證檔案放置於專案的指定位置，並修改程式碼中讀取憑證的路徑。
4.  **建置與執行：** 在 Visual Studio 中建置 (Build) 專案，然後執行 (`F5`)。
