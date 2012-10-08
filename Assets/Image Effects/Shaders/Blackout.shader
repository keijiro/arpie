Shader "Hidden/Blackout" {
	Properties {
		_MainTex ("Base", 2D) = "" {}
	}
	Subshader {
		Pass {
			Cull Off
			ZTest Off
			ZWrite Off
			Fog { Mode off }

			GLSLPROGRAM

			uniform sampler2D _MainTex;
			uniform lowp float level;
			varying lowp vec2 uv;

			#ifdef VERTEX
			void main() {
	            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
				uv = gl_MultiTexCoord0.xy;
			}
			#endif

			#ifdef FRAGMENT
			void main() {
				gl_FragColor = max(texture2D(_MainTex, uv) - level, 0.0);
			}
			#endif

			ENDGLSL
		}
	}
}
