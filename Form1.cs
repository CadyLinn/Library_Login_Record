using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Library_Login_Record
{
    public partial class Form1 : Form
    {
        Label lblTitle, lblEmployeeId, lblComputerId;
        TextBox txtEmployeeId, txtComputerId;
        Button btnSubmit;
        Panel pnlMain, headerPanel;

        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.FromArgb(45, 52, 67); // 深藍灰背景
            this.Load += Form1_Load;
        }

        private void EnterLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmit.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 字型設定 
            var titleFont = new Font("Microsoft JhengHei UI", 32, FontStyle.Bold);
            var labelFont = new Font("Microsoft JhengHei UI", 16, FontStyle.Regular);
            var inputFont = new Font("Microsoft JhengHei UI", 18);
            var buttonFont = new Font("Microsoft JhengHei UI", 18, FontStyle.Bold);

            // 主面板 - 白色卡片樣式
            pnlMain = new Panel
            {
                Width = 600,
                Height = 550,
                BackColor = System.Drawing.Color.White,
                Cursor = Cursors.Default
            };
            pnlMain.Paint += Panel1_Paint; // 圓角

            // 頂部裝飾面板
            headerPanel = new Panel
            {
                Width = pnlMain.Width,
                Height = 120,
                BackColor = System.Drawing.Color.FromArgb(52, 152, 219), // 藍色
                Top = 0,
                Left = 0
            };
            headerPanel.Paint += HeaderPanel_Paint; // 圓角

            // 標題
            lblTitle = new Label
            {
                Text = "📚 圖書館電腦使用登記",
                Font = titleFont,
                ForeColor = System.Drawing.Color.White,
                Width = pnlMain.Width,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 20,
                Left = 0,
                BackColor = System.Drawing.Color.Transparent
            };

            // 員工編號 Label
            lblEmployeeId = new Label
            {
                Text = "👤 員工編號",
                Font = labelFont,
                ForeColor = System.Drawing.Color.FromArgb(44, 62, 80),
                Width = 200,
                Height = 30,
                Top = 170,
                Left = 60,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.Transparent
            };

            // 員工編號 TextBox 
            txtEmployeeId = new RoundedTextBox
            {
                Font = inputFont,
                Multiline = false,
                Width = 480,
                Height = 50,
                Top = 200,
                Left = 60,
                MaxLength = 5,
                BorderStyle = BorderStyle.None,
                BackColor = System.Drawing.Color.FromArgb(230, 230, 230), // 灰色
                ForeColor = System.Drawing.Color.FromArgb(44, 62, 80)
            };
            txtEmployeeId.KeyPress += TextBox_KeyPress_AlphaNumericOnly;
            txtEmployeeId.KeyDown += EnterLogin_KeyDown;

            // 電腦編號 Label
            lblComputerId = new Label
            {
                Text = "💻 電腦編號",
                Font = labelFont,
                ForeColor = System.Drawing.Color.FromArgb(44, 62, 80),
                Width = 200,
                Height = 30,
                Top = 280,
                Left = 60,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.Transparent
            };

            // 電腦編號 TextBox
            txtComputerId = new RoundedTextBox
            {
                Font = inputFont,
                Multiline = false,
                Width = 480,
                Height = 50,
                Top = 310,
                Left = 60,
                MaxLength = 2,
                BorderStyle = BorderStyle.None,
                BackColor = System.Drawing.Color.FromArgb(230, 230, 230),
                ForeColor = System.Drawing.Color.FromArgb(44, 62, 80)
            };
            txtComputerId.KeyPress += TextBox_KeyPress_DigitOnly;
            txtComputerId.KeyDown += EnterLogin_KeyDown;

            // 確認按鈕 
            btnSubmit = new RoundedButton
            {
                Text = "送出",
                Font = buttonFont,
                Width = 200,
                Height = 55,
                Top = 420,
                Left = (pnlMain.Width - 200) / 2,
                BackColor = System.Drawing.Color.FromArgb(46, 204, 113),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += button1_Click;

            // 加入元件到頂部面板
            headerPanel.Controls.Add(lblTitle);

            // 加入元件到主面板
            pnlMain.Controls.Add(headerPanel);
            pnlMain.Controls.Add(lblEmployeeId);
            pnlMain.Controls.Add(txtEmployeeId);
            pnlMain.Controls.Add(lblComputerId);
            pnlMain.Controls.Add(txtComputerId);
            pnlMain.Controls.Add(btnSubmit);

            this.Controls.Add(pnlMain);

            // Panel 置中
            pnlMain.Left = (this.ClientSize.Width - pnlMain.Width) / 2;
            pnlMain.Top = (this.ClientSize.Height - pnlMain.Height) / 2;

            // 添加陰影效果
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 繪製陰影效果
            Rectangle shadowRect = new Rectangle(pnlMain.Left + 5, pnlMain.Top + 5, pnlMain.Width, pnlMain.Height);
            using (Brush shadowBrush = new SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0)))
            {
                e.Graphics.FillRoundedRectangle(shadowBrush, shadowRect, 15);
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            // 圓角主面板
            Rectangle rect = new Rectangle(0, 0, pnlMain.Width, pnlMain.Height);
            using (Brush brush = new SolidBrush(pnlMain.BackColor))
            {
                e.Graphics.FillRoundedRectangle(brush, rect, 15);
            }
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            // 圓角頂部面板（只有上方圓角）
            Rectangle rect = new Rectangle(0, 0, headerPanel.Width, headerPanel.Height + 15);
            using (Brush brush = new SolidBrush(headerPanel.BackColor))
            {
                e.Graphics.FillRoundedRectangle(brush, rect, 15);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string empId = txtEmployeeId.Text.Trim().ToUpper();
            string pcId = txtComputerId.Text.Trim();

            if (string.IsNullOrEmpty(empId) || string.IsNullOrEmpty(pcId))
            {
                ShowCustomMessageBox("請輸入員工編號與電腦編號！", "輸入錯誤", MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(empId, @"^[a-zA-Z0-9]{5}$"))
            {
                ShowCustomMessageBox("員工編號必須為 5 碼英數字！", "格式錯誤", MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(pcId, @"^\d{1,2}$"))
            {
                ShowCustomMessageBox("電腦編號最多只能輸入 2 碼數字！", "格式錯誤", MessageBoxIcon.Warning);
                return;
            }

            string folderPath = @"C:\lib";
            Directory.CreateDirectory(folderPath);
            string fileName = Path.Combine(folderPath, $"LoginLog_{DateTime.Now:yyyyMM}.csv");
            var utf8WithBom = new UTF8Encoding(true);

            try
            {
                if (!File.Exists(fileName))
                {
                    string header = "登入時間,員工編號,電腦編號,電腦名稱";
                    File.WriteAllText(fileName, header + Environment.NewLine, utf8WithBom);
                }

                string logEntry = $"'{DateTime.Now:yyyy-MM-dd HH:mm:ss},{empId},{pcId},{Environment.MachineName}";
                File.AppendAllText(fileName, logEntry + Environment.NewLine, utf8WithBom);

                GoogleSheetHelper.AppendRow(empId, pcId, Environment.MachineName);

                ShowCustomMessageBox("登入成功，資料已記錄與上傳！", "登入成功", MessageBoxIcon.Information);
                Application.Exit();
            }
            catch (IOException ioex)
            {
                ShowCustomMessageBox("檔案正在被其他程式使用，請關閉後再試！\n" + ioex.Message, "檔案錯誤", MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowCustomMessageBox("發生其他錯誤：\n" + ex.Message, "系統錯誤", MessageBoxIcon.Error);
            }
        }

        private void ShowCustomMessageBox(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void TextBox_KeyPress_AlphaNumericOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox_KeyPress_DigitOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4) || keyData == (Keys.Control | Keys.W))
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    // 圓角 TextBox 類別
    public class RoundedTextBox : TextBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // TextBox 的圓角效果通過 Region 實現
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetRoundedRegion();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetRoundedRegion();
        }

        private void SetRoundedRegion()
        {
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, 8))
            {
                this.Region = new Region(path);
            }
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    // 圓角 Button 類別
    public class RoundedButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, 10))
            {
                this.Region = new Region(path);
            }
            base.OnPaint(pevent);
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    // Graphics 擴展方法
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle rect, int radius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, radius))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
            }
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    public static class GoogleSheetHelper
    {
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "Login Logger";
        static string SpreadsheetId = "";
        static string SheetName = "libRecord";
        static string CredentialPath = @"C:\";

        private static SheetsService GetSheetsService()
        {
            try
            {
                if (!File.Exists(CredentialPath))
                {
                    MessageBox.Show("找不到憑證檔案：" + CredentialPath);
                    throw new FileNotFoundException("Google 憑證不存在", CredentialPath);
                }

                GoogleCredential credential;
                using (var stream = new FileStream(CredentialPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                }

                return new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化 SheetsService 發生錯誤：\n" + ex.Message + "\n\n" + ex.StackTrace);
                throw;
            }
        }

        public static void AppendRow(string empId, string pcId, string machineName)
        {
            var service = GetSheetsService();
            var range = $"{SheetName}!A:D";

            var objectList = new List<object>
            {
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                empId,
                pcId,
                machineName
            };

            var valueRange = new ValueRange
            {
                Values = new List<IList<object>> { objectList }
            };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.Execute();
        }
    }
}