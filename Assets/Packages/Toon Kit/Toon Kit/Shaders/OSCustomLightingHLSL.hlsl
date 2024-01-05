#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

half4 GetShadowCoord(half3 WorldPos)
{
#ifndef SHADERGRAPH_PREVIEW
#if SHADOWS_SCREEN
    half4 clipPos = TransformWorldToHClip(RayPos);
    half4 shadowCoord = ComputeScreenPos(clipPos);
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
	return shadowCoord;
#endif
}

void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten)
{
#ifdef SHADERGRAPH_PREVIEW
    Direction = float3(0.5, 0.5, 0);
    Color = 1;
    DistanceAtten = 1;
    ShadowAtten = 1;
#else
	half4 shadowCoord = GetShadowCoord(WorldPos);
    
	Light mainLight = GetMainLight(shadowCoord);
	Direction = normalize(mainLight.direction);
	Color = mainLight.color;
	DistanceAtten = mainLight.distanceAttenuation;
	ShadowAtten = mainLight.shadowAttenuation;
#endif
}

void AdditionalLights_float(float3 WorldPosition, float3 WorldNormal, float3 WorldView, out float3 Diffuse)
{
    float3 diffuseColor = 0;
#ifdef SHADERGRAPH_PREVIEW
	Diffuse = 0;
#else
    WorldNormal = normalize(WorldNormal);
    WorldView = SafeNormalize(WorldView);
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i)
    {
        Light light = GetAdditionalLight(i, WorldPosition);
        half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
        diffuseColor += LightingLambert(attenuatedLightColor, light.direction, WorldNormal);
    }
#endif
    Diffuse = diffuseColor;
}
void GetAO_half(float2 InputUV, out half IndirectAO, out half DirectAO) {
#ifdef SHADERGRAPH_PREVIEW
	IndirectAO = 1;
	DirectAO = 1;
	#else
	#if defined(_SCREEN_SPACE_OCCLUSION)
		AmbientOcclusionFactor aoFactor = GetScreenSpaceAmbientOcclusion(InputUV);
		IndirectAO = aoFactor.indirectAmbientOcclusion;
		DirectAO = aoFactor.directAmbientOcclusion;
	#else
		IndirectAO = 1;
		DirectAO = 1;
	#endif
#endif
}

#endif