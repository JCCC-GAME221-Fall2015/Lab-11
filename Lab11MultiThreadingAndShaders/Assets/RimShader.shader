Shader "RimShader" //Name
{
	Properties //prop.s block so user can adjust on interface
	{
		_Color("Color",Color) = (1.0,1.0,1.0,1.0)
		_SpecColor("Specular Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Float) = 10
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimPower("Rim Power",Range(0.1,10)) = 3.0
	}
	SubShader
	{
		Pass
		{
			Tags {"LightMode" = "ForwardBase"}
			
			CGPROGRAM
			#pragma vertex vertexProgram
			#pragma fragment fragmentProgram
			
			//user defined variables
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;
			uniform float4 _RimColor;
			uniform float _RimPower;
			//Unity defined Variables
			uniform float4 _LightColor0;
			
			struct vertexInput
			{
				float4 vertex :POSITION;
				float3 normal :NORMAL;
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
				//Lighting
				float3 ambientLight = UNITY_LIGHTMODEL_AMBIENT.rgb;
				//multiple lines for diffuse lighting
				float3 diffuseLighting = dot(normalDirection, lightDirection);
				diffuseLighting = max(0.0, diffuseLighting);
				diffuseLighting = float3(_LightColor0.rgb) * diffuseLighting;
				diffuseLighting = attenuation * diffuseLighting;
				//multiple lines for diffuse lighting
				float3 specularLighting = reflect(-lightDirection, normalDirection);
				specularLighting = dot(specularLighting, viewDirection);
				specularLighting = max(0.0, specularLighting);
				specularLighting = pow(specularLighting, _Shininess);
				//multiple lines for diffuse lighting
				float3 specularColouring = dot(normalDirection, lightDirection);
				specularColouring = max(0.0, specularColouring);
				specularColouring = attenuation * specularColouring;
				specularColouring = float3(_SpecColor.rgb) * specularColouring;

				specularLighting = specularLighting * specularColouring;
				
				//Rim Lighting
				float actualRim = 1 - saturate(dot(normalize(viewDirection), normalDirection));
				float4 rimLighting = attenuation * _LightColor0.rgb * _RimColor * 
					saturate(dot(normalDirection, lightDirection)) * pow(actualRim, _RimPower);
				
				//Final Lighting
				//float3 finalLight = (ambientLight + diffuseLighting + specularLighting) * float3(_Color.rgb);
				float3 finalLight = rimLighting;
				
				//Test Lighting
				
				return float4(finalLight, 1.0);
			}
			
			ENDCG
		}
	}
}