Shader "Custom/URPRefraction"
{
    Properties
    {
        _RefractionStrength("Refraction Strength", Range(0, 1)) = 0.1
        _NormalMap("Normal Map", 2D) = "bump" {}
        _MainTex("Main Texture", 2D) = "white" {}
        _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        _EmissionMap("Emission Map", 2D) = "black" {}
        _EmissionColor("Emission Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 viewDirWS : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
            };

            sampler2D _CameraOpaqueTexture;
            sampler2D _MainTex;
            sampler2D _NormalMap;
            sampler2D _EmissionMap;
            float _RefractionStrength;
            float4 _BaseColor;
            float4 _EmissionColor;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                float3 positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.positionHCS = TransformWorldToHClip(positionWS);
                OUT.uv = IN.uv;

                float3 normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.normalWS = normalWS;

                float3 viewDirWS = GetWorldSpaceViewDir(positionWS);
                OUT.viewDirWS = viewDirWS;

                OUT.screenPos = ComputeScreenPos(OUT.positionHCS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Normalize
                float3 viewDir = normalize(IN.viewDirWS);
                float3 normalTS = UnpackNormal(tex2D(_NormalMap, IN.uv));
                float3 normalWS = normalize(IN.normalWS + normalTS);

                // Refraction offset
                float2 offset = normalWS.xy * _RefractionStrength;

                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
                screenUV += offset;

                // Sample the opaque texture behind
                float4 refracted = tex2D(_CameraOpaqueTexture, screenUV);

                // Base texture color
                float4 baseColor = tex2D(_MainTex, IN.uv) * _BaseColor;

                // Emission map and color
                float4 emission = tex2D(_EmissionMap, IN.uv) * _EmissionColor;

                // Combine base color, refraction, and emission
                return lerp(baseColor, refracted, 0.5) + emission;
            }
            ENDHLSL
        }
    }
    FallBack "Hidden/InternalErrorShader"
}
