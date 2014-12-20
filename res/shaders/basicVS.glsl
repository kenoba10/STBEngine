#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec2 textureCoordinates;

out vec2 textureCoordinates1;

uniform mat4 projection;
uniform mat4 transformation;

void main()
{
	
	gl_Position = projection * transformation * vec4(position, 1);
	textureCoordinates1 = textureCoordinates;
	
}