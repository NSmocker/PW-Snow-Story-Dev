// Death Shader for Unity 6 URP (HLSL-based shader)
// Place in Assets and use via a Material

Shader "Custom/DeathEffectSAO"
{
    Properties
    {
        _BaseMap("Texture", 2D) = "white" {}
        _DeathProgress("Death Progress", Range(0,1)) = 0
        _NoiseScale("Noise Scale", Float) = 1
        _UpwardSpeed("Upward Speed", Float) = 1
        _EmissionColor("Emission Color", Color) = (1, 0.5, 0.5, 1)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

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
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _BaseMap;
            float4 _BaseMap_ST;
            float _DeathProgress;
            float _NoiseScale;
            float _UpwardSpeed;
            float4 _EmissionColor;

            // Simple hash function for noise-like randomness
            float rand(float3 co)
            {
                return frac(sin(dot(co, float3(12.9898, 78.233, 45.164))) * 43758.5453);
            }

            float3 animatedOffset(float3 pos, float deathProgress)
            {
                float noise = rand(pos * _NoiseScale);
                float3 dir = float3(rand(pos.yzx), rand(pos.zxy), rand(pos.xyz));
                dir = normalize(dir * 2.0 - 1.0);
                return dir * noise * deathProgress + float3(0, _UpwardSpeed * deathProgress, 0);
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                float3 worldPos = TransformObjectToWorld(IN.positionOS.xyz);
                float3 offset = animatedOffset(worldPos, _DeathProgress);
                float3 displaced = worldPos + offset;
                OUT.positionCS = TransformWorldToHClip(displaced);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                OUT.worldPos = displaced;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float4 col = tex2D(_BaseMap, IN.uv);
                float emissive = saturate(1.0 - _DeathProgress);
                return col + _EmissionColor * emissive;
            }
            ENDHLSL
        }
    }
    FallBack "Hidden/InternalErrorShader"
}
