//Simple instance shader
//Author: NMCG
//Version: 1.0
//Issues: None
//Modified: 6/9/22

#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix World;
matrix ViewProjection;

struct VSInput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
};

struct VertexShaderInstanceInput
{
	float4 row1 : TEXCOORD0;
	float4 row2 : TEXCOORD1;
	float4 row3 : TEXCOORD2;
	float4 row4 : TEXCOORD3;
};

struct VSOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
};

float4x4 CreateMatrixFromCols(
	float4 col1, float4 col2, float4 col3, float4 col4)
{
	return float4x4(
		col1.x, col2.x, col3.x, col4.x,
		col1.y, col2.y, col3.y, col4.y,
		col1.z, col2.z, col3.z, col4.z,
		col1.w, col2.w, col3.w, col4.w);
}

VSOutput InstancedMainVS(in VSInput input, VertexShaderInstanceInput instanceInput)
{
	VSOutput output = (VSOutput)0;

	float4x4 WorldInstance = CreateMatrixFromCols(
		instanceInput.row1,
		instanceInput.row2,
		instanceInput.row3,
		instanceInput.row4);

	output.Position = mul(mul(input.Position, transpose(WorldInstance)), ViewProjection);
	output.Color = input.Color;
	return output;
}

float4 MainPS(VSOutput input) : COLOR
{
	return input.Color;
}

technique DrawInstanced
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL InstancedMainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};