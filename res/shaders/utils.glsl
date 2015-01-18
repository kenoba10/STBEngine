vec2 calculateTextureCoordinates(vec3 eyePosition, vec3 position, vec2 textureCoordinates, mat3 tbnMatrix, float scale, float bias, sampler2D map)
{
	
	return textureCoordinates + (normalize(eyePosition - position) * tbnMatrix).xy * (texture(map, textureCoordinates).x * scale + bias);
	
}

vec3 calculateNormal(vec2 textureCoordinates, mat3 tbnMatrix, sampler2D map)
{
	
	return tbnMatrix * (255.0 / 128.0 * texture(map, textureCoordinates).xyz - 1);
	
}
