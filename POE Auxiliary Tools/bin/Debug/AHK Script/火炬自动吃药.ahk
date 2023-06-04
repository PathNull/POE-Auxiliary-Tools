Return

WAutoBloodBottle_HJ(isStartUp,frequency){
    if (isStartUp=="True"){ 
        SetTimer,AutoBloodBottleHJ, 1000
    }else{
        SetTimer,AutoBloodBottleHJ,Off
    }
}

AutoBloodBottleHJ:
 if (WinActive("ahk_class UnrealWindow")){
   PixelGetColor, getColor, posX, posY
        if (getColor!=color)  
        {
            Send q
            Sleep, 1000
        }
 }
      





