﻿Shader "Custom/SnowShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Bump ("Bump", 2D) = "bump" {}
        _Snow ("Snow Level", Range(0,1) ) = 0
        _SnowColor ("Snow Color", Color) = (1.0,1.0,1.0,1.0)
        _SnowDirection ("Snow Direction", Vector) = (0,1,0)
        _SnowDepth ("Snow Depth", Range(0,0.2)) = 0.1
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert 

        sampler2D _MainTex;
        sampler2D _Bump;
        float _Snow;
        float4 _SnowColor;
        float4 _SnowDirection;
        float _SnowDepth; 

        struct Input {
            float2 uv_MainTex;
            float2 uv_Bump;
            float3 worldNormal;
            INTERNAL_DATA
        };

        void vert (inout appdata_full v) {
            //Convert the normal to world coortinates
            //float3 sn = mul((float3x3)_World2Object, normalize(_SnowDirection.xyz)).xyz;

			//float mod = dot(v.normal, sn) >= lerp(1,-1, (_Snow*2)/3);
			//if(dot(v.normal, sn) >= lerp(1,-1, (_Snow*2)/3)) {
           		//v.vertex.xyz += lerp(0, normalize(sn + v.normal) * _SnowDepth * _Snow, mod)
           	//	v.vertex.xyz += normalize(sn + v.normal) * _SnowDepth * _Snow;
            //}
        }

        void surf (Input IN, inout SurfaceOutput o) { 
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Normal = UnpackNormal (tex2D (_Bump, IN.uv_Bump));
                
          	float mod = dot(WorldNormalVector(IN, o.Normal), _SnowDirection.xyz) >= lerp(1,-1,_Snow);
          	//float mod = dot(o.Normal, half3(0,1,0)) >= lerp(1,-1,_Snow);
            
            o.Albedo = lerp(c.rgb, _SnowColor.rgb, mod);
            
            o.Alpha = 1;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}