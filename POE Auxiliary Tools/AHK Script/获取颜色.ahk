F2::
MouseGetPos, MouseX, MouseY
PixelGetColor, color, %MouseX%, %MouseY%
MsgBox X: %MouseX% , Y:%MouseY%  %color%.
return