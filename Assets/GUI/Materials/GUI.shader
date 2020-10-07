Shader "Custom/GUI"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _MainTex;
    half4 _Color;

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
        return tex2D(_MainTex, uv) * _Color;
    }

    ENDCG

    SubShader
    {
        Tags { "Queue"="Transparent" }
        Pass
        {
            ZWrite Off Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
            ENDCG
        }
    }
}
