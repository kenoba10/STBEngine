#version 330 core

#include "lighting.glsl"

layout (location = 0) out vec4 color;

in Vertex
{
	
	vec3 position;
	vec2 textureCoordinates;
	vec3 normal;

} vertex;

uniform SpotLight light;

void main()
{
	
	color = calculateSpotLight(light, vertex.position, vertex.normal);
	
}
