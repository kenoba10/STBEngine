#version 330 core

const int MAX_DIRECTIONAL_LIGHTS = 8;

in vec2 textureCoordinates1;
in vec3 normal1;

layout (location = 0) out vec4 color;

struct BaseLight
{

	vec4 color;
	float intensity;

};

struct DirectionalLight
{

	BaseLight base;
	vec3 direction;

};

uniform int useTexture;

uniform vec4 baseColor;
uniform sampler2D activeTexture;

uniform float ambientLight;
uniform DirectionalLight directionalLights[MAX_DIRECTIONAL_LIGHTS];

vec4 calculateLight(BaseLight light, vec3 direction, vec3 normal)
{

	vec4 diffuseColor = vec4(0, 0, 0, 0);
	float diffuseFactor = dot(normal, direction);

	if(diffuseFactor > 0)
		diffuseColor = vec4(light.color) * light.intensity * diffuseFactor;

	return diffuseColor;

}

vec4 calculateDirectionalLight(DirectionalLight light, vec3 normal)
{

	return calculateLight(light.base, light.direction, normal);

}

void main()
{
	
	vec4 outputColor = baseColor;
	vec4 outputTexture = texture(activeTexture, textureCoordinates1);

	vec4 light = vec4(ambientLight, ambientLight, ambientLight, 1);

	if(directionalLights[0].base.intensity > 0)
		light += calculateDirectionalLight(directionalLights[0], normal1);

	if(directionalLights[1].base.intensity > 0)
		light += calculateDirectionalLight(directionalLights[1], normal1);

	if(directionalLights[2].base.intensity > 0)
		light += calculateDirectionalLight(directionalLights[2], normal1);

	if(directionalLights[3].base.intensity > 0)
		light += calculateDirectionalLight(directionalLights[3], normal1);

	if(directionalLights[4].base.intensity > 0)
		light += calculateDirectionalLight(directionalLights[4], normal1);

	if(directionalLights[5].base.intensity > 0)
		light += calculateDirectionalLight(directionalLights[5], normal1);

	if(directionalLights[6].base.intensity > 0)
		light += calculateDirectionalLight(directionalLights[6], normal1);

	if(directionalLights[7].base.intensity > 0)
		light += calculateDirectionalLight(directionalLights[7], normal1);

	if(useTexture == 1)
		color = light * outputColor * outputTexture;
	else
		color = light * outputColor;
	
}