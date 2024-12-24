Module Program
    Sub Main()
        ' 獲取命令列引數
        Dim args As String() = Environment.GetCommandLineArgs()

        ' 传入计算机名
        Dim linkMachineName As String = Environment.MachineName
        If args.Length > 1 Then
            linkMachineName = args(1)
        End If
        Application.Run(New FormGroupSelect(linkMachineName))

    End Sub
End Module
