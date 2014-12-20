#version 330 core

in vec2 textureCoordinates1;

layout (location = 0) out vec4 color;

uniform float useTexture;

uniform vec4 baseColor;
uniform sampler2D activeTexture;

void main()
{
	
	vec4 outputColor = baseColor;
	vec4 outputTexture = texture(activeTexture, textureCoordinates1);

	if(useTexture == 1)
		color = outputColor * outputTexture;
	else
		color = outputColor;
	
}