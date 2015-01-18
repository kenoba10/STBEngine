#version 330 core

layout (location = 0) out vec4 color;

in Vertex
{
	
	vec2 position;
	vec2 textureCoordinates;

} vertex;

uniform int useTexture;

uniform vec4 baseColor;
uniform sampler2D activeTexture;

void main()
{
	
	color = baseColor;
	
	if(useTexture == 1)
		color *= texture(activeTexture, vertex.textureCoordinates);
	
}
