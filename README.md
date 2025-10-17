# 📚 Library_Login_Record：圖書館電腦使用登記系統

## 📋 目錄

1.  [📄 專案摘要 (Project Abstract)](#-專案摘要-project-abstract)
2.  [💡 核心功能 (Key Features)](#-核心功能-key-features)
3.  [🛠 安裝與設定 (Installation Guide)](#-安裝與設定-installation-guide)
    * [必要條件 (Prerequisites)](#1-必要條件-prerequisites)
    * [下載與專案配置 (Setup)](#2-下載與專案配置-setup)
4.  [🚀 使用方式 (Usage)](#-使用方式-usage)
5.  [🔗 聯絡與授權 (Contact & License)](#-聯絡與授權-contact--license)

---

## 📄 專案摘要 (Project Abstract)

**Library_Login_Record** 是一個基於 **Windows Forms (.NET Framework 4.8)** 平台所開發的桌面應用程式，旨在提供一個高效、自動化的解決方案，用於追蹤圖書館或公共電腦的使用記錄。

本系統最初是在**亞東紀念醫院圖書館**實際部署上線執行，用於追蹤醫院員工使用圖書館電腦的記錄。經主管同意，本專案已開源，旨在為有類似需求的學術或企業單位提供一個可靠的即時資料管理參考架構。

核心技術採用 C# 搭配 **Google Sheets API v4**，成功地將本地端的 CSV 檔案備份與雲端試算表的即時集中管理結合，確保資料的可靠性與可分析性。

---

## 💡 核心功能 (Key Features)

| 功能項目 | 描述 |
| :--- | :--- |
| **員工 ID 登記** | 透過簡潔的 GUI 介面，快速記錄使用者的員工編號。 |
| **電腦編號追蹤** | 自動記錄電腦名稱，並支援手動輸入電腦編號。 |
| **Google Sheets 整合** | 透過 API 將登入記錄**即時同步**上傳至指定的雲端試算表，實現資料集中管理。 |
| **本地 CSV 備份** | 所有登入記錄會即時儲存於本地端的 CSV 檔案中 ，作為資料備份和離線記錄。 |
| **單機部署** | 應用程式為 WinForms 獨立執行檔，可單獨部署於各台 Windows 電腦。 |

---

## 🛠 安裝與設定 (Installation Guide)

### 1. 必要條件 (Prerequisites)

* **運行環境：** 需安裝 **.NET Framework 4.8**。
* **Google Sheets API 憑證：** 必須在 Google Cloud Platform (GCP) 上執行以下步驟：
    1.  建立一個 GCP 專案並啟用 **Google Sheets API**。
    2.  建立並下載 **服務帳戶 (Service Account)** 的 JSON 憑證金鑰。

### 2. 下載與專案配置 (Setup)

1.  **克隆專案：**
    ```bash
    git clone [https://github.com/CadyLinn/Library_Login_Record.git](https://github.com/CadyLinn/Library_Login_Record.git)
    ```
2.  **還原 NuGet 套件：**
    * 在 Visual Studio (建議 2022) 中開啟 `Library_Login_Record.sln` 檔案，確保所有 NuGet 套件被自動或手動還原。
    
3.  **配置 `App.config`：**
    * 打開 `App.config` 檔案，在 `<appSettings>` 區塊中填寫您的設定：
        ```xml
        <appSettings>
          <add key="GoogleSpreadsheetId" value="[請替換為您的 Google Sheet ID]" /> 
          <add key="GoogleCredentialPath" value="credentials.json" /> 
        </appSettings>
        ```
    
4.  **放置憑證檔案：**
    * 將您下載的服務帳戶 JSON 憑證檔案**重新命名**為 `credentials.json`。
    * 將該檔案放置於專案的 **執行目錄** (`bin/Debug` 或 `bin/Release`)。
    * **重要：** 記得將服務帳戶的 Email 地址**共享**給您的目標 Google 試算表，並賦予其 **編輯權限**。

---

## 🚀 使用方式 (Usage)

1.  **啟動應用程式：** 執行 `Library_Login_Record.exe` 檔案。
2.  **輸入資料：**
    * 輸入員工的 5 碼員工編號。
    * 輸入電腦編號 (最多 2 碼數字)。
3.  **送出：** 點擊 **「送出」** 按鈕或按下 **Enter** 鍵。
4.  **資料同步：** 應用程式將會：
    * 寫入一筆新的記錄到本地的 CSV 檔案中。
    * 異步呼叫 Google Sheets API，將相同的記錄寫入指定的雲端試算表的 `libRecord` 工作表。
5.  **結束：** 成功寫入後，程式會自動退出。

---

## 🔗 聯絡與授權 (Contact & License)

| 項目 | 資訊 |
| :--- | :--- |
| **作者 (Author)** | **Cady Lin 林郁庭** (最初部署於亞東紀念醫院圖書館) |
| **著作權 (Copyright)** | **© 2025 CadyLin 林郁庭** |
| **聯絡方式 (Contact)** | yytttt420@gmail.com |
| **專案授權 (License)** | **MIT License** |

**詳細的授權條款請參閱專案根目錄下的 `LICENSE` 檔案。**
