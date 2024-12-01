using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace winusermgr
{
    /// <summary>
    /// 檢查是否以管理員身份執行程式和重新以管理員許可權啟動程式。
    /// </summary>
    internal class UAC
    {
        /// <summary>
        /// 檢查當前程序是否以管理員許可權執行。
        /// </summary>
        /// <returns>如果當前程序以管理員許可權執行，則返回 true；否則返回 false。</returns>
        public static bool IsRunAsAdministrator()
        {
            try
            {
                // 獲取當前程序的 Windows 身份資訊
                WindowsIdentity identity = WindowsIdentity.GetCurrent();

                // 建立一個 WindowsPrincipal 物件，用於檢查角色許可權
                WindowsPrincipal principal = new WindowsPrincipal(identity);

                // 檢查當前使用者是否屬於內建管理員角色
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                // 如果發生異常，返回 false，表示不是以管理員許可權執行
                return false;
            }
        }

        /// <summary>
        /// 重新以管理員許可權啟動當前應用程式。
        /// </summary>
        /// <returns>如果啟動成功，則返回 true；如果啟動失敗，則返回 false。</returns>
        public static bool RestartAsAdministrator()
        {
            try
            {
                // 建立一個新的程序啟動資訊物件
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath, // 當前應用程式的路徑
                    UseShellExecute = true,                // 使用外殼程式執行
                    Verb = "runas"                         // 指定操作為 "runas"，以管理員身份執行
                };

                // 啟動新的程序
                Process.Start(processInfo);

                // 如果啟動成功，返回 true
                return true;
            }
            catch (Exception)
            {
                // 如果發生異常（例如使用者拒絕許可權提升），返回 false
                // MessageBox.Show("无法以管理员权限重新启动应用程序。\n\n错误信息：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }

}
