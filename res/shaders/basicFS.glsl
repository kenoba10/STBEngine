#version 330 core

in vec2 textureCoordinates1;

layout (location = 0) out vec4 color;

uniform sampler2D activeTexture;

void main()
{
	
	color = texture(activeTexture, textureCoordinates1);
	
}