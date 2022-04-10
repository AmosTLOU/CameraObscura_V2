Shader "Universal Render Pipeline/Grass"
{
    Properties
    {
        // Specular vs Metallic workflow
        [HideInInspector] _WorkflowMode("WorkflowMode", Float) = 1.0

        [MainTexture] _BaseMap("Albedo", 2D) = "white" {}
        [MainColor] _BaseColor("Color", Color) = (1,1,1,1)

        

        // SRP batching compatibility for Clear Coat (Not used in Lit)
        [HideInInspector] _ClearCoatMask("_ClearCoatMask", Float) = 0.0
        [HideInInspector] _ClearCoatSmoothness("_ClearCoatSmoothness", Float) = 0.0

            // Blending state
            [HideInInspector] _Surface("__surface", Float) = 0.0
            [HideInInspector] _Blend("__blend", Float) = 0.0
            [HideInInspector] _AlphaClip("__clip", Float) = 0.0
            [HideInInspector] _SrcBlend("__src", Float) = 1.0
            [HideInInspector] _DstBlend("__dst", Float) = 0.0
            [HideInInspector] _ZWrite("__zw", Float) = 1.0
            [HideInInspector] _Cull("__cull", Float) = 2.0

            _ReceiveShadows("Receive Shadows", Float) = 1.0
            // Editmode props
            [HideInInspector] _QueueOffset("Queue offset", Float) = 0.0

            // ObsoleteProperties
            [HideInInspector] _MainTex("BaseMap", 2D) = "white" {}
            [HideInInspector] _Color("Base Color", Color) = (1, 1, 1, 1)
            [HideInInspector] _GlossMapScale("Smoothness", Float) = 0.0
            [HideInInspector] _Glossiness("Smoothness", Float) = 0.0
            [HideInInspector] _GlossyReflections("EnvironmentReflections", Float) = 0.0

            [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
            [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
            [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}

            [Header(Shading)]
        _TopColor("Top Color", Color) = (1,1,1,1)
        _BottomColor("Bottom Color", Color) = (1,1,1,1)
        _TranslucentGain("Translucent Gain", Range(0,1)) = 0.5
        _BendRotationRandom("Bend Rotation Random", Range(0, 1)) = 0.2
        _BladeWidth("Blade Width", Float) = 0.05
        _BladeWidthRandom("Blade Width Random", Float) = 0.02
        _BladeHeight("Blade Height", Float) = 0.5
        _BladeHeightRandom("Blade Height Random", Float) = 0.3
        _TessellationUniform("Tessellation Uniform", Range(1, 64)) = 1
        _WindDistortionMap("Wind Distortion Map", 2D) = "white" {}
        _WindFrequency("Wind Frequency", Vector) = (0.05, 0.05, 0, 0)
        _WindStrength("Wind Strength", Float) = 1

            CGINCLUDE
#include "UnityCG.cginc"
#include "Autolight.cginc"
//#include "Shaders/CustomTessellation.cginc"

        float _BendRotationRandom;
        float _BladeHeight;
        float _BladeHeightRandom;
        float _BladeWidth;
        float _BladeWidthRandom;
        sampler2D _WindDistortionMap;
        float4 _WindDistortionMap_ST;
        float2 _WindFrequency;
        float _WindStrength;


        struct vertexInput
        {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float4 tangent : TANGENT;
        };

        struct vertexOutput
        {
            float4 vertex : SV_POSITION;
            float3 normal : NORMAL;
            float4 tangent : TANGENT;
        };

        struct TessellationFactors
        {
            float edge[3] : SV_TessFactor;
            float inside : SV_InsideTessFactor;
        };

        vertexInput vert(vertexInput v)
        {
            return v;
        }

        vertexOutput tessVert(vertexInput v)
        {
            vertexOutput o;
            // Note that the vertex is NOT transformed to clip
            // space here; this is done in the grass geometry shader.
            o.vertex = v.vertex;
            o.normal = v.normal;
            o.tangent = v.tangent;
            return o;
        }

        float _TessellationUniform;

        TessellationFactors patchConstantFunction(InputPatch<vertexInput, 3> patch)
        {
            TessellationFactors f;
            f.edge[0] = _TessellationUniform;
            f.edge[1] = _TessellationUniform;
            f.edge[2] = _TessellationUniform;
            f.inside = _TessellationUniform;
            return f;
        }

        [UNITY_domain("tri")]
        [UNITY_outputcontrolpoints(3)]
        [UNITY_outputtopology("triangle_cw")]
        [UNITY_partitioning("integer")]
        [UNITY_patchconstantfunc("patchConstantFunction")]
        vertexInput hull(InputPatch<vertexInput, 3> patch, uint id : SV_OutputControlPointID)
        {
            return patch[id];
        }

        [UNITY_domain("tri")]
        vertexOutput domain(TessellationFactors factors, OutputPatch<vertexInput, 3> patch, float3 barycentricCoordinates : SV_DomainLocation)
        {
            vertexInput v;

#define MY_DOMAIN_PROGRAM_INTERPOLATE(fieldName) v.fieldName = \
		patch[0].fieldName * barycentricCoordinates.x + \
		patch[1].fieldName * barycentricCoordinates.y + \
		patch[2].fieldName * barycentricCoordinates.z;

            MY_DOMAIN_PROGRAM_INTERPOLATE(vertex)
                MY_DOMAIN_PROGRAM_INTERPOLATE(normal)
                MY_DOMAIN_PROGRAM_INTERPOLATE(tangent)

                return tessVert(v);
        }

        // Simple noise function, sourced from http://answers.unity.com/answers/624136/view.html
        // Extended discussion on this function can be found at the following link:
        // https://forum.unity.com/threads/am-i-over-complicating-this-random-function.454887/#post-2949326
        // Returns a number in the 0...1 range.
        float rand(float3 co)
    {
        return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
    }

        // Construct a rotation matrix that rotates around the provided axis, sourced from:
        // https://gist.github.com/keijiro/ee439d5e7388f3aafc5296005c8c3f33
        float3x3 AngleAxis3x3(float angle, float3 axis)
        {
            float c, s;
            sincos(angle, s, c);

            float t = 1 - c;
            float x = axis.x;
            float y = axis.y;
            float z = axis.z;

            return float3x3(
                t * x * x + c, t * x * y - s * z, t * x * z + s * y,
                t * x * y + s * z, t * y * y + c, t * y * z - s * x,
                t * x * z - s * y, t * y * z + s * x, t * z * z + c
                );
        }




        struct geometryOutput
        {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        geometryOutput VertexOutput(float3 pos, float2 uv)
        {
            geometryOutput o;
            o.pos = UnityObjectToClipPos(pos);
            o.uv = uv;
            return o;
        }

        [maxvertexcount(3)]
        void geo(triangle vertexOutput IN[3], inout TriangleStream<geometryOutput> triStream)
        {
            float3 pos = IN[0].vertex;
            float3 vNormal = IN[0].normal;
            float4 vTangent = IN[0].tangent;
            float3 vBinormal = cross(vNormal, vTangent) * vTangent.w;

            float3x3 tangentToLocal = float3x3(
                vTangent.x, vBinormal.x, vNormal.x,
                vTangent.y, vBinormal.y, vNormal.y,
                vTangent.z, vBinormal.z, vNormal.z
                );

            float3x3 facingRotationMatrix = AngleAxis3x3(rand(pos) * UNITY_TWO_PI, float3(0, 0, 1));
            float3x3 bendRotationMatrix = AngleAxis3x3(rand(pos.zzx) * _BendRotationRandom * UNITY_PI * 0.5, float3(-1, 0, 0));
            float2 uv = pos.xz * _WindDistortionMap_ST.xy + _WindDistortionMap_ST.zw + _WindFrequency * _Time.y;
            float2 windSample = (tex2Dlod(_WindDistortionMap, float4(uv, 0, 0)).xy * 2 - 1) * _WindStrength;
            float3 wind = normalize(float3(windSample.x, windSample.y, 0));
            float3x3 windRotation = AngleAxis3x3(UNITY_PI * windSample, wind);

            float3x3 transformationMatrix = mul(mul(mul(tangentToLocal, windRotation), facingRotationMatrix), bendRotationMatrix);

            float height = (rand(pos.zyx) * 2 - 1) * _BladeHeightRandom + _BladeHeight;
            float width = (rand(pos.xzy) * 2 - 1) * _BladeWidthRandom + _BladeWidth;

            triStream.Append(VertexOutput(pos + mul(transformationMatrix, float3(width, 0, 0)), float2(0, 0)));
            triStream.Append(VertexOutput(pos + mul(transformationMatrix, float3(-width, 0, 0)), float2(1, 0)));
            triStream.Append(VertexOutput(pos + mul(transformationMatrix, float3(0, 0, height)), float2(0.5, 1)));
        }

        ENDCG
    }

        SubShader
    {
            Cull Off
        // Universal Pipeline tag is required. If Universal render pipeline is not set in the graphics settings
        // this Subshader will fail. One can add a subshader below or fallback to Standard built-in to make this
        // material work with both Universal Render Pipeline and Builtin Unity Pipeline
        
        LOD 300

        // ------------------------------------------------------------------
        //  Forward pass. Shades all light in a single pass. GI + emission + Fog
        Pass
        {
            // Lightmode matches the ShaderPassName set in UniversalRenderPipeline.cs. SRPDefaultUnlit and passes with
            // no LightMode tag are also rendered by Universal Render Pipeline
            Name "ForwardLit"
            Tags{"LightMode" = "UniversalForward" "RenderType" = "Opaque"}

          

            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma geometry geo
                #pragma hull hull
                #pragma domain domain
                #pragma target 4.6

                #include "Lighting.cginc"

                float4 _TopColor;
                float4 _BottomColor;
                float _TranslucentGain;

                float4 frag(geometryOutput i, fixed facing : VFACE) : SV_Target
                {
                    return lerp(_BottomColor, _TopColor, i.uv.y);
                }
                ENDCG
    }

   

    

    
    }

        


        //FallBack "Hidden/Universal Render Pipeline/FallbackError"
        //CustomEditor "UnityEditor.Rendering.Universal.ShaderGUI.LitShader"
}
