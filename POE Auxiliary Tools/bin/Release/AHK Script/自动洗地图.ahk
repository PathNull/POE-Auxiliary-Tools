Main(dangerStr,quantity,isMapNail,mapType){
    WinActivate, ahk_exe PathOfExile_x64.exe
    if (quantity==""){
        quantity = 1
    }
    dangerList :=  StrSplit(dangerStr,A_Space)
    mapPosArray :=[] ;地图坐标
    posArray :=  GetPos() ;初始化 12*12 格子坐标数据
    noIden := []       ;未鉴定
    refreshArray :=[]  ;需要重铸的
    no20 := []         ;需要打品质的
    dangerArray :=[]   ;有危险词缀的
    upgradeArray :=[]  ;需要点金的
    quantityArray:=[]  ;物品数量不够的
    if (mapType=="地图"){
        mapPosArray := PixSearch(posArray) ;搜索12*12格子 找到地图 初始化坐标数据
    }
    if (mapType=="日志"){
        mapPosArray := RZPixSearch(posArray) ;搜索12*12格子 找到日志 初始化坐标数据
    }
    
    
    lastClip := "start"
    clipboard :="start"
    Loop % mapPosArray.MaxIndex(){
        mousemove, mapPosArray[A_Index][1],mapPosArray[A_Index][2]
        lastClip := return Map_Copy(lastClip,2)
        clip :=  RegExReplace(lastClip,"`r`n","")
        ;MsgBox , %clip%
        ;test := RegExMatch(clip,"普通")
        ;MsgBox , %test%
        ;未鉴定的 
        if (RegExMatch(clip,"未鉴定")!=0){
            noIden.Insert(mapPosArray[A_Index])
        }
        if (mapType=="地图"){
            ;品质不够20
        if (RegExMatch(clip,"品质: \+20%") ==0){
            no20.Insert(mapPosArray[A_Index])
        }
        ;不是稀有 或者 是稀有但是有辣鸡词缀
        if ((RegExMatch(clip,"稀 有 度: 普通") ==0 and RegExMatch(clip,"品质: \+20%") ==0) ) or (RegExMatch(clip,"稀 有 度: 魔法") !=0)  {
            refreshArray.Insert(mapPosArray[A_Index])
            upgradeArray.Insert(mapPosArray[A_Index])
            dangerArray.Insert(mapPosArray[A_Index])
        }
        else if (RegExMatch(clip,"稀 有 度: 普通") !=0){
            upgradeArray.Insert(mapPosArray[A_Index])
            dangerArray.Insert(mapPosArray[A_Index])
        }
        }
        if (mapType=="日志"){
        ;物品数量
            if (!RegExMatch(clip,"稀 有 度: 普通")!=0){
                RegExMatch(clipboard, "物品数量: \+(.*?)%", SubPat)
                if (quantity>SubPat1){
                    dangerArray.Insert(mapPosArray[A_Index])
                }
            }else{
                dangerArray.Insert(mapPosArray[A_Index])
                upgradeArray.Insert(mapPosArray[A_Index])
            }
        }
        else{
            index := A_Index
            ;危险词缀
            Loop % dangerList.MaxIndex(){
                if (RegExMatch(clip,dangerList[A_Index]) !=0 and RegExMatch(clip,"已腐化")==0){
                    dangerArray.Insert(mapPosArray[index])
                }
            }
            ;物品数量
             if (!RegExMatch(clip,"稀 有 度: 普通")!=0){
                RegExMatch(clipboard, "物品数量: \+(.*?)%", SubPat)
                if (quantity>SubPat1){
                    dangerArray.Insert(mapPosArray[A_Index])
                }
            }
        }
    }
    ;未鉴定的全部鉴定
    if (noIden.MaxIndex()>=1){
        Click 761,118 left ;左键点击通货页
        Sleep 200
        Click 110,202 right ;右键点击鉴定卷轴
        Sleep 200
        Click 752,142 left ;左键点击地图页
        Sleep 200
        Send {Shift Down}   
        Loop % noIden.MaxIndex(){
            x := noIden[A_Index][1]
            y := noIden[A_Index][2]
            Click %x% %y% left
            Sleep 200
        }
        Send {Shift Up}
    }
    ;重铸掉不是稀有或者品质不够20%的
    if (refreshArray.MaxIndex()>=1){
        Click 761,118 left ;左键点击通货页
        Sleep 200
        Click 435,510 right ;右键点击重铸石
        Sleep 200
        Click 752,142 left ;左键点击地图页
        Sleep 200
        Send {Shift Down} 
        Loop % refreshArray.MaxIndex(){
            x := refreshArray[A_Index][1]
            y := refreshArray[A_Index][2]
            Click %x% %y% left
            Sleep 200
        }
        Send {Shift Up}
    }
    ;打上品质
    if (isMapNail=="True"){
        if (no20.MaxIndex()>=1){
            Click 761,118 left ;左键点击通货页
            Sleep 200
            Click 605,201 right ;右键点击制图钉
            Sleep 200
            Click 752,142 left ;左键点击地图页
            Sleep 200
            Send {Shift Down} 
            Loop % no20.MaxIndex(){
                x := no20[A_Index][1]
                y := no20[A_Index][2]
                Loop, 5{
                    Click %x% %y% left
                       Sleep 100
                }
            }
        Send {Shift Up}
        }
    }
    
    ;点金
    if (upgradeArray.MaxIndex()>=1){
        Click 761,118 left ;左键点击通货页
        Sleep 200
        Click 493,272 right ;右键点击点金石
        Sleep 200
        Click 752,142 left ;左键点击地图页
        Sleep 200
        Send {Shift Down} 
        Loop % upgradeArray.MaxIndex(){
            x := upgradeArray[A_Index][1]
            y := upgradeArray[A_Index][2]
            Click %x% %y% left
            Sleep 200
        }
        Send {Shift Up}
    }
    ;处理不想打得词缀 和 物品数量不够的
    Filter(quantity,dangerList,dangerArray)
    MsgBox,  完成!
}
;洗撕图地图
QuickMap(){
     WinActivate, ahk_exe PathOfExile_x64.exe
     posArray :=  GetPos() ;初始化 12*12 格子坐标数据
	 refreshArray :=[]  ;需要重铸的
	 noIden := []       ;未鉴定的魔法地图
     upgradeArray :=[]  ;需要点金的
	 mapPosArray := PixSearch(posArray) ;搜索12*12格子 找到地图 初始化坐标数据
	 lastClip  :=  "start"
     clipboard :="start"
	 Loop % mapPosArray.MaxIndex(){
		mousemove, mapPosArray[A_Index][1],mapPosArray[A_Index][2]
        lastClip := return Map_Copy(lastClip,2)
        clip :=  RegExReplace(lastClip,"`r`n","")
		 if (RegExMatch(clip,"稀 有 度: 魔法") !=0){
            if(RegExMatch(clip,"未鉴定")!=0){
                noIden.Insert(mapPosArray[A_Index])
            }
            refreshArray.Insert(mapPosArray[A_Index])
            upgradeArray.Insert(mapPosArray[A_Index])
         }
         if (RegExMatch(clip,"稀 有 度: 普通") !=0){
            upgradeArray.Insert(mapPosArray[A_Index])
            
         }
	}
	;未鉴定的全部鉴定
    if (noIden.MaxIndex()>=1){
        Click 761,118 left ;左键点击通货页
        Sleep 200
        Click 110,202 right ;右键点击鉴定卷轴
        Sleep 200
        Click 752,142 left ;左键点击地图页
        Sleep 200
        Send {Shift Down}   
        Loop % noIden.MaxIndex(){
            x := noIden[A_Index][1]
            y := noIden[A_Index][2]
            Click %x% %y% left
            Sleep 200
        }
        Send {Shift Up}
    }
    ;重铸
    if (refreshArray.MaxIndex()>=1){
        Click 761,118 left ;左键点击通货页
        Sleep 200
        Click 435,510 right ;右键点击重铸石
        Sleep 200
        Click 752,142 left ;左键点击地图页
        Sleep 200
        Send {Shift Down} 
        Loop % refreshArray.MaxIndex(){
            x := refreshArray[A_Index][1]
            y := refreshArray[A_Index][2]
            Click %x% %y% left
            Sleep 200
        }
        Send {Shift Up}
    }
    ;点金
    if (upgradeArray.MaxIndex()>=1){
        Click 761,118 left ;左键点击通货页
        Sleep 200
        Click 493,272 right ;右键点击点金石
        Sleep 200
        Click 752,142 left ;左键点击地图页
        Sleep 200
        Send {Shift Down} 
        Loop % upgradeArray.MaxIndex(){
            x := upgradeArray[A_Index][1]
            y := upgradeArray[A_Index][2]
            Click %x% %y% left
            Sleep 200
        }
        Send {Shift Up}
    }
     MsgBox,  完成!
}



;不想打得词缀重铸点金
Filter(quantity,dangerList,dangerArray){
    lastClip := "start"
    clipboard :="start"
    tempArray := [] 
    Loop % dangerArray.MaxIndex(){
        mousemove, dangerArray[A_Index][1],dangerArray[A_Index][2]
        lastClip := return Map_Copy(lastClip,5)
        clip :=  RegExReplace(lastClip,"`r`n","")
        index := A_Index
        ;危险词缀
        Loop % dangerList.MaxIndex(){
            if (RegExMatch(clip,dangerList[A_Index]) !=0 and RegExMatch(clip,"已腐化")==0){
                tempArray.Insert(dangerArray[index])
            }
        }
         RegExMatch(clip, "物品数量: \+(.*?)%", SubPats)
            if (quantity>SubPats1){
                tempArray.Insert(dangerArray[A_Index])
            }
    }
    ;重铸
    if (tempArray.MaxIndex()>=1){
        Click 761,118 left ;左键点击通货页
        Click 435,510 right ;右键点击重铸石
        Click 752,142 left ;左键点击地图页
        Send {Shift Down} 
        Loop % tempArray.MaxIndex(){
            x := tempArray[A_Index][1]
            y := tempArray[A_Index][2]
            Click %x% %y% left
        }
        Send {Shift Up}
    }
    ;点金
    if (tempArray.MaxIndex()>=1){
        Click 761,118 left ;左键点击通货页
        Click 493,272 right ;右键点击点金石
        Click 752,142 left ;左键点击地图页
        Send {Shift Down} 
        Loop % tempArray.MaxIndex(){
            x := tempArray[A_Index][1]
            y := tempArray[A_Index][2]
            Click %x% %y% left
            Sleep 100
        }
        Send {Shift Up}
    }
    if (tempArray.MaxIndex()>=1){
       return Filter(quantity,dangerList,tempArray)
    }
}
Map_Copy(lastClip,MaxIndex){
    MaxIndex--
    Send ^{c}
    Sleep 100
    currClip := clipboard 
    if (StrLen(currClip)!=StrLen(lastClip)) or (MaxIndex == 0) {
        return currClip
    }else{
        Sleep 100
        return Map_Copy(lastClip,MaxIndex)
    }
}
GetPos(){
    posArry :=[]
    _pos :=[40,156]
    _base := 53
    Loop, 12 {
        x := _pos[1] + _base * (A_Index -1)
            Loop, 12 {
            y := _pos[2] + _base * (A_Index -1)
            posArry.Insert([x,y])
        }
    }
    return posArry
}
PixSearch(posArray){
    mapPosArray := []
    Loop % posArray.MaxIndex(){
        x := posArray[A_Index][1]
        y := posArray[A_Index][2]
        _color := GetPosColor(30,142)
        ;mousemove x-20,y-20
        ;Sleep, 2000
        ;mousemove x+20,y+20                         0x45474D
        PixelSearch, Px, Py, x-20, y-20, x+20, y+20, 0x181614 ,3 ,Fast
        if (!ErrorLevel){
             mapPosArray.Insert([x,y])
        }          
    }
    ;MsgBox, pos search end!
    return mapPosArray
}
;日志
RZPixSearch(posArray){
    mapPosArray := []
    Loop % posArray.MaxIndex(){
        x := posArray[A_Index][1]
        y := posArray[A_Index][2]
        _color := GetPosColor(30,142)
        ;mousemove x-20,y-20
        ;Sleep, 2000
        ;mousemove x+20,y+20
        PixelSearch, Px, Py, x-20, y-20, x+20, y+20, 0x1F1F1F ,3 ,Fast
        if (!ErrorLevel){
             mapPosArray.Insert([x,y])
        }          
    }
    ;MsgBox, pos search end!
    return mapPosArray
}
GetPosColor(x,y){
    PixelGetColor, color, x, y
    return %color%
}


F12::ExitApp  ; Exit script with Escape key