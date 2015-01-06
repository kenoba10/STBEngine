struct Attenuation
{
	
	float constant;
	float linear;
	float exponent;

};

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

uniform float specularIntensity;
uniform float specularExponent;

vec4 calculateLight(BaseLight light, vec3 direction, vec3 position, vec3 normal)
{

	float diffuseFactor = dot(normal, direction);

	vec4 diffuseColor = vec4(0, 0, 0, 0);
	vec4 specularColor = vec4(0, 0, 0, 0);

	if(diffuseFactor > 0)
	{
		
		diffuseColor = vec4(light.color) * light.intensity * diffuseFactor;

		vec3 directionToEye = normalize(eyePosition - position);
		vec3 directionToReflect = normalize(reflect(direction, normal));

		float specularFactor = pow(dot(directionToEye, directionToReflect), specularExponent);

		if(specularFactor > 0)
			specularColor = vec4(light.color) * specularIntensity * specularFactor;


	}

	return diffuseColor + specularColor;

}

vec4 calculateDirectionalLight(DirectionalLight light, vec3 position, vec3 normal)
{

	return calculateLight(light.base, light.direction, position, normal);

}

vec4 calculatePointLight(PointLight light, vec3 position, vec3 normal)
{

	vec3 direction = position - light.position;
	float distance = length(direction);

	direction = normalize(direction);

	if(distance > light.range)
		return vec4(0, 0, 0, 0);

	vec4 color = calculateLight(light.base, direction, position, normal);

	float attenuation = light.attenuation.constant + light.attenuation.linear * distance + light.attenuation.exponent * distance * distance + 0.0001;

	return color / attenuation;

}

vec4 calculateSpotLight(SpotLight light, vec3 position, vec3 normal)
{

	vec3 direction = normalize(position - light.base.position);
	float factor = dot(direction, light.direction);

	vec4 color = vec4(0, 0, 0, 0);

	if(factor > light.cutoff)
	{
		
		color = calculatePointLight(light.base, position, normal) * (1 - (1 - factor) / (1 - light.cutoff));

	}

	return color;

}
