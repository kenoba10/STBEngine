#version 330 core

#include "lighting.glsl"
#include "utils.glsl"

layout (location = 0) out vec4 color;

in Vertex
{
	
	vec3 position;
	vec2 textureCoordinates;
	vec3 normal;
	mat3 tbnMatrix;

} vertex;

uniform float displacementScale;
uniform float displacementBias;

uniform int useDisplacementMap;
uniform sampler2D displacementMap;

uniform int useNormalMap;
uniform sampler2D normalMap;

uniform SpotLight light;

void main()
{

	vec3 position;
	vec2 textureCoordinates;
	vec3 normal;
	
	position = vertex.position;
	
	if(useDisplacementMap == 1)
		textureCoordinates = calculateTextureCoordinates(eyePosition, vertex.position, vertex.textureCoordinates, vertex.tbnMatrix, displacementScale, displacementBias, displacementMap);
	else
		textureCoordinates = vertex.textureCoordinates;
	
	if(useNormalMap == 1)
		normal = normalize(calculateNormal(textureCoordinates, vertex.tbnMatrix, normalMap));
	else
		normal = normalize(vertex.normal);
	
	color = calculateSpotLight(light, position, normal);
	
}
