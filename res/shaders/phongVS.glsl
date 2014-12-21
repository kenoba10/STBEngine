#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec2 textureCoordinates;
layout (location = 2) in vec3 normal;

out vec2 textureCoordinates1;
out vec3 normal1;

uniform mat4 projection;
uniform mat4 transformation;

void main()
{
	
	gl_Position = projection * transformation * vec4(position, 1);
	textureCoordinates1 = textureCoordinates;
	normal1 = (transformation * vec4(normal, 0)).xyz;
	
}