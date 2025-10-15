Shader "Custom/VerticalBlurCharacter"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _BlurAmount("Blur Amount", Range(0, 0.01)) = 0.001
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Name "VerticalBlur"
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float _BlurAmount;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv;

                // 5 семплів для вертикального розмиття
                half4 c0 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                half4 c1 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(0, _BlurAmount * 1.0));
                half4 c2 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv - float2(0, _BlurAmount * 1.0));
                half4 c3 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(0, _BlurAmount * 2.0));
                half4 c4 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv - float2(0, _BlurAmount * 2.0));

                half4 color = (c0 + c1 + c2 + c3 + c4) / 5.0;
                return color;
            }
            ENDHLSL
        }
    }
}
