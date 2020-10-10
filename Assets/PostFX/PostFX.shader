Shader "Hidden/PostFX"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _MainTex;
    half _Vignette;
    half _Fading;

    void Vertex(float4 vertex : POSITION,
                float2 uv : TEXCOORD0,
                out float4 outVertex : SV_Position,
                out float2 outUV : TEXCOORD0)
    {
        outVertex = UnityObjectToClipPos(vertex);
        outUV = uv;
    }

    half4 Fragment(float4 vertex : SV_Position,
                   float2 uv : TEXCOORD0) : SV_Target
    {
        half4 src = tex2D(_MainTex, uv);
        half amp = 1 - length(uv - 0.5) * _Vignette;
        return lerp(src * amp, 0, _Fading);
    }

    ENDCG

    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
            ENDCG
        }
    }
}
