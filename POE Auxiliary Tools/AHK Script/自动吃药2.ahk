

WAutoBloodBottle(isStartUp,frequency2){
    t := frequency2 * 1000
    
    if (isStartUp=="True"){ 
        SetTimer,AutoBloodBottle, %t%
    }else{
        SetTimer,AutoBloodBottle,Off
    }
}
WAutoDabaoPotion(isStart){
    if (isStart=="True"){
        SetTimer,AutoDabaoPotion1,6000
        Sleep 200
        SetTimer,AutoDabaoPotion2,3000
        Sleep 200
        SetTimer,AutoDabaoPotion3,4800
    }else{
        SetTimer,AutoDabaoPotion1,Off
        SetTimer,AutoDabaoPotion2,Off
        SetTimer,AutoDabaoPotion3,Off
    }
}
AutoBloodBottle:
    if (WinActive("ahk_class POEWindowClass")){
        PixelGetColor, getColor, posX, posY
        aj_array := StrSplit(wAutoBloodParm, A_Space) 
        count := aj_array.MaxIndex() - 1
       
        if (getColor!=color)  
        {
            Send 1

            Loop % count{
                this_aj := aj_array[a_index]
                Send %this_aj%
            }   

            Sleep, jgTime * 1000
        }
    }
Return
AutoDabaoPotion1:
    if (WinActive("ahk_class POEWindowClass")){
        Send 3
    }
Return
AutoDabaoPotion2:
    if (WinActive("ahk_class POEWindowClass")){
        Send 4
    }
Return
AutoDabaoPotion3:
    if (WinActive("ahk_class POEWindowClass")){
        Send 5
    }
Return
