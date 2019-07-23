globalvar fuel;//beginning rocket fuel
globalvar velocity;//rocket's velocity
globalvar v_flag;//made v_flag global so player object can set false once space is hit in step
fuel = 100000;
velocity = 0;
v_flag = false;

v_margin = .2;//once velocity exceeds this number then the zero out velocity prompt can pop up

//create random stars
var i;
for(i = 0; i < 1000; i ++)
{
	instance_create_layer(irandom_range(100,1900),
	irandom_range(100,1900),"Instances",obj_star);
}

for(i = 0; i < 500; i++)
{
	instance_create_layer(irandom_range(100,1900),
	irandom_range(100,1900), "Instances", obj_asteroid0);
}

