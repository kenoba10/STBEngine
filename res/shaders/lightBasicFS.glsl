#version 330 core

#include "utils.glsl"

layout (location = 0) out vec4 color;

in Vertex
{
	
	vec3 position;
	vec2 textureCoordinates;
	vec3 normal;
	mat3 tbnMatrix;

} vertex;

uniform vec3 eyePosition;

uniform float displacementScale;
uniform float displacementBias;

uniform int useDisplacementMap;
uniform sampler2D displacementMap;

uniform int useTexture;

uniform vec4 baseColor;
uniform sampler2D activeTexture;

uniform float ambientLight;

void main()
{
	
	color = baseColor;
	
	color *= vec4(ambientLight, ambientLight, ambientLight, 1);
	
	vec2 textureCoordinates;
	
	if(useDisplacementMap == 1)
		textureCoordinates = calculateTextureCoordinates(eyePosition, vertex.position, vertex.textureCoordinates, vertex.tbnMatrix, displacementScale, displacementBias, displacementMap);
	else
		textureCoordinates = vertex.textureCoordinates;
	
	if(useTexture == 1)
		color *= texture(activeTexture, textureCoordinates);
	
}
