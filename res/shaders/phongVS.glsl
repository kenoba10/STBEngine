#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec2 textureCoordinates;
layout (location = 2) in vec3 normal;

out Vertex
{
	
	vec3 position;
	vec2 textureCoordinates;
	vec3 normal;

} vertex;

uniform mat4 projection;
uniform mat4 transformation;

void main()
{
	
	gl_Position = projection * transformation * vec4(position, 1);

	vertex.position = (transformation * vec4(position, 1)).xyz;
	vertex.textureCoordinates = textureCoordinates;
	vertex.normal = (transformation * vec4(normal, 0)).xyz;
	
}