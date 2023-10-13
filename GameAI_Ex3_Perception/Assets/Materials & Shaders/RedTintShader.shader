Shader "Custom/RedTintShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TintStrength ("Tint Strength", Range(0, 1)) = 1
    }
 
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float _TintStrength;
 
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord);
                col.r = col.r * _TintStrength;
                col.g = 0;
                col.b = 0;
                return col;
            }
            ENDCG
        }
    }
}
