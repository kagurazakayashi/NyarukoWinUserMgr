using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UserInfo;

namespace WinUserMgr
{
    internal class ChangeUser
    {
        // 每次用 ShowChangeList 前必須先設定以下兩個屬性
        public int defaultDataGridUsersColumnsCount = 0;
        public string doDomain = ""; // 如果設定值，將真正執行修改操作，否則只是顯示修改列表
        //

        public bool hasChPwd = false; // 這是 ShowChangeList 的返回值，用於讀取

        public List<DataGridChange> changes = new List<DataGridChange>();
        private Color[] addDelColor;
        public ConfirmWindow parentWindow = null;
        static string[] okInfo = { " 成功。", " 失败: " };

        public ChangeUser(Color[] addDelColor)
        {
            this.addDelColor = addDelColor;
        }

        /// <summary>
        /// 獲取 DataGridView 的資料更改列表。
        /// 該方法會檢查所有單元格的值，並與原始資料進行比較，記錄發生更改的單元格資訊。
        /// 同時還會檢查標記為刪除的行，並將其記錄為變更項。
        /// </summary>
        /// <param name="dataGridUsers">DataGridView 控制元件，包含使用者資料。</param>
        /// <param name="originalData">字典，儲存原始資料，鍵為單元格的行列索引 (row, col)，值為單元格的原始值。</param>
        public void GetChangeList(DataGridView dataGridUsers, Dictionary<(int row, int col), object> originalData)
        {
            // 清空之前記錄的更改列表。
            changes.RemoveAll(x => true);

            // 遍歷 DataGridView 中的每一行。
            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                // 遍歷當前行中的每一個單元格。
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // 跳過無效的單元格（索引超出範圍的情況）。
                    if (cell.RowIndex < 0 || cell.ColumnIndex < 0 ||
                        cell.RowIndex >= dataGridUsers.Rows.Count ||
                        cell.ColumnIndex >= dataGridUsers.Columns.Count)
                    {
                        continue;
                    }

                    // 判斷該單元格是否為新行資料。
                    bool isNewRow = !originalData.ContainsKey((cell.RowIndex, cell.ColumnIndex));
                    if (isNewRow)
                    {
                        // 如果是新行，將其當前值儲存為原始資料。
                        originalData[(cell.RowIndex, cell.ColumnIndex)] = cell.Value;
                    }

                    // 獲取單元格的原始值和當前值。
                    var originalValue = originalData[(cell.RowIndex, cell.ColumnIndex)];
                    var newValue = cell.Value;

                    // 判斷兩個值是否都為 null 或為空字串。
                    bool isBothNullOrEmpty = (originalValue == null || string.IsNullOrEmpty(originalValue.ToString())) &&
                                             (newValue == null || string.IsNullOrEmpty(newValue.ToString()));

                    // 如果原始值和當前值不相等且不同時為 null 或空，則記錄為變更。
                    if (!isBothNullOrEmpty && !Equals(originalValue, newValue))
                    {
                        changes.Add(new DataGridChange
                        {
                            OriginalValue = originalValue ?? "",
                            NewValue = newValue,
                            RowIndex = cell.RowIndex,
                            ColumnIndex = cell.ColumnIndex,
                            RowFirstColumnValue = (string)row.Cells[0].Value, // 當前行第一列的值。
                            Title = dataGridUsers.Columns[cell.ColumnIndex].HeaderText, // 當前列的標題。
                            isNewRow = isNewRow // 是否為新行。
                        });
                    }
                }
            }

            // 獲取被標記為刪除的行的第一列值。
            List<string> delRows = GetMarkedDelRowsFirstColumn(dataGridUsers.Rows);
            foreach (string delRow in delRows)
            {
                // 將標記為刪除的行記錄為變更。
                changes.Add(new DataGridChange
                {
                    OriginalValue = "", // 被刪除行的原始值為空。
                    NewValue = "", // 被刪除行的當前值為空。
                    RowIndex = -1, // 特殊標記，表示整行刪除。
                    ColumnIndex = -1, // 特殊標記，表示整行刪除。
                    RowFirstColumnValue = delRow, // 被刪除行的第一列值。
                    Title = "", // 列標題為空。
                    isNewRow = false // 標記為非新行。
                });
            }
        }

        /// <summary>
        /// 根據使用者的更改記錄生成更改列表，並對使用者資訊進行相應的修改。
        /// </summary>
        /// <returns>返回包含更改描述的字串陣列。</returns>
        public string[] ShowChangeList()
        {
            // 初始化使用者資訊修改器，如果域名不為空，則建立例項
            UserInfoModifier chUsr = doDomain.Length > 0 ? new UserInfoModifier(doDomain) : null;
            hasChPwd = false; // 標識是否更改了密碼
            int ii = 0; // 更改記錄計數器
            List<string> markedRows = new List<string>(); // 儲存生成的更改描述
            int changeLen = changes.Count; // 更改記錄的數量

            // 遍歷每一條更改記錄
            foreach (var change in changes)
            {
                // 將原始值和新值轉換為可顯示的字串
                string originalValue = viewTaskStrConv(change.OriginalValue.ToString());
                string newValue = viewTaskStrConv(change.NewValue.ToString());
                string info = ""; // 單條更改描述
                string name = change.RowFirstColumnValue; // 當前更改涉及的使用者名稱

                // 跳過無效的使用者名稱
                if (string.IsNullOrEmpty(name)) continue;

                ii++; // 更新更改計數器
                string err = ""; // 儲存錯誤資訊
                bool isError = false; // 標識是否發生錯誤

                // 根據列索引處理不同型別的更改
                switch (change.ColumnIndex)
                {
                    case -1 when change.RowIndex == -1:
                        // 刪除使用者
                        info = $"{ii}. 删除用户 \"{name}\"";
                        err = chUsr?.DeleteUser(name) ?? "";
                        break;

                    case 0:
                        // 建立新使用者
                        info = $"{ii}. 创建新用户 \"{name}\"";
                        err = chUsr?.CreateUser(name, "") ?? "";
                        break;

                    case 1:
                        // 更改使用者全名
                        info = $"{ii}. 将用户 \"{name}\" 的全名从 \"{originalValue}\" 改为 \"{newValue}\"";
                        err = chUsr?.SetFullName(name, newValue) ?? "";
                        break;

                    case 2:
                        // 更改使用者描述
                        info = $"{ii}. 将用户 \"{name}\" 的描述从 \"{originalValue}\" 改为 \"{newValue}\"";
                        err = chUsr?.SetDescription(name, newValue) ?? "";
                        break;

                    case 4:
                        // 重置使用者密碼
                        info = $"{ii}. 将用户 \"{name}\" 的密码重置为 \"{newValue}\"";
                        hasChPwd = true;
                        err = chUsr?.SetUserPassword(name, newValue) ?? "";
                        break;

                    case 5:
                        // 停用或啟用使用者
                        bool disable = change.NewValue.ToString() == "True";
                        info = $"{ii}. {(disable ? "禁用" : "启用")}用户 \"{name}\"";
                        err = chUsr?.SetAccountDisabled(name, disable) ?? "";
                        break;

                    case 6:
                        // 設定密碼是否永不過期
                        bool neverExpires = change.NewValue.ToString() == "True";
                        info = $"{ii}. 将用户 \"{name}\" 的密码设置为{(neverExpires ? "永不过期" : "需要定期更改")}";
                        err = chUsr?.SetPasswordNeverExpires(name, neverExpires) ?? "";
                        break;

                    case 7:
                        // 設定密碼是否允許使用者更改
                        bool cannotChange = change.NewValue.ToString() == "True";
                        info = $"{ii}. 将用户 \"{name}\" 的密码设置为{(cannotChange ? "不允许用户更改" : "可以由用户更改")}";
                        err = chUsr?.SetPasswordCannotBeChanged(name, cannotChange) ?? "";
                        break;

                    default:
                        // 處理使用者組相關的更改
                        if (change.ColumnIndex >= defaultDataGridUsersColumnsCount)
                        {
                            bool addToGroup = change.NewValue.ToString() == "True";
                            info = $"{ii}. 将用户 \"{name}\" {(addToGroup ? "添加到" : "从")}用户组 \"{change.Title}\" {(addToGroup ? "" : "中移除")}";
                            err = addToGroup ? chUsr?.AddToGroup(name, change.Title) ?? "" : chUsr?.RemoveFromGroup(name, change.Title) ?? "";
                        }
                        else
                        {
                            ii--;
                            Console.WriteLine($"禁止将用户 \"{name}\" 的 \"{change.Title}\" 从 \"{originalValue}\" 改为 \"{newValue}\"");
                            continue;
                        }
                        break;
                }

                // 處理錯誤資訊
                if (chUsr != null)
                {
                    if (!string.IsNullOrEmpty(err))
                    {
                        info += okInfo[1] + err;
                        isError = true;
                    }
                    else if (!isError)
                    {
                        info += okInfo[0];
                    }
                }

                // 新增到結果列表（去重）
                if (!markedRows.Contains(info))
                {
                    markedRows.Add(info);
                    if (parentWindow != null)
                    {
                        parentWindow.Invoke((Action)(() =>
                        {
                            parentWindow.listBoxTasks.Items.Add(info);
                            parentWindow.toolStripProgressBar1.Maximum = changeLen;
                            parentWindow.toolStripProgressBar1.Value = ii;
                        }));
                    }
                }
            }

            // 返回所有的更改描述
            return markedRows.ToArray();
        }

        /// <summary>
        /// 根據輸入字串的內容返回對應的中文描述。
        /// 如果輸入為 null 或為空字串，返回 "（空）"。
        /// 如果輸入為 "True"，返回 "是"。
        /// 如果輸入為 "False"，返回 "否"。
        /// 否則返回原始字串。
        /// </summary>
        /// <param name="info">輸入的字串</param>
        /// <returns>對應的中文描述或原始字串</returns>
        private string viewTaskStrConv(string info)
        {
            // 判斷輸入是否為 null 或為空字串
            if (info == null || info.Length == 0)
            {
                return "（空）"; // 返回 "（空）" 表示輸入為空
            }
            // 判斷輸入是否為 "True"
            else if (info == "True")
            {
                return "是"; // 返回 "是" 表示布林值 True
            }
            // 判斷輸入是否為 "False"
            else if (info == "False")
            {
                return "否"; // 返回 "否" 表示布林值 False
            }
            // 輸入不滿足上述條件，返回原始字串
            return info;
        }

        /// <summary>
        /// 獲取已標記為刪除狀態的行的第一列資料。
        /// </summary>
        /// <param name="dataGridUsersRows">DataGridView 的行集合。</param>
        /// <returns>返回一個字串列表，包含已標記為刪除狀態的行的第一列資料。</returns>
        private List<string> GetMarkedDelRowsFirstColumn(DataGridViewRowCollection dataGridUsersRows)
        {
            // 建立一個字串列表用於儲存標記為刪除狀態的行的第一列資料
            List<string> markedRows = new List<string>();

            // 遍歷 DataGridView 的所有行
            foreach (DataGridViewRow row in dataGridUsersRows)
            {
                // 檢查當前行的背景色是否為指定的標記刪除顏色
                if (row.DefaultCellStyle.BackColor == addDelColor[1])
                {
                    // 檢查該行的第一列單元格是否有值
                    if (row.Cells[0].Value != null)
                    {
                        // 將第一列單元格的值新增到列表中
                        markedRows.Add(row.Cells[0].Value.ToString());
                    }
                }
            }

            // 返回標記行的第一列資料列表
            return markedRows;
        }
    }
}
