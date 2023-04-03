LockWindow(){
    WinActivate, ahk_exe PathOfExile_x64.exe
}

ClickGZ()
{
    Click 108,279 right ;右键点击改造石
    Click 332,459 left ;左键点击装备

}
ClickHD()
{
    Click 547,269 right ;右键点击混沌石
    Click 332,459 left ;左键点击装备

}
AddStone(){
     Click 224,318 right ;右键点击增幅石
     Click 332,459 left ;左键点击装备
}
Copy(){
    Random, randX, -37, 37
    Random, randY, -44, 44
    x :=  332 + randX
    y :=  459 + randY
    mousemove, x,y 
    Send ^{c}
}

