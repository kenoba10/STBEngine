#version 330 core

const int MAX_DIRECTIONAL_LIGHTS = 8;
const int MAX_POINT_LIGHTS = 8;
const int MAX_SPOT_LIGHTS = 8;

in vec3 position1;
in vec2 textureCoordinates1;
in vec3 normal1;

layout (location = 0) out vec4 color;

struct BaseLight
{

	vec4 color;
	float intensity;

};

struct Attenuation
{
	
	float constant;
	float linear;
	float exponent;

};

struct DirectionalLight
{

	BaseLight base;
	vec3 direction;

};

struct PointLight
{

	BaseLight base;
	Attenuation attenuation;
	vec3 position;
	float range;

};

struct SpotLight
{

	PointLight base;
	vec3 direction;
	float cutoff;

};

uniform vec3 eyePosition;

uniform int useTexture;

uniform vec4 baseColor;
uniform sampler2D activeTexture;

uniform float ambientLight;
uniform DirectionalLight directionalLights[MAX_DIRECTIONAL_LIGHTS];
uniform PointLight pointLights[MAX_POINT_LIGHTS];
uniform SpotLight spotLights[MAX_SPOT_LIGHTS];

uniform float specularIntensity;
uniform float specularExponent;

vec4 calculateLight(BaseLight light, vec3 direction, vec3 normal)
{

	float diffuseFactor = dot(normal, direction);

	vec4 diffuseColor = vec4(0, 0, 0, 0);
	vec4 specularColor = vec4(0, 0, 0, 0);

	if(diffuseFactor > 0)
	{
		
		diffuseColor = vec4(light.color) * light.intensity * diffuseFactor;

		vec3 directionToEye = normalize(eyePosition - position1);
		vec3 directionToReflect = normalize(reflect(direction, normal));

		float specularFactor = pow(dot(directionToEye, directionToReflect), specularExponent);

		if(specularFactor > 0)
			specularColor = vec4(light.color) * specularIntensity * specularFactor;


	}

	return diffuseColor + specularColor;

}

vec4 calculateDirectionalLight(DirectionalLight light, vec3 normal)
{

	return calculateLight(light.base, light.direction, normal);

}

vec4 calculatePointLight(PointLight light, vec3 normal)
{

	vec3 direction = position1 - light.position;
	float distance = length(direction);

	direction = normalize(direction);

	if(distance > light.range)
		return vec4(0, 0, 0, 0);

	vec4 color = calculateLight(light.base, direction, normal);

	float attenuation = light.attenuation.constant + light.attenuation.linear * distance + light.attenuation.exponent * distance * distance + 0.0001;

	return color / attenuation;

}

vec4 calculateSpotLight(SpotLight light, vec3 normal)
{

	vec3 direction = normalize(position1 - light.base.position);
	float factor = dot(direction, light.direction);

	vec4 color = vec4(0, 0, 0, 0);

	if(factor > light.cutoff)
	{
		
		color = calculatePointLight(light.base, normal) * (1 - (1 - factor) / (1 - light.cutoff));

	}

	return color;

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

	if(pointLights[0].base.intensity > 0)
		light += calculatePointLight(pointLights[0], normal1);

	if(pointLights[1].base.intensity > 0)
		light += calculatePointLight(pointLights[1], normal1);

	if(pointLights[2].base.intensity > 0)
		light += calculatePointLight(pointLights[2], normal1);

	if(pointLights[3].base.intensity > 0)
		light += calculatePointLight(pointLights[3], normal1);

	if(pointLights[4].base.intensity > 0)
		light += calculatePointLight(pointLights[4], normal1);

	if(pointLights[5].base.intensity > 0)
		light += calculatePointLight(pointLights[5], normal1);

	if(pointLights[6].base.intensity > 0)
		light += calculatePointLight(pointLights[6], normal1);

	if(pointLights[7].base.intensity > 0)
		light += calculatePointLight(pointLights[7], normal1);

	if(spotLights[0].base.base.intensity > 0)
		light += calculateSpotLight(spotLights[0], normal1);

	if(spotLights[1].base.base.intensity > 0)
		light += calculateSpotLight(spotLights[1], normal1);

	if(spotLights[2].base.base.intensity > 0)
		light += calculateSpotLight(spotLights[2], normal1);

	if(spotLights[3].base.base.intensity > 0)
		light += calculateSpotLight(spotLights[3], normal1);

	if(spotLights[4].base.base.intensity > 0)
		light += calculateSpotLight(spotLights[4], normal1);

	if(spotLights[5].base.base.intensity > 0)
		light += calculateSpotLight(spotLights[5], normal1);

	if(spotLights[6].base.base.intensity > 0)
		light += calculateSpotLight(spotLights[6], normal1);

	if(spotLights[7].base.base.intensity > 0)
		light += calculateSpotLight(spotLights[7], normal1);

	if(useTexture == 1)
		color = light * outputColor * outputTexture;
	else
		color = light * outputColor;
	
}