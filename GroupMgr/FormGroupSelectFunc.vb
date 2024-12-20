Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Windows.Forms
Imports SystemRes
Imports UserInfo

Partial Public Class FormGroupSelect
    Inherits Form

    ''' <summary>
    ''' 載入設定和系統內使用者組列表
    ''' </summary>
    Private Sub reloadConfig()
        ' 如果動畫字型不為空，開始顯示等待動畫
        If slboot.aniFont IsNot Nothing Then
            ' 顯示等待標籤
            labelWait.Visible = True
            ' 啟動等待動畫的定時器
            timerWaitAni.Enabled = True
        End If

        ' 清空已選擇的群組列表框
        listBoxSelectedGroup.Items.Clear()
        ' 清空系統群組列表框
        listBoxSystemGroup.Items.Clear()

        ' 將列表框的起始專案設為空
        listBoxSystemGroupStartItems = Nothing
        listBoxSelectedGroupStartItems = Nothing

        ' 停用刪除群組的按鈕
        buttonDelGroup.Enabled = False
        ' 停用確認的按鈕
        buttonOK.Enabled = False

        ' 建立一個後臺執行緒，用於非同步獲取使用者組
        Dim thread As New Thread(AddressOf getUserGroup) With {
        .IsBackground = True ' 設定執行緒為後臺執行緒
    }
        thread.Start() ' 啟動執行緒
    End Sub


    ''' <summary>
    ''' 儲存當前配置到 INI 檔案。
    ''' </summary>
    ''' <returns>儲存成功返回 true，失敗返回 false。</returns>
    Private Function saveConfig() As Boolean
        Try
            ' 寫入組數量到 INI 檔案
            iniconf.IniWriteValue("Config", "GroupsCount", listBoxSelectedGroup.Items.Count.ToString())
            ' 寫入每個組名到 INI 檔案
            For i As Integer = 0 To listBoxSelectedGroup.Items.Count - 1
                iniconf.IniWriteValue("Groups", $"G{i}", listBoxSelectedGroup.Items(i).ToString())
            Next
            Return True
        Catch ex As Exception
            ' 彈出錯誤提示並允許使户者试試儲配置
            If MessageBox.Show(ex.Message, "配置写入失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Retry Then
                Return saveConfig()
            End If
        End Try
        Return False
    End Function

    ''' <summary>
    ''' 載入 INI 配置檔案中的組資訊並填充到列表框中。
    ''' 如果配置檔案不存在或配置讀取失敗，則適當處理。
    ''' </summary>
    Private Sub loadConfig()
        ' 檢查配置檔案是否存在，如果不存在，則直接返回。
        If Not iniconf.ExistINIFile() Then
            Return
        End If

        Try
            ' 讀取配置中的組數量。
            Dim countS As String = iniconf.IniReadValue("Config", "GroupsCount")
            ' 如果組數量字串為空，則直接返回。
            If countS.Length = 0 Then
                Return
            End If

            ' 將組數量字串轉換為整數。
            Dim count As Integer = Integer.Parse(countS)
            ' 遍歷每個組，將其新增到列表框中。
            For i As Integer = 0 To count - 1
                listBoxSelectedGroup.Items.Add(iniconf.IniReadValue("Groups", $"G{i}"))
            Next

            ' 移除列表框中的重複項。
            removeDuplicateItems()
        Catch ex As Exception
            ' 如果讀取配置時發生異常，提示使用者重試或取消。
            If MessageBox.Show(ex.Message, "配置讀取失敗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Retry Then
                ' 如果使用者選擇重試，則再次呼叫 loadConfig 方法。
                loadConfig()
            End If
        End Try
    End Sub


    ''' <summary>
    ''' 獲取系統使用者組的方法，執行在單獨的執行緒中。
    ''' 獲取完成後，透過 Invoke 方法更新 UI。
    ''' </summary>
    Private Sub getUserGroup()
        ' 呼叫 UserLoader 獲取系統使用者組資訊
        Dim groups As String()() = UserLoader.GetGroups(toolStripComboBoxMachine.Text)

        ' 使用 Invoke 更新 UI 執行緒上的控制元件
        Me.Invoke(CType(Sub()
                            ' 检查获取的用户组列表是否为空或包含特定的错误标识
                            If groups.Length = 0 OrElse (groups.Length = 1 AndAlso groups(0).Length >= 1 AndAlso groups(0)(0) = "%E%") Then
                                ' 构建错误信息，提示无法获取当前机器的用户组列表
                                Dim errinfo As String = "无法获取 " + toolStripComboBoxMachine.Text + " 的用户组列表"

                                ' 如果用户组列表长度为 1，并且该组包含足够的元素（至少有 2 个），
                                ' 则将附加第二个元素（详细的错误信息）到错误信息中
                                If groups.Length = 1 AndAlso groups(0).Length >= 1 AndAlso groups(0)(0).Length >= 2 Then
                                    errinfo += "\n" + groups(0)(1)  ' 如果有额外的错误信息，追加到错误信息
                                End If

                                ' 弹出消息框，显示错误信息，并停止当前操作
                                MessageBox.Show(errinfo, "无法获取用户组列表", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                ' 设置关闭标志为 True，表示程序需要关闭
                                closeNow = True

                                ' 禁用等待动画的定时器
                                timerWaitAni.Enabled = False

                                ' 如果当前机器名等于选中的机器名，则关闭当前窗体
                                If toolStripComboBoxMachine.Text = Environment.MachineName Then
                                    Close()
                                Else
                                    ' 否则，将当前机器名设置为当前机器，并重新获取用户组
                                    toolStripComboBoxMachine.Text = Environment.MachineName
                                    getUserGroup()  ' 重新调用获取用户组的函数
                                End If

                                ' 提前结束当前方法的执行
                                Return
                            End If

                            ' 如果没有发生错误，将窗体标题设置为选中的机器名加上默认标题
                            Text = toolStripComboBoxMachine.Text + defaultTitle


                            ' 將獲取的使用者組名稱新增到列表框中
                            For Each group In groups
                                listBoxSystemGroup.Items.Add(group(1))
                            Next

                            ' 將系統使用者組儲存為陣列
                            systemGroups = New String(listBoxSystemGroup.Items.Count - 1) {}
                            listBoxSystemGroup.Items.CopyTo(systemGroups, 0)

                            ' 載入配置
                            loadConfig()

                            ' 儲存初始的列表框狀態
                            listBoxSystemGroupStartItems = listBoxSystemGroup.Items.Cast(Of String)().ToArray()
                            listBoxSelectedGroupStartItems = listBoxSelectedGroup.Items.Cast(Of String)().ToArray()

                            ' 檢查是否發生更改
                            chkChange()

                            ' 啟用停止動畫的計時器
                            timerStopWaitAni.Enabled = True
                        End Sub, Action))
    End Sub

    ''' <summary>
    ''' 新增使用者組的方法。
    ''' 建立一個新的使用者組，並根據操作結果更新 UI。
    ''' </summary>
    Private Sub addUserGroup()
        ' 建立一個 UserInfoModifier 物件，用於使用者資訊修改操作
        Dim chUser As UserInfoModifier = New UserInfoModifier(nowMachine)

        ' 呼叫 CreateGroup 方法建立使用者組，傳入使用者輸入的組名
        Dim err As String = chUser.CreateGroup(textBoxCustom.Text)

        ' 使用 Invoke 更新 UI 執行緒上的控制元件
        Me.Invoke(CType(Sub()
                            ' 如果建立使用者組時發生錯誤，顯示錯誤資訊
                            If err.Length > 0 Then
                                MessageBox.Show(err, "建立使用者組 " + textBoxCustom.Text + " 失敗", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Else
                                ' 如果建立成功，顯示成功資訊
                                MessageBox.Show("已建立使用者組 " + textBoxCustom.Text, "建立使用者組成功", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                            ' 重新載入配置資訊
                            reloadConfig()
                        End Sub, Action))
    End Sub

    ''' <summary>
    ''' 刪除使用者組的方法。
    ''' 刪除指定的使用者組，並根據操作結果更新 UI。
    ''' </summary>
    Private Sub delUserGroup()
        ' 建立一個 UserInfoModifier 物件，用於使用者資訊修改操作
        Dim chUser As UserInfoModifier = New UserInfoModifier(nowMachine)

        ' 呼叫 DeleteGroup 方法刪除使用者組，傳入當前操作的組名
        Dim err As String = chUser.DeleteGroup(nowAction)

        ' 使用 Invoke 更新 UI 執行緒上的控制元件
        Me.Invoke(CType(Sub()
                            ' 如果刪除使用者組時發生錯誤，顯示錯誤資訊
                            If err.Length > 0 Then
                                MessageBox.Show(err, "刪除使用者組 " + nowAction + " 失敗", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Else
                                ' 如果刪除成功，顯示成功資訊
                                MessageBox.Show("已刪除使用者組 " + nowAction, "刪除使用者組成功", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                            ' 重新載入配置資訊
                            reloadConfig()
                        End Sub, Action))
    End Sub

    ''' <summary>
    ''' 檢查使用者組列表框是否有更改，並啟用/禁用確定按鈕。
    ''' </summary>
    ''' <returns>返回所有列表框是否都未更改</returns>
    Private Function chkChange() As Boolean
        ' 獲取當前列表框的專案
        Dim listBoxSystemGroupNowItems As String() = listBoxSystemGroup.Items.Cast(Of String)().ToArray()
        Dim listBoxSelectedGroupNowItems As String() = listBoxSelectedGroup.Items.Cast(Of String)().ToArray()

        ' 比較當前專案和初始專案是否一致
        Dim listBoxSystemGroupEqual As Boolean = listBoxSystemGroupStartItems.SequenceEqual(listBoxSystemGroupNowItems)
        Dim listBoxSelectedGroupEqual As Boolean = listBoxSelectedGroupStartItems.SequenceEqual(listBoxSelectedGroupNowItems)

        ' 檢查所有列表框是否都未更改
        Dim allEqual As Boolean = listBoxSystemGroupEqual AndAlso listBoxSelectedGroupEqual

        ' 根據是否更改設定確定按鈕的啟用狀態
        buttonOK.Enabled = Not allEqual
        Return allEqual
    End Function

    ''' <summary>
    ''' 檢查是否移除虛擬使用者組。
    ''' </summary>
    ''' <returns>如果有需要移除的組，返回 True；否則返回 False。</returns>
    Private Function chkIsRemoveVGroup() As Boolean
        ' 呼叫 chkSystemGroup 方法獲取需要移除的組
        Dim removedGroups As String() = chkSystemGroup()
        ' 如果返回的組數量大於 0，說明需要移除
        Return removedGroups.Length > 0
    End Function

    ''' <summary>
    ''' 檢查是否有未儲存的更改，並提示使用者操作。
    ''' </summary>
    ''' <returns>返回使用者在對話方塊中選擇的結果。</returns>
    Private Function ChkUnsavedChanges() As DialogResult
        ' 如果沒有更改，提示使用者是否儲存。
        If Not chkChange() Then
            Return MessageBox.Show(
                "列的显示配置还没有保存，是否要保存更改？",
                "更改未保存",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question)
        End If
        ' 如果沒有觸發儲存提示，返回 Abort 表示無操作
        Return DialogResult.Abort
    End Function

    ''' <summary>
    ''' 檢查當前選擇的組是否為虛擬使用者組。
    ''' </summary>
    ''' <returns>如果是虛擬使用者組，返回 True；否則返回 False。</returns>
    Private Function itemIsV() As Boolean
        ' 獲取系統中標記為虛擬使用者的組
        Dim vGroups As String() = chkSystemGroup()
        ' 初始化變數以儲存是否為虛擬使用者組的結果
        Dim isV As Boolean = False
        ' 如果虛擬使用者組列表不為空，且使用者選擇了一個有效的組
        If vGroups.Length > 0 And listBoxSelectedGroup.SelectedIndex > -1 Then
            ' 遍歷虛擬使用者組列表，檢查是否與選定的組匹配
            For Each item In vGroups
                If item = listBoxSelectedGroup.SelectedItem.ToString() Then
                    ' 如果匹配，設定 isV 為 True 並退出迴圈
                    isV = True
                    Exit For
                End If
            Next
        End If
        ' 返回檢查結果
        Return isV
    End Function

    ''' <summary>
    ''' 檢查當前系統使用者組列表框中新增的使用者組。
    ''' </summary>
    ''' <returns>返回新增使用者組的字串陣列</returns>
    Private Function chkSystemGroup() As String()
        ' 建立一個列表用於儲存新增的使用者組
        Dim list As New List(Of String)()

        ' 遍歷當前使用者組列表框的
        For Each item In listBoxSystemGroup.Items
            ' 如果該使用者組不在原始組中，則認為是新增的
            If Not systemGroups.Contains(item.ToString()) Then
                list.Add(item.ToString())
            End If
        Next
        ' 遍歷當前系統組列表框
        For Each item In listBoxSelectedGroup.Items
            ' 如果該使用者組不在原始組中，則認為是新增的
            If Not systemGroups.Contains(item.ToString()) Then
                list.Add(item.ToString())
            ElseIf list.Contains(item.ToString()) Then
                ' 如果該使用者組在新增列表中，則移除
                list.Remove(item.ToString())
            End If
        Next

        ' 返回新增使用者組的陣列
        Return list.ToArray()
    End Function

    ''' <summary>
    ''' 檢查並更新新增和移除按鈕的啟用狀態。
    ''' </summary>
    Private Sub chkBtnEnable()
        ' 如果系統組列表框中有選中項，則啟用刪除按鈕
        buttonDelGroup.Enabled = listBoxSystemGroup.SelectedIndex <> -1
        ' 如果已選組列表框中有選中項，則啟用移除按鈕
        buttonRemove.Enabled = listBoxSelectedGroup.SelectedIndex <> -1
        ' 如果系統組列表框中有選中項，則啟用新增按鈕
        buttonAdd.Enabled = listBoxSystemGroup.SelectedIndex <> -1
    End Sub

    ''' <summary>
    ''' 移除在列表框中重複的專案。
    ''' 從 listBoxSelectedGroup 中的專案中查詢，並刪除 listBoxSystemGroup 中的重複項。
    ''' </summary>
    Private Sub removeDuplicateItems()
        ' 遍歷 listBoxSelectedGroup 的所有專案
        For i As Integer = listBoxSelectedGroup.Items.Count - 1 To 0 Step -1
            ' 遍歷 listBoxSystemGroup 的所有專案
            For j As Integer = listBoxSystemGroup.Items.Count - 1 To 0 Step -1
                ' 如果兩個列表中有相同的專案，則從 listBoxSystemGroup 中移除
                If listBoxSelectedGroup.Items(i).ToString() = listBoxSystemGroup.Items(j).ToString() Then
                    listBoxSystemGroup.Items.RemoveAt(j)
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' 控制等待動畫的啟用或禁用。
    ''' </summary>
    ''' <param name="enable">是否啟用等待動畫。true 表示啟用，false 表示禁用。</param>
    Private Sub waitAni(enable As Boolean)
        ' 如果禁用動畫並且結束模式為 -1，則直接返回
        If Not enable AndAlso timerStopWaitAniEndMode = -1 Then
            Return
        End If

        ' 如果禁用動畫並且結束模式為 1，則退出應用程式
        If Not enable AndAlso timerStopWaitAniEndMode = 1 Then
            Application.Exit()
        End If

        ' 設定等待游標狀態
        UseWaitCursor = enable

        ' 設定等待標籤的可見性
        labelWait.Visible = enable

        ' 如果啟用動畫並且動畫字型不為空
        If enable AndAlso slboot.aniFont IsNot Nothing Then
            ' 啟用等待動畫和停止等待動畫的定時器
            timerWaitAni.Enabled = enable
            timerStopWaitAni.Enabled = enable
        Else
            ' 禁用定時器並停止更新字元
            timerWaitAni.Enabled = False
            timerStopWaitAni.Enabled = False
            slboot.StopUpdateChar()
        End If
    End Sub

    ''' <summary>
    ''' 檢查並嘗試以管理員許可權執行程式。
    ''' 如果程式未以管理員許可權執行，提示使用者獲取管理員許可權。
    ''' 如果使用者同意獲取許可權，將以管理員許可權重新啟動程式。
    ''' </summary>
    ''' <returns>
    ''' 如果程式已以管理員許可權執行，則返回 True；否則返回 False。
    ''' </returns>
    Private Function useAdmin() As Boolean
        ' 檢查當前是否已以管理員許可權執行
        If UAC.IsRunAsAdministrator() Then
            ' 如果已是管理員許可權，則返回 True
            Return True
        End If

        ' 如果未以管理員許可權執行，提示使用者獲取許可權
        If MessageBox.Show(
            "此操作需要以管理員許可權執行本程式。" & vbCrLf &
            "要獲取管理員許可權嗎？" & vbCrLf &
            "未儲存的改動將丟失。",
            "許可權不足",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) = DialogResult.Yes Then
            ' 如果使用者選擇“是”，嘗試以管理員許可權重新啟動程式
            If UAC.RestartAsAdministrator() Then
                ' 如果重新啟動成功，退出當前程式例項
                Application.Exit()
            End If
        End If

        ' 如果未獲取管理員許可權，則返回 False
        Return False
    End Function

    ''' <summary>
    ''' 用於處理使用者操作的函式，包含對未儲存更改的檢查和相關處理。
    ''' </summary>
    ''' <returns>如果可以繼續操作則返回 True，否則返回 False。</returns>
    Private Function userAction() As Boolean
        ' 檢查是否有未儲存的更改，返回一個對話方塊結果
        Dim result As DialogResult = ChkUnsavedChanges()

        ' 根據使用者的選擇執行相應的操作
        If result = DialogResult.Abort Then
            ' 沒有未儲存的更改
            Return True
        ElseIf result = DialogResult.Yes Then
            ' 有未儲存的更改並且選擇儲存
            closeNO = True ' 設定標誌表示不關閉
            buttonOK_Click(Nothing, EventArgs.Empty) ' 呼叫確認按鈕點選事件進行儲存操作
            reloadConfig() ' 重新載入配置
            Return True
        ElseIf result = DialogResult.No Then
            ' 有未儲存的更改但是選擇不儲存
            reloadConfig() ' 重新載入配置
            Return True
        ElseIf result = DialogResult.Cancel Then
            ' 有未儲存的更改並選擇取消操作
            Return False
        End If

        ' 如果動畫字型不為空，啟動等待動畫
        If slboot.aniFont IsNot Nothing Then
            labelWait.Visible = True ' 顯示等待動畫標籤
            timerWaitAni.Enabled = True ' 啟用等待動畫計時器
        End If

        ' 返回 True，表示操作成功完成
        Return True
    End Function

End Class
