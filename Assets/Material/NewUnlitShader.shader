Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        // Range(0,1) 범위를 가지는 float값으로 텍스처의 특정 중심점을 기준으로 변경을 정의
        // 반경 바깥의 픽셀은 투명(검정색,알파 0)으로 설정
        _Radius ("Radius", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // 알파 블랜딩이 추가되어 반경 바깥 픽셀 = (col = fixed4(0,0,0,0))이 실제로 투명처리됨
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 텍스처의 좌표 중심을 (0.5,0.5)로 설정
                // 픽셀좌표 (i.uv)와 중심 사이의 거리를 계산한뒤 
                // _Radius와 비교하여 반경 바깥쪽 픽셀은 투명으로 만든다.
                float2 center = float2(0.5,0.5);
                float dist = distance(i.uv, center);

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                if(dist > _Radius)
                {
                    col = fixed4(0,0,0,0);
                }

                return col;
            }
            ENDCG
        }
    }
}