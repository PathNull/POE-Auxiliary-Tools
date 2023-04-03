F2::
MouseGetPos, MouseX, MouseY
PixelGetColor, color, %MouseX%, %MouseY%
MsgBox The color at the current cursor position is %color%.
return