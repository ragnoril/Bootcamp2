Shader "Bootcamp/FirstShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DissolveTex("Dissolve Texture", 2D) = "white" {}
        _DissolveCutoff("Dissolve Cutoff", Range(0, 1)) = 0

        _ExtrudeAmount ("Extrude Amount", float) = 0
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ExtrudeAmount;
            float4 _Color;

            sampler2D _DissolveTex;
            float _DissolveCutoff;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz += v.normal.xyz * _ExtrudeAmount;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                float4 texColor = tex2D(_MainTex, i.uv);
                float4 dissolveColor = tex2D(_DissolveTex, i.uv);
                clip(dissolveColor.rgb - _DissolveCutoff);

                fixed4 col = texColor * _Color;

                return col;
            }
            ENDCG
        }
    }
}
