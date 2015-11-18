Shader "RimShader" 
{
	Properties
	{
		// Interfaces for the unity inspector
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecColor("Specular Color", Color) = (1, 1, 1, 1)
		_Shininess("Shininess", Float) = 10
		_RimColor("Rim Color", Color) = (1, 1, 1, 1)
		_RimPower("Rim Power", Range(0.1, 10)) = 3.0
	}
		SubShader
	{
		Pass
		{
			Tags { "LightMode" = "ForwardBase"}

			CGPROGRAM
			#pragma vertex vertexProgram
			#pragma fragment fragmentProgram

			// user defined variables
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform float4 _RimColor;
			uniform float _RimPower;

			// Unity defined variables
			uniform float4 _LightColor0;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
			};

			vertexOutput vertexProgram(vertexInput input)
			{
				vertexOutput output;

				output.posWorld = mul(_Object2World, input.vertex);
				output.normalDir = normalize(float3(mul(float4(input.normal, 0.0), _World2Object).xyz));
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);

				return output;
			}

			float4 fragmentProgram(vertexOutput input) : COLOR
			{
				float3 normalDirection = normalize(input.normalDir);
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - float3(input.posWorld.xyz));
				float3 lightDirection;
				float attenuation = 1.0;
				lightDirection = normalize(float3(_WorldSpaceLightPos0.xyz));

				// Lighting
				float3 ambientLight = UNITY_LIGHTMODEL_AMBIENT.rgb;

				// Lighting - Diffuse
				float3 diffuseLighting = attenuation * float3(_LightColor0.rgb) * max(0.0, dot(normalDirection, lightDirection));

				// Lighting - Specular
				float3 specularLighting = max(0.0, dot(normalDirection, lightDirection)) * attenuation * float3(_LightColor0.rgb) * 
					float3(_SpecColor.rgb) * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);

				// Specular Coloring
				float3 specularColoring = dot(normalDirection, lightDirection);
				specularColoring = max(0.0, specularColoring);
				specularColoring = attenuation * specularColoring;
				specularColoring = float3(_SpecColor.rgb) * specularColoring;
				specularLighting = specularColoring * specularLighting;

				// Lighting - Rim
				float actualRim = 1 - saturate(dot(normalize(viewDirection), normalDirection));
				float3 rimLighting = attenuation * _LightColor0.rgb * _RimColor * saturate(dot(normalDirection, lightDirection)) * pow(actualRim, _RimPower);

				// Lighting - Final
				float3 finalLight = rimLighting + specularLighting + diffuseLighting;

				// Test Lighting
				return float4(finalLight, 1.0);

			}

			ENDCG
		}
	}

	FallBack "Diffuse"
}
