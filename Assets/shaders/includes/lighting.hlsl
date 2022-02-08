#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED
//#define MAX_DIRECTIONAL_LIGHT_COUNT 4

void CalculateMainLight_float(float3 WorldPos, out float3 Direction, out float3 Color)
{
#if defined(SHADERGRAPH_PREVIEW) //if shader defined in preview window, set this values as the position 
	Direction = float3(0.5, 0.5, 0);
	Color = 1;
#else
	Light mainLight = GetMainLight(1);
	Direction = mainLight.direction;
	Color = mainLight.color;


#endif
}
#endif