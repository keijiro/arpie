Shader "Custom/Arpie"
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

        struct Input { half param; };

        void vert(inout appdata_full v, out Input o)
        {
            o.param = v.texcoord.x;
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _Color.rgb * (IN.param < 0.5);
            o.Alpha = _Color.a;
            o.Emission = IN.param > 0.75;
        }

        ENDCG
    }
}
