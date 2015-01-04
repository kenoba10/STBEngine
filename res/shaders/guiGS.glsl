#version 330 core

layout (triangles) in;

layout (triangle_strip) out;
layout (max_vertices = 3) out;

in Vertex
{
	
	vec2 position;
	vec2 textureCoordinates;

} vertices[];

out Vertex
{

	vec2 position;
	vec2 textureCoordinates;

} vertex;

void main()
{
	
	for(int i = 0; i < gl_in.length(); i++)
	{
		
		gl_Position = gl_in[i].gl_Position;

		vertex.position = vertices[i].position;
		vertex.textureCoordinates = vertices[i].textureCoordinates;

		EmitVertex();

	}

	EndPrimitive();

}