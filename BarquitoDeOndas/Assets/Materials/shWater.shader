Shader "Custom/shWater" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_NoiseMap ("Noise", 2D) = "noise" {}
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_WaveLenght ("WaveLenght", Range(0,250)) = 0.05
		_Amplitud ("Amplitud", Range(0,1)) = 0.01
		_Speed ("Speed", Range(0,1000)) = 400
		_DirectionX ("DirX", Range(0,100)) = 0.1
		_DirectionZ ("DirZ", Range(0,100)) = 0.1

		_RadioCirculoFallOFF ("RadioCirculoFall", Range(0,10)) = 3
		_OffWave ("OffWave", Range(0,1)) = 1.0

		_Click ("Click", Range(0,1)) = 0

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		//#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _NoiseMap;

		float _WaveLenght; 
		float _Amplitud;
		float _Speed;
		float _DirectionX;
		float _DirectionZ;
		float _RadioCirculoFallOFF;
		float _OffWave;
		int _Click;
		float _Glossiness;
		float _Metallic;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_NoiseMap;
		};

		void vert (inout appdata_full v) {

			if (_Click) {
				float2 dir;
				dir.x = _DirectionX;
				dir.y = _DirectionZ;

				float2 posXZ;
				posXZ.x = v.vertex.x;
				posXZ.y = v.vertex.z;

				float dist = distance(posXZ, dir);
				float a = lerp(0,_RadioCirculoFallOFF, 1-_OffWave);
				float falloffCenter = smoothstep(a,_RadioCirculoFallOFF, dist);

				float falloff = (1.0f-smoothstep(0.0,_RadioCirculoFallOFF, dist)) * falloffCenter;

				float offsetVertex =  (v.vertex.x * v.vertex.x) + (v.vertex.z * v.vertex.z);
				float wave;
				offsetVertex = ((v.vertex.x - _DirectionX)*(v.vertex.x - _DirectionX) + (v.vertex.z - _DirectionZ)*(v.vertex.z - _DirectionZ));
				wave = falloff * _Amplitud * sin(offsetVertex * _WaveLenght + (-1.0f)*_Time.y *_Speed);

				//wave = falloff * _Amplitud * sin(offsetVertex * _WaveLenght + (-1.0f)*_Time.y *_Speed);
				//v.vertex.y += wave;

				float h = falloff * _WaveLenght * (v.vertex.x - _DirectionX) * _Amplitud * cos(offsetVertex * _WaveLenght + (-1.0f)*_Time.y *_Speed);
				v.normal.x = -1.0f*h;
				v.normal.y = 1.0f;
				v.normal.z = 1.0f*h;
			}


      	}


		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard  o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			IN.uv_NoiseMap.x = IN.uv_NoiseMap.x + _Time.y*0.045f;
			IN.uv_NoiseMap.y = IN.uv_NoiseMap.y + sin(_Time.y)*0.05f;
			fixed4 cNoise = tex2D (_NoiseMap, IN.uv_NoiseMap);


			//fixed4 c =  _Color;
			o.Albedo = c.rgb;
			IN.uv_BumpMap.x = IN.uv_BumpMap.x + _Time.y*-0.035f;
			//float4 npWave = tex2D (_BumpMap, IN.uv_BumpMap);
			//o.Normal = UnpackNormal (normalize(npWave));//o.Normal;
			float3 npWave = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			//float sNP = abs(sin(_Time.y));
			npWave = float3(npWave.x*cNoise.x, npWave.y*cNoise.x, npWave.z );
			o.Normal = npWave;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
