

WAutoBlueBottle(isStartUp,frequency){
    t := frequency * 1000
    
    if (isStartUp=="True"){ 
        SetTimer,AutoBlueBottle, %t%
    }else{
        SetTimer,AutoBlueBottle,Off
    }
}
AutoBlueBottle:
    if (WinActive("ahk_class POEWindowClass")){
        PixelGetColor, getColor, posX2, posY2
        if (getColor!=color2)  
        {
            Send 2
            Sleep, jgTime2 * 1000
        }
    }
Return