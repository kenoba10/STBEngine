#version 330 core

layout (triangles) in;

layout (triangle_strip) out;
layout (max_vertices = 3) out;

in Vertex
{
	
	vec3 position;
	vec2 textureCoordinates;
	vec3 normal;

} vertices[];

out Vertex
{

	vec3 position;
	vec2 textureCoordinates;
	vec3 normal;

} vertex;

void main()
{
	
	for(int i = 0; i < gl_in.length(); i++)
	{
		
		gl_Position = gl_in[i].gl_Position;

		vertex.position = vertices[i].position;
		vertex.textureCoordinates = vertices[i].textureCoordinates;
		vertex.normal = vertices[i].normal;

		EmitVertex();

	}

	EndPrimitive();

}