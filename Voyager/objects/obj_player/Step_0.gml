if fuel > 0
{
	if keyboard_check(vk_right){
		image_angle -= 5;
		fuel--;
	}
	else if keyboard_check(vk_left){
		image_angle += 5;
		fuel--;
	}
	else if keyboard_check(vk_up){
		motion_add(image_angle, .05);
		fuel-=5;
	}
	else if keyboard_check(vk_space){
		speed = 0;
		v_flag = false;
		fuel -=20;
	}
}

velocity = speed;

if speed > 5{
	speed = 5;
}
move_wrap(true, true, sprite_width/2);