#Persistent
Hotkey,^!a,hotkey_1,ON UseErrorLevel
Return
ContinuityClick(hotKey,isStart){
    if (isStart=="True"){
        Hotkey,%hotKey%,hotkey_1,ON UseErrorLevel
    }else{
        Hotkey,%hotKey%,hotkey_1,Off UseErrorLevel
    }
}
hotkey_1:
    Send, {LControl Down}
    Loop
    {
        GetKeyState, state, %hotKey%, P;
        if state = U
        {
            Send, {LControl Up}
            Break
        }
        Else
        {
            Send {LButton}
            Sleep, clickFrequency
        }
    }
Return