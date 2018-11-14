#version 440 core
#define WORK_GROUP_SIZE 128

layout(local_size_x = WORK_GROUP_SIZE, local_size_y = 1, local_size_z = 1) in;

layout(std430, binding = 0) buffer positionBuffer{vec4 position[]; };
layout(std430, binding = 1) buffer velocityBuffer{vec4 velocity[]; };
layout(std430, binding = 2) buffer initVelocityBuffer{vec4 initVelocity[]; };

uniform mat4 vp;


void main()
{
	uint i = gl_GlobalInvocationID.x;

	vec3 gravity = vec3(0.0f, -9.8 * .0167f, 0.0f);
	velocity[i].xyz += gravity;

	position[i].xyz += velocity[i].xyz;
	position[i].w -= 2.5 * 0.0167f;

	if (position[i].w <= 0.0f)
	{
		position[i].xyzw = vec4(0.0f, 10.0f, 0.0f, initVelocity[i].w);
		velocity[i] = initVelocity[i];
	}
}