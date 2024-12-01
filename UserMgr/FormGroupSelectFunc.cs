using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UserInfo;

namespace winusermgr
{
    public partial class FormGroupSelect : Form
    {
        /// <summary>
        /// 儲存當前配置到 INI 檔案。
        /// </summary>
        /// <returns>儲存成功返回 true，失敗返回 false。</returns>
        private bool saveConfig()
        {
            try
            {
                // 寫入組數量到 INI 檔案
                iniconf.IniWriteValue("Config", "GroupsCount", listBoxSelectedGroup.Items.Count.ToString());
                // 寫入每個組名到 INI 檔案
                for (int i = 0; i < listBoxSelectedGroup.Items.Count; i++)
                {
                    iniconf.IniWriteValue("Groups", $"G{i}", listBoxSelectedGroup.Items[i].ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                // 彈出錯誤提示並允許使户者试試儲配置
                if (MessageBox.Show(ex.Message, "配置写入失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    return saveConfig();
                }
            }
            return false;
        }

        /// <summary>
        /// 從 INI 檔案載入配置。
        /// </summary>
        private void loadConfig()
        {
            // 檢查 INI 檔案是否存在
            if (!iniconf.ExistINIFile())
            {
                return;
            }

            try
            {
                // 讀取組數量並載入每個組名到列表框
                int count = int.Parse(iniconf.IniReadValue("Config", "GroupsCount"));
                for (int i = 0; i < count; i++)
                {
                    listBoxSelectedGroup.Items.Add(iniconf.IniReadValue("Groups", $"G{i}"));
                }
                // 移除重複的組名
                RemoveDuplicateItems();
            }
            catch (Exception ex)
            {
                // 彈出錯誤提示
                if (MessageBox.Show(ex.Message, "配置读取失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    loadConfig();
                }
            }
        }

        /// <summary>
        /// 獲取系統使用者組的方法，執行在單獨的執行緒中。
        /// 獲取完成後，透過 Invoke 方法更新 UI。
        /// </summary>
        private void getUserGroup()
        {
            // 呼叫 UserLoader 獲取系統使用者組資訊
            string[][] groups = UserLoader.GetGroups(linkMachineName);

            // 使用 Invoke 更新 UI 執行緒上的控制元件
            this.Invoke((Action)(() =>
            {
                // 如果獲取的組列表為空或表示錯誤，則顯示錯誤資訊並關閉視窗
                if (groups.Length == 0 || (groups.Length == 1 && groups[0].Length >= 1 && groups[0][0] == "%E%"))
                {
                    string errinfo = "无法获取系统用户组列表";
                    if (groups.Length == 1 && groups[0].Length >= 1 && groups[0][0].Length >= 2)
                    {
                        errinfo = groups[0][1]; // 使用错误信息
                    }
                    MessageBox.Show(errinfo, "无法获取系统用户组列表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    closeNow = true;
                    timerWaitAni.Enabled = false;
                    this.Close();
                    return;
                }

                // 將獲取的使用者組名稱新增到列表框中
                foreach (var group in groups)
                {
                    listBoxSystemGroup.Items.Add(group[1]);
                }

                // 將系統使用者組儲存為陣列
                systemGroups = new string[listBoxSystemGroup.Items.Count];
                listBoxSystemGroup.Items.CopyTo(systemGroups, 0);

                // 載入配置
                loadConfig();

                // 儲存初始的列表框狀態
                listBoxSystemGroupStartItems = listBoxSystemGroup.Items.Cast<string>().ToArray();
                listBoxSelectedGroupStartItems = listBoxSelectedGroup.Items.Cast<string>().ToArray();

                // 檢查是否發生更改
                chkChange();

                // 啟用停止動畫的計時器
                timerStopWaitAni.Enabled = true;
            }));
        }

        /// <summary>
        /// 檢查使用者組列表框是否有更改，並啟用/禁用確定按鈕。
        /// </summary>
        /// <returns>返回所有列表框是否都未更改</returns>
        private bool chkChange()
        {
            // 獲取當前列表框的專案
            string[] listBoxSystemGroupNowItems = listBoxSystemGroup.Items.Cast<string>().ToArray();
            string[] listBoxSelectedGroupNowItems = listBoxSelectedGroup.Items.Cast<string>().ToArray();

            // 比較當前專案和初始專案是否一致
            bool listBoxSystemGroupEqual = listBoxSystemGroupStartItems.SequenceEqual(listBoxSystemGroupNowItems);
            bool listBoxSelectedGroupEqual = listBoxSelectedGroupStartItems.SequenceEqual(listBoxSelectedGroupNowItems);

            // 檢查所有列表框是否都未更改
            bool allEqual = listBoxSystemGroupEqual && listBoxSelectedGroupEqual;

            // 根據是否更改設定確定按鈕的啟用狀態
            buttonOK.Enabled = !allEqual;
            return allEqual;
        }

        /// <summary>
        /// 檢查當前系統使用者組列表框中新增的使用者組。
        /// </summary>
        /// <returns>返回新增使用者組的字串陣列</returns>
        private string[] chkSystemGroup()
        {
            // 建立一個列表用於儲存新增的使用者組
            List<string> list = new List<string>();

            // 遍歷當前使用者組列表框的專案
            foreach (var item in listBoxSystemGroup.Items)
            {
                // 如果該使用者組不在原始組中，則認為是新增的
                if (!systemGroups.Contains(item.ToString()))
                {
                    list.Add(item.ToString());
                }
            }

            // 返回新增使用者組的陣列
            return list.ToArray();
        }

        /// <summary>
        /// 檢查並更新新增和移除按鈕的啟用狀態。
        /// </summary>
        private void chkBtnEnable()
        {
            // 如果已選組列表框中有選中項，則啟用移除按鈕
            buttonRemove.Enabled = listBoxSelectedGroup.SelectedIndex != -1;
            // 如果系統組列表框中有選中項，則啟用新增按鈕
            buttonAdd.Enabled = listBoxSystemGroup.SelectedIndex != -1;
        }

        /// <summary>
        /// 移除在列表框中重複的專案。
        /// 從 listBoxSelectedGroup 中的專案中查詢，並刪除 listBoxSystemGroup 中的重複項。
        /// </summary>
        private void RemoveDuplicateItems()
        {
            // 遍歷 listBoxSelectedGroup 的所有專案
            for (int i = listBoxSelectedGroup.Items.Count - 1; i >= 0; i--)
            {
                // 遍歷 listBoxSystemGroup 的所有專案
                for (int j = listBoxSystemGroup.Items.Count - 1; j >= 0; j--)
                {
                    // 如果兩個列表中有相同的專案，則從 listBoxSystemGroup 中移除
                    if (listBoxSelectedGroup.Items[i].ToString() == listBoxSystemGroup.Items[j].ToString())
                    {
                        listBoxSystemGroup.Items.RemoveAt(j);
                    }
                }
            }
        }

        /// <summary>
        /// 控制等待動畫的啟用或禁用。
        /// </summary>
        /// <param name="enable">是否啟用等待動畫。true 表示啟用，false 表示禁用。</param>
        private void waitAni(bool enable)
        {
            // 如果禁用動畫並且結束模式為 -1，則直接返回
            if (!enable && timerStopWaitAniEndMode == -1)
            {
                return;
            }

            // 如果禁用動畫並且結束模式為 1，則退出應用程式
            if (!enable && timerStopWaitAniEndMode == 1)
            {
                Application.Exit();
            }

            // 設定等待游標狀態
            UseWaitCursor = enable;

            // 設定等待標籤的可見性
            labelWait.Visible = enable;

            // 如果啟用動畫並且動畫字型不為空
            if (enable && slboot.aniFont != null)
            {
                // 啟用等待動畫和停止等待動畫的定時器
                timerWaitAni.Enabled = enable;
                timerStopWaitAni.Enabled = enable;
            }
            else
            {
                // 禁用定時器並停止更新字元
                timerWaitAni.Enabled = false;
                timerStopWaitAni.Enabled = false;
                slboot.StopUpdateChar();
            }
        }
    }
}
