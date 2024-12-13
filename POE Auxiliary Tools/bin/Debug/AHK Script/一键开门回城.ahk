#Persistent
Hotkey,^!a,hotkey_hc,ON UseErrorLevel
Return
AutoHC(hotKey,isStart){
    if (isStart=="True"){
        Hotkey,%hotKey%,hotkey_hc,ON UseErrorLevel
    }else{
        Hotkey,%hotKey%,hotkey_hc,Off UseErrorLevel
    }
}
hotkey_hc:
    MouseGetPos, xpos, ypos
    Send i
    ;x := 1320 * scaleX
    ;y := 880 * scaleY
    x := 1220 * scaleX
    y := 810 * scaleY
    Click, %x%,%y% right
    Send {Esc}
    MouseMove, %xpos%,%ypos%
    ;x:=616 * scaleX 
    ;y:=322 * scaleY
    ;Click, %x%,%y% left
