#version 330 core

layout (location = 0) out vec4 color;

in Vertex
{
	
	vec3 position;
	vec2 textureCoordinates;
	vec3 normal;

} vertex;

uniform int useTexture;

uniform vec4 baseColor;
uniform sampler2D activeTexture;

void main()
{
	
	vec4 outputColor = baseColor;
	vec4 outputTexture = texture(activeTexture, vertex.textureCoordinates);

	if(useTexture == 1)
		color = outputColor * outputTexture;
	else
		color = outputColor;
	
}