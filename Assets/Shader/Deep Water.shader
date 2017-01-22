Shader "Unlit/Deep Water"
{
	Properties
	{
		_Col ("Color", Color) = (1,1,1,0)
		_Alpha("Aplha", Float) = 0.5

	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			float _Alpha;
			float4 _Col;

			float4 vert (float4 v : POSITION) : SV_POSITION
			{
				float4 o;
				o = mul(UNITY_MATRIX_MVP, v);
				return o;
			}

			//Todo: deeper = weniger zu sehen

			fixed4 frag (float4 i : SV_POSITION) : COLOR
			{
				fixed4 o = _Col;
				o.a = _Alpha;
				return o;
			}
			ENDCG
		}
	}
}
