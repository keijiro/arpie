Shader "Custom/Gradated Diffuse"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM

        #pragma surface surf Lambert vertex:vert

        half4 _Color;

        struct Input { half4 color; };

        void vert(inout appdata_full v, out Input o)
        {
            o.color = lerp(0, _Color, (v.vertex.y + 1) / 2);
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = IN.color.rgb;
            o.Alpha = _Color.a;
        }

        ENDCG
    }
}
