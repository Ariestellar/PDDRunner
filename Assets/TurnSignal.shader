Shader "Shields/Transparent"
{
	Properties
	{
		_ShieldIntensity("Shield Intensity", Range(0,1)) = 1.0
		_ShieldColor("Shield Color", Color) = (1, 0, 0, 1)
		_MainTex ("Transparency Mask", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
                                float3 normal: NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float4 _ShieldColor;
			
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			float _ShieldIntensity;
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 transparencyMask = tex2D(_MainTex, i.uv);
				return fixed4(_ShieldColor.r, _ShieldColor.g, _ShieldColor.b, _ShieldIntensity * transparencyMask.r);
			}
			ENDCG
		}
	}
}