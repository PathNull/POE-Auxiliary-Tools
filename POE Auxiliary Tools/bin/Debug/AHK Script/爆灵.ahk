toggle := false

; 按下 F4 开启/关闭功能
F4::toggle := !toggle

; 当功能开启时，鼠标右键触发 W 键
~RButton::
    if (toggle)
    {
	Sleep 300
        Send, w
Sleep 50
        Send, w
Sleep 50
        Send, w


    }
return