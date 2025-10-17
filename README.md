# 📚 Library_Login_Record：圖書館電腦使用登記系統

**專案定位：** 一套用於 Windows 桌面環境，結合本地備份與 Google Sheets 雲端集中管理的數據追蹤解決方案。

## 技術概覽 (Tech Stack Summary)

| 類型 | 技術 | 關鍵應用 |
| :--- | :--- | :--- |
| **前端/桌面應用** | **C# (Windows Forms)** | 基於穩定的 **.NET Framework 4.8**，提供輕量級、易部署的桌面 GUI 介面。 |
| **雲端服務/資料庫** | **Google Sheets API v4** | 實現實時數據同步與集中管理，無需維護後端資料庫。 |
| **本地資料庫/備份** | **本地 CSV 檔案** | 作為離線緩衝與資料備份機制，確保數據可靠性。 |
| **API/套件** | **Google.Apis.Sheets.v4** | .NET 生態系統中可靠的 Google 服務整合工具。 |

---

## 📋 目錄

1.  [📄 專案摘要與業務價值 (Project Abstract & Business Value)](#-專案摘要與業務價值-project-abstract--business-value)
2.  [💡 核心功能與演示 (Key Features & Demo)](#-核心功能與演示-key-features--demo)
3.  [🛠 安裝與配置指南 (Installation & Configuration Guide)](#-安裝與配置指南-installation--configuration-guide)
    * [必要條件 (Prerequisites)](#1-必要條件-prerequisites)
    * [專案配置 (Setup & Configuration)](#2-專案配置-setup--configuration)
4.  [🚀 操作流程 (Usage Workflow)](#-操作流程-usage-workflow)
5.  [🔗 聯絡與授權 (Contact & License)](#-聯絡與授權-contact--license)

---

## 📄 專案摘要與業務價值 (Project Abstract & Business Value)

**Library_Login_Record** 是一個專為 Windows 桌面環境設計的數據記錄應用程式。本專案的目標是提供一個**高效、零維護、易於部署**的解決方案，用於追蹤公共電腦（如圖書館、研討室）的使用情況。

### 業務/技術價值：

* **實務驗證：** 本系統已在**亞東紀念醫院圖書館**實際部署並穩定運行，證明其在企業級環境中的可靠性和實用性。
* **低成本數據管理：** 透過整合 Google Sheets API，完美取代傳統資料庫架構，實現**資料即時集中化**，大幅降低數據庫維護成本。
* **數據可靠性：** 採用**本地 CSV 備份 + 雲端即時同步**的雙重機制，確保在網路不穩定的情況下，數據依然能夠被完整記錄與追蹤。

---

## 💡 核心功能與演示 (Key Features & Demo)

| 功能項目 | 描述 (功能亮點) |
| :--- | :--- |
| **簡潔 ID 登記** | 提供直覺式 GUI 介面，優化使用者體驗，僅需輸入員工/使用者 ID 即可完成記錄。 |
| **自動化資產追蹤** | 自動採集電腦名稱，並提供電腦編號手動輸入選項，方便追蹤特定硬體使用頻率。 |
| **雲端即時同步** | 記錄透過 Google Sheets API **異步**上傳至指定試算表，實現集中、實時的數據分析基礎。 |
| **高容錯性備份** | 所有記錄同步儲存於本地 CSV 檔案，作為網路離線或 API 故障時的終極備份。 |
| **獨立部署能力** | 應用程式為 WinForms 單獨執行檔，支援在任何安裝 **.NET Framework 4.8** 的 Windows 電腦上獨立運行。 |

### 📈 操作演示

此 GIF 演示了從輸入使用者 ID 到數據同步完成的完整流程。

![Library Login Record 操作演示](assets/demo.gif)

---

## 🛠 安裝與配置指南 (Installation & Configuration Guide)

### 1. 必要條件 (Prerequisites)

* **作業系統：** Windows OS
* **運行環境：** 必須安裝 **.NET Framework 4.8**。
* **Google Cloud 配置：**
    1.  在 Google Cloud Platform (GCP) 建立專案並**啟用 Google Sheets API**。
    2.  建立並下載 **服務帳戶 (Service Account)** 的 JSON 憑證金鑰。

### 2. 專案配置 (Setup & Configuration)

1.  **克隆專案：**
    ```bash
    git clone [https://github.com/CadyLinn/Library_Login_Record.git]
    ```
2.  **依賴項安裝：**
    * 在 Visual Studio (建議 2022) 中開啟 `Library_Login_Record.sln`，確保所有 NuGet 套件（特別是 Google API 相關套件）已成功還原。
    
3.  **API 參數配置 (`App.config`)：**
    * 打開 `App.config` 檔案，將 `<appSettings>` 區塊中的佔位符替換為您的配置：
        ```xml
        <appSettings>
          <add key="GoogleSpreadsheetId" value="[請替換為您的目標 Google Sheet ID]" />  
          <add key="GoogleCredentialPath" value="credentials.json" />  
        </appSettings>
        ```
    
4.  **服務憑證部署 (關鍵步驟)：**
    * 將您下載的服務帳戶 JSON 憑證檔案**重新命名**為 `credentials.json`。
    * 將該檔案放置於專案的 **執行目錄** (`bin/Debug` 或 `bin/Release`)。
    * **授權：** **重要！** 請將服務帳戶的 Email 地址（格式通常為 `[service-account-name]@[project-id].iam.gserviceaccount.com`）**共享**給您的目標 Google 試算表，並賦予其 **編輯權限**。

---

## 🚀 操作流程 (Usage Workflow)

本應用程式設計為流程簡潔、單次執行。

1.  **啟動應用程式：** 執行位於部署目錄的 `Library_Login_Record.exe`。
2.  **數據輸入：**
    * 使用者輸入其 5 碼員工編號。
    * 輸入電腦編號 (如適用，最多 2 碼數字)。
3.  **數據傳輸：** 點擊 **「送出」** 或按下 **Enter** 鍵。
4.  **同步處理：** 系統將同步執行兩項任務：
    * 將帶有時間戳的記錄寫入本地的 CSV 備份檔案。
    * **異步**調用 Google Sheets API，將記錄寫入雲端試算表的 `libRecord` 工作表。
5.  **流程結束：** 成功寫入後，程式自動退出，等待下一次使用。

---

## 🔗 聯絡與授權 (Contact & License)

| 項目 | 資訊 |
| :--- | :--- |
| **作者 (Author)** | **Cady Lin 林郁庭** (最初部署於亞東紀念醫院圖書館) |
| **著作權 (Copyright)** | **© 2025 CadyLin 林郁庭** |
| **聯絡方式 (Contact)** | yytttt420@gmail.com |
| **專案授權 (License)** | **MIT License** (請遵守開源協議) |

**詳細的授權條款請參閱專案根目錄下的 `LICENSE` 檔案，歡迎 Fork 與貢獻。**
