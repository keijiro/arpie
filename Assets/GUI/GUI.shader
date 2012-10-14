Shader "Custom/GUI" {
	Properties {
		_MainTex ("Base", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "Queue"="Transparent" }
		Pass {
			ZWrite Off
			Fog { Mode off }
			Blend SrcAlpha OneMinusSrcAlpha

			GLSLPROGRAM

			uniform sampler2D _MainTex;
			uniform lowp vec4 _Color;
			varying lowp vec2 uv;

			#ifdef VERTEX
			void main() {
	            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
				uv = gl_MultiTexCoord0.xy;
			}
			#endif

			#ifdef FRAGMENT
			void main() {
				gl_FragColor = texture2D(_MainTex, uv) * _Color;
			}
			#endif

			ENDGLSL
		}
	} 
}
