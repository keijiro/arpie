Shader "Custom/Unlit Color" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    Subshader {
        Tags { "RenderType"="Opaque" }
        Pass {
            GLSLPROGRAM

            uniform lowp vec4 _Color;

            #ifdef VERTEX
            void main() {
                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
            }
            #endif

            #ifdef FRAGMENT
            void main() {
                gl_FragColor = _Color;
            }
            #endif

            ENDGLSL
        }
    }
}
