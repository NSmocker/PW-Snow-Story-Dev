// Upgrade NOTE: commented out 'float3 _WorldSpaceCameraPos', a built-in variable

Shader "Custom/EmissionWithHDRIReflection"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _EmissionColor ("Emission Color", Color) = (0,0,0,0)
        _EmissionIntensity ("Emission Intensity", Range(0,10)) = 1
        _HDRIReflection ("HDRI Reflection (Equirectangular)", 2D) = "white" {}
        _ReflectionIntensity ("Reflection Intensity", Range(0,1)) = 0.5
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        [Toggle] _DoubleSided ("Double Sided", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldRefl : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
                float3 viewDir : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _EmissionColor;
            float _EmissionIntensity;
            sampler2D _HDRIReflection;
            float _ReflectionIntensity;
            float _Smoothness;
            float _DoubleSided;

            // float3 _WorldSpaceCameraPos;

            // Функція перетворення напрямку у UV координати equirectangular HDRI
            float2 DirToEquirectangularUV(float3 dir)
            {
                float u = 0.5 + atan2(dir.z, dir.x) / (6.2831853); // 2*PI
                float v = 0.5 - asin(dir.y) / 3.1415927; // PI
                return float2(u, v);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldNormal = normalize(o.worldNormal);

                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos);

                // Якщо двосторонній режим, інвертуємо нормаль для зворотної сторони
                #ifdef UNITY_FRONT_FACING
                if (_DoubleSided > 0.5 && !unity_FrontFacing)
                    o.worldNormal = -o.worldNormal;
                #endif

                o.worldRefl = reflect(-o.viewDir, o.worldNormal);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 albedo = tex2D(_MainTex, i.uv);
                
                // Конвертуємо напрямок відбиття у UV для HDRI текстури
                float2 hdriUV = DirToEquirectangularUV(normalize(i.worldRefl));
                
                // Вибираємо колір із HDRI текстури
                fixed4 reflection = tex2D(_HDRIReflection, hdriUV);

                // Змішуємо базовий колір з рефлексами
                fixed3 color = albedo.rgb * (1 - _ReflectionIntensity) + reflection.rgb * _ReflectionIntensity;

                // Емісія
                fixed3 emission = _EmissionColor.rgb * _EmissionIntensity;

                return fixed4(color + emission, albedo.a);
            }
            ENDCG

            // Додаємо ключ для двостороннього рендеру
            Cull Off
        }
    }
    FallBack "Diffuse"
}
