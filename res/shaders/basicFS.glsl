#version 330 core

in vec3 position1;
in vec2 textureCoordinates1;
in vec3 normal1;

layout (location = 0) out vec4 color;

uniform int useTexture;

uniform vec4 baseColor;
uniform sampler2D activeTexture;

uniform float ambientLight;

void main()
{
	
	vec4 outputColor = baseColor;
	vec4 outputTexture = texture(activeTexture, textureCoordinates1);

	vec4 light = vec4(ambientLight, ambientLight, ambientLight, 1);

	if(useTexture == 1)
		color = light * outputColor * outputTexture;
	else
		color = light * outputColor;
	
}