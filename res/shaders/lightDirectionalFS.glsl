#version 330 core

#include "lighting.glsl"

layout (location = 0) out vec4 color;

in Vertex
{
	
	vec3 position;
	vec2 textureCoordinates;
	vec3 normal;

} vertex;

uniform DirectionalLight light;

void main()
{
	
	color = calculateDirectionalLight(light, vertex.position, vertex.normal);
	
}
