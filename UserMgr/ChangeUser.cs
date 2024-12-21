using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUserMgr
{
    internal class ChangeUser
    {
        // 每次用 ShowChangeList 前必須先設定以下兩個屬性
        public int defaultDataGridUsersColumnsCount = 0;
        public bool doIt = false;
        //

        public bool hasChPwd = false; // 這是 ShowChangeList 的返回值，用於讀取

        public List<DataGridChange> changes = new List<DataGridChange>();
        private Color[] addDelColor;

        public ChangeUser(Color[] addDelColor)
        {
            this.addDelColor = addDelColor;
        }

        public void GetChangeList(DataGridView dataGridUsers, Dictionary<(int row, int col), object> originalData)
        {
            changes.RemoveAll(x => true);
            foreach (DataGridViewRow row in dataGridUsers.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.RowIndex < 0 || cell.ColumnIndex < 0 || cell.RowIndex >= dataGridUsers.Rows.Count || cell.ColumnIndex >= dataGridUsers.Columns.Count)
                    {
                        continue;
                    }
                    bool isNewRow = !originalData.ContainsKey((cell.RowIndex, cell.ColumnIndex));
                    if (isNewRow)
                    {
                        originalData[(cell.RowIndex, cell.ColumnIndex)] = cell.Value;
                    }
                    var originalValue = originalData[(cell.RowIndex, cell.ColumnIndex)];
                    var newValue = cell.Value;
                    bool isBothNullOrEmpty = (originalValue == null || string.IsNullOrEmpty(originalValue.ToString())) &&
                                     (newValue == null || string.IsNullOrEmpty(newValue.ToString()));
                    if (!isBothNullOrEmpty && !Equals(originalValue, newValue))
                    {
                        changes.Add(new DataGridChange
                        {
                            OriginalValue = originalValue ?? "",
                            NewValue = newValue,
                            RowIndex = cell.RowIndex,
                            ColumnIndex = cell.ColumnIndex,
                            RowFirstColumnValue = (string)row.Cells[0].Value,
                            Title = dataGridUsers.Columns[cell.ColumnIndex].HeaderText,
                            isNewRow = isNewRow
                        });
                    }
                }
            }
            List<string> delRows = GetMarkedDelRowsFirstColumn(dataGridUsers.Rows);
            foreach (string delRow in delRows)
            {
                changes.Add(new DataGridChange
                {
                    OriginalValue = "",
                    NewValue = "",
                    RowIndex = -1,
                    ColumnIndex = -1,
                    RowFirstColumnValue = delRow,
                    Title = "",
                    isNewRow = false
                });
            }
        }

        public string[] ShowChangeList()
        {
            hasChPwd = false;
            int ii = 0;
            List<string> markedRows = new List<string>();
            for (int i = 0; i < changes.Count; i++)
            {
                DataGridChange change = changes[i];
                string originalValue = viewTaskStrConv(change.OriginalValue.ToString());
                string newValue = viewTaskStrConv(change.NewValue.ToString());
                string info = "";
                string name = change.RowFirstColumnValue;
                if (name == null || name.Length == 0)
                {
                    continue;
                }
                ii++;
                if (change.RowIndex == -1 && change.ColumnIndex == -1)
                {
                    info = $"{ii}. 删除用户 \"{change.RowFirstColumnValue}\"";
                }
                else if (change.ColumnIndex == 0)
                {
                    info = $"{ii}. 创建新用户 \"{change.RowFirstColumnValue}\"";
                }
                else if (change.ColumnIndex == 1)
                {
                    info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的全名从 \"{originalValue}\" 改为 \"{newValue}\"";
                }
                else if (change.ColumnIndex == 2)
                {
                    info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的描述从 \"{originalValue}\" 改为 \"{newValue}\"";
                }
                else if (change.ColumnIndex == 4)
                {
                    //string passwd = "";
                    //for (int j = 0; j < newValue.Length; j++)
                    //{
                    //    passwd += "*";
                    //}
                    info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的密码重置为 \"{newValue}\"";
                    hasChPwd = true;
                }
                else if (change.ColumnIndex == 5)
                {
                    if (change.NewValue.ToString() == "False")
                    {
                        info = $"{ii}. 禁用用户 \"{change.RowFirstColumnValue}\"";
                    }
                    else
                    {
                        info = $"{ii}. 启用用户 \"{change.RowFirstColumnValue}\"";
                    }
                }
                else if (change.ColumnIndex == 6)
                {
                    if (change.NewValue.ToString() == "False")
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的密码设置为永不过期";
                    }
                    else
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的密码设置为需要定期更改";
                    }
                }
                else if (change.ColumnIndex == 7)
                {
                    if (change.NewValue.ToString() == "False")
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的密码设置为可以由用户更改";
                    }
                    else
                    {
                        info = $"{i + 1}. 将用户 \"{change.RowFirstColumnValue}\" 的密码设置为不允许用户更改";
                    }
                }
                else if (change.ColumnIndex >= defaultDataGridUsersColumnsCount)
                {
                    if (change.NewValue.ToString() == "False")
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 从用户组 \"{change.Title}\" 中移除";
                    }
                    else
                    {
                        info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 添加到用户组 \"{change.Title}\"";
                    }
                }
                else
                {
                    //info = $"{ii}. 将用户 \"{change.RowFirstColumnValue}\" 的 \"{change.Title}\" 从 \"{originalValue}\" 改为 \"{newValue}\"";
                    continue;
                }
                if (!markedRows.Contains(info))
                {
                    markedRows.Add(info);
                }
            }
            return markedRows.ToArray();
        }

        private string viewTaskStrConv(string info)
        {
            if (info == null || info.Length == 0)
            {
                return "（空）";
            }
            else if (info == "True")
            {
                return "是";
            }
            else if (info == "False")
            {
                return "否";
            }
            return info;
        }

        private List<string> GetMarkedDelRowsFirstColumn(DataGridViewRowCollection dataGridUsersRows)
        {
            List<string> markedRows = new List<string>();
            foreach (DataGridViewRow row in dataGridUsersRows)
            {
                if (row.DefaultCellStyle.BackColor == addDelColor[1])
                {
                    if (row.Cells[0].Value != null)
                    {
                        markedRows.Add(row.Cells[0].Value.ToString());
                    }
                }
            }
            return markedRows;
        }
    }
}
