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
    Send i
    Click, 1224,820 right
    Click, 616,322 left
