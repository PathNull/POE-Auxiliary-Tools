#Persistent
Hotkey,^!a,hotkey_kills,ON UseErrorLevel
a_switch:=0
Return

AutoSkills(hotKey,isStart){
    if (isStart=="True"){
        Hotkey,%hotKey%,hotkey_kills,ON UseErrorLevel
    }else{
        Hotkey,%hotKey%,hotkey_kills,Off UseErrorLevel
         a_switch:=1-a_switch
    }
}
hotkey_kills:
    if (WinActive("ahk_class POEWindowClass")){
         a_switch:=1-a_switch
         if(a_switch=1){
            key_array := StrSplit(keys, A_Space) 
            time_array := StrSplit(times, A_Space) 
            count := key_array.MaxIndex() - 1
            Loop % count{
                this_key := key_array[a_index]
                this_time := time_array[a_index]
                if(this_key == "q"){
                    t := %this_time% * 1000
                    SetTimer,DownQ, 1000
                }
                if(this_key == "w"){
                   t := %this_time% * 1000
                   SetTimer,DownW, 1000
                }
                if(this_key == "e"){
                   t := %this_time% * 1000
                   SetTimer,DownE, 1000
                }
                if(this_key == "r"){
                   t := %this_time% * 1000
                   SetTimer,DownR, 1000
                }
                if(this_key == "t"){
                   t := %this_time% * 1000
                   SetTimer,DownT, 1000
                }
                
            }   
         }
         
    }
return
DownQ:
if(a_switch=1){
  MsgBox , qqqq
  Send q
}
return 
DownW:
if(a_switch=1){
  Send w
}
return 
DownE:
if(a_switch=1){
  Send e
}
return 
DownR:
if(a_switch=1){
  Send r
}
return 
DownT:
if(a_switch=1){
  Send t
}
return 
   