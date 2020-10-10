Shader "Custom/BG"
{
    Properties
    {
        _MainTex("Texture", 2D) = "black"{}
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    Texture2D _MainTex;

    float4 Vertex(float4 vertex : POSITION) : SV_Position
    {
        return UnityObjectToClipPos(vertex);
    }

    float4 Fragment(float4 vertex : SV_Position) : SV_Target
    {
        uint w, h;
        _MainTex.GetDimensions(w, h);
        return _MainTex.Load(uint3(vertex.xy % uint2(w, h), 0));
    }

    ENDCG

    SubShader
    {
        Tags { "Queue"="Background"
               "RenderType"="Background"
               "PreviewType"="Skybox" }

        Cull Off ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
            ENDCG
        }
    }
}
