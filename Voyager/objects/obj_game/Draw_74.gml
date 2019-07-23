draw_text(20,20,"Fuel: "+string(fuel));
draw_text(20,40,"Velocity: "+string(velocity));

if velocity <= .2 and v_flag{
	draw_text(100,100,"Press space to zero out velocity");
}