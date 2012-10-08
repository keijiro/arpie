Shader "Hidden/Vignetting" {
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
			uniform sampler2D grad_texture;
			uniform lowp float vignette_intensity;

			varying lowp vec2 uv;

			#ifdef VERTEX
			void main() {
	            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
				uv = gl_MultiTexCoord0.xy;
			}
			#endif

			#ifdef FRAGMENT
			void main() {
				lowp vec4 source = texture2D(_MainTex, uv);
				lowp float grad = texture2D(grad_texture, uv).w;
				gl_FragColor = source * (1.0 - grad * vignette_intensity);
			}
			#endif

			ENDGLSL
		}
	}
}
