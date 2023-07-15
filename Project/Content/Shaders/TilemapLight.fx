#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
Texture2D LightTexture;

float4 KeyColor;
float4 NewColor;

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
	input.Position.y = 50.0f;
	float4 rt_color = tex2D(SpriteTextureSampler,input.TextureCoordinates);
	float4 lightColor = tex2D(LightTextureSampler,input.TextureCoordinates);

	if(rt_color.r == KeyColor.r && rt_color.g == KeyColor.g && rt_color.b == KeyColor.b)
	{
		if(lightColor.a > 0.0f)
			rt_color =  NewColor;
		else
			rt_color = float4(0.0f, 0.0f, 0.0f, 0.0f);
	}
	

	return rt_color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};