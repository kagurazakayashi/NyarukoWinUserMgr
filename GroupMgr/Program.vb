Module Program
    Sub Main()
        ' 獲取命令列引數
        Dim args As String() = Environment.GetCommandLineArgs()

        ' 顯示引數，用於除錯
        For i As Integer = 0 To args.Length - 1
            Console.WriteLine($"参数 {i}: {args(i)}")
        Next

        ' 传入计算机名
        Dim linkMachineName As String = Environment.MachineName
        If args.Length > 1 Then
            linkMachineName = args(1)
        End If
        Application.Run(New FormGroupSelect(linkMachineName))

    End Sub
End Module
