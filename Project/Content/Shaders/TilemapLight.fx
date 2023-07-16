#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

extern Texture2D SpriteTexture;
extern Texture2D LightTexture;

float4 KeyColor1;
float4 KeyColor2;
float4 NewColor1;
float4 NewColor2;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

sampler2D LightTextureSampler = sampler_state
{
	Texture = <LightTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 rt_color = tex2D(SpriteTextureSampler,input.TextureCoordinates);
	float4 lightColor = tex2D(LightTextureSampler,input.TextureCoordinates);

	bool iskeyColor1 = rt_color.r == KeyColor1.r && rt_color.g == KeyColor1.g && rt_color.b == KeyColor1.b;
	bool iskeyColor2 = rt_color.r == KeyColor2.r && rt_color.g == KeyColor2.g && rt_color.b == KeyColor2.b;
	bool isLightColorNewColor = lightColor.r == NewColor1.r && lightColor.g == NewColor1.g && lightColor.b == NewColor1.b;

	if(iskeyColor1 || iskeyColor2)
	{
		rt_color = float4(0.0f, 0.0f, 0.0f, 1.0f);
		if(lightColor.a > 0.0f)
		{
			if(iskeyColor2) rt_color = NewColor2;
			if(iskeyColor1) rt_color =  NewColor1;
		}
	}

	return rt_color;
}

technique LightDrawing
{
	pass P0
	{
		AlphaBlendEnable = TRUE;
		DestBlend = INVSRCALPHA;
		SrcBlend = SRCALPHA;
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};