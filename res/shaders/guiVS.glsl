#version 330 core

layout (location = 0) in vec2 position;
layout (location = 1) in vec2 textureCoordinates;

out Vertex
{
	
	vec2 position;
	vec2 textureCoordinates;

} vertex;

uniform float depth;

uniform mat4 projection;

void main()
{
	
	gl_Position = projection * vec4(position, depth, 1);
	
	vertex.position = position;
	vertex.textureCoordinates = textureCoordinates;
	
}