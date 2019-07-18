/// @description Insert description here
// You can write your code in this editor
if keyboard_check(vk_right){
	image_angle = 0;
}
else if keyboard_check(vk_left){
	image_angle=180;
}
else if keyboard_check(vk_up){
	image_angle=90;
}
else if keyboard_check(vk_down){
	image_angle=270;
}

if keyboard_check(vk_space)
{
	motion_add(image_angle, .05);
}
move_wrap(true, true, sprite_width/2);