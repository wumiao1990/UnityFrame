// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:5870,x:33535,y:32759,varname:node_5870,prsc:2|emission-7631-OUT,alpha-8492-OUT;n:type:ShaderForge.SFN_TexCoord,id:1129,x:31781,y:33019,varname:node_1129,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:2510,x:31123,y:32951,ptovrint:False,ptlb:dissolve_u,ptin:_dissolve_u,varname:node_2510,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;n:type:ShaderForge.SFN_Slider,id:3742,x:31113,y:33340,ptovrint:False,ptlb:dissolve_v,ptin:_dissolve_v,varname:node_3742,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;n:type:ShaderForge.SFN_Append,id:6336,x:31781,y:32889,varname:node_6336,prsc:2|A-1658-OUT,B-1003-OUT;n:type:ShaderForge.SFN_Add,id:2428,x:31942,y:32911,varname:node_2428,prsc:2|A-6336-OUT,B-1129-UVOUT;n:type:ShaderForge.SFN_Multiply,id:1003,x:31600,y:33131,varname:node_1003,prsc:2|A-3742-OUT,B-9669-T;n:type:ShaderForge.SFN_Time,id:9669,x:31202,y:33151,varname:node_9669,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1658,x:31584,y:32889,varname:node_1658,prsc:2|A-2510-OUT,B-9669-T;n:type:ShaderForge.SFN_Step,id:6306,x:32465,y:32949,varname:node_6306,prsc:2|A-1487-R,B-5126-U;n:type:ShaderForge.SFN_Multiply,id:8492,x:33236,y:33016,varname:node_8492,prsc:2|A-7915-A,B-6883-OUT,C-199-R,D-1381-A;n:type:ShaderForge.SFN_Tex2d,id:7915,x:32131,y:32737,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_7915,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8f142999b95161340926196ff2902d6d,ntxv:0,isnm:False|UVIN-2018-OUT;n:type:ShaderForge.SFN_Tex2d,id:1487,x:32131,y:32930,ptovrint:False,ptlb:Dissolve,ptin:_Dissolve,varname:node_1487,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:424e18ef53cbc564da364da234732f7b,ntxv:0,isnm:False|UVIN-2428-OUT;n:type:ShaderForge.SFN_Multiply,id:3158,x:32568,y:32586,varname:node_3158,prsc:2|A-1381-RGB,B-7915-RGB,C-3898-OUT,D-1381-A;n:type:ShaderForge.SFN_VertexColor,id:1381,x:32113,y:32350,varname:node_1381,prsc:2;n:type:ShaderForge.SFN_Color,id:4499,x:31972,y:32496,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4499,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:4273,x:31202,y:32508,varname:node_4273,prsc:2,uv:1,uaff:True;n:type:ShaderForge.SFN_Append,id:7453,x:31508,y:32532,varname:node_7453,prsc:2|A-4273-Z,B-4273-W;n:type:ShaderForge.SFN_TexCoord,id:2962,x:31507,y:32692,varname:node_2962,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Add,id:2018,x:31788,y:32561,varname:node_2018,prsc:2|A-7453-OUT,B-2962-UVOUT;n:type:ShaderForge.SFN_Add,id:7151,x:32465,y:33152,varname:node_7151,prsc:2|A-5126-U,B-3266-OUT;n:type:ShaderForge.SFN_Slider,id:3266,x:32047,y:33318,ptovrint:False,ptlb:soft,ptin:_soft,varname:node_3266,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.05045052,max:1;n:type:ShaderForge.SFN_Smoothstep,id:6883,x:32838,y:33069,varname:node_6883,prsc:2|A-6306-OUT,B-7717-OUT,V-1487-R;n:type:ShaderForge.SFN_Step,id:7717,x:32655,y:33152,varname:node_7717,prsc:2|A-1487-R,B-7151-OUT;n:type:ShaderForge.SFN_TexCoord,id:5126,x:31981,y:33088,varname:node_5126,prsc:2,uv:2,uaff:True;n:type:ShaderForge.SFN_Multiply,id:7631,x:33256,y:32582,varname:node_7631,prsc:2|A-199-R,B-6883-OUT,C-3158-OUT,D-7915-A;n:type:ShaderForge.SFN_Tex2d,id:199,x:32870,y:32454,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_199,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a09565d316642144180b078e519b7318,ntxv:0,isnm:False|UVIN-5205-OUT;n:type:ShaderForge.SFN_Slider,id:9089,x:31558,y:31916,ptovrint:False,ptlb:mask_u,ptin:_mask_u,varname:_dissolve_u_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;n:type:ShaderForge.SFN_Time,id:3963,x:31637,y:32116,varname:node_3963,prsc:2;n:type:ShaderForge.SFN_Slider,id:6892,x:31548,y:32305,ptovrint:False,ptlb:mask_v,ptin:_mask_v,varname:_dissolve_v_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;n:type:ShaderForge.SFN_Multiply,id:6693,x:32035,y:32096,varname:node_6693,prsc:2|A-6892-OUT,B-3963-T;n:type:ShaderForge.SFN_Multiply,id:7349,x:32019,y:31854,varname:node_7349,prsc:2|A-9089-OUT,B-3963-T;n:type:ShaderForge.SFN_Append,id:5882,x:32216,y:31854,varname:node_5882,prsc:2|A-7349-OUT,B-6693-OUT;n:type:ShaderForge.SFN_TexCoord,id:7596,x:32286,y:32117,varname:node_7596,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:5205,x:32674,y:32153,varname:node_5205,prsc:2|A-5882-OUT,B-7596-UVOUT;n:type:ShaderForge.SFN_Add,id:3898,x:32261,y:32593,varname:node_3898,prsc:2|A-4499-RGB,B-2739-OUT;n:type:ShaderForge.SFN_Slider,id:2739,x:31907,y:32661,ptovrint:False,ptlb:Color_Intensty,ptin:_Color_Intensty,varname:node_2739,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;proporder:4499-2739-7915-1487-2510-3742-3266-199-9089-6892;pass:END;sub:END;*/

Shader "Custom/Xmioon/Yalen_Dissolve_Mask_Additive" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Color_Intensty ("Color_Intensty", Range(-10, 10)) = 0
        _Tex ("Tex", 2D) = "white" {}
        _Dissolve ("Dissolve", 2D) = "white" {}
        _dissolve_u ("dissolve_u", Range(-10, 10)) = 0
        _dissolve_v ("dissolve_v", Range(-10, 10)) = 0
        _soft ("soft", Range(-1, 1)) = 0.05045052
        _Mask ("Mask", 2D) = "white" {}
        _mask_u ("mask_u", Range(-10, 10)) = 0
        _mask_v ("mask_v", Range(-10, 10)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fog

            uniform float4 _TimeEditor;
            uniform float _dissolve_u;
            uniform float _dissolve_v;
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            uniform sampler2D _Dissolve; uniform float4 _Dissolve_ST;
            uniform float4 _Color;
            uniform float _soft;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _mask_u;
            uniform float _mask_v;
            uniform float _Color_Intensty;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 uv2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_3963 = _Time + _TimeEditor;
                float2 node_5205 = (float2((_mask_u*node_3963.g),(_mask_v*node_3963.g))+i.uv0);
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_5205, _Mask));
                
//                float4 node_9669 = _Time + _TimeEditor;
//                float2 node_2428 = (float2((_dissolve_u * node_9669.g),(_dissolve_v*node_9669.g)) + i.uv0);                
//                float4 _Dissolve_var = tex2D(_Dissolve,TRANSFORM_TEX(node_2428, _Dissolve));
//                float node_6883 = smoothstep( step(_Dissolve_var.r,i.uv2.r), step(_Dissolve_var.r,(i.uv2.r+_soft)), _Dissolve_var.r );
                
                float3 _Dissolve_var = tex2D(_Dissolve, i.uv0).rgb - i.uv1.x;
                clip(_Dissolve_var); 
                
                float2 node_2018 = (float2(i.uv1.b,i.uv1.a)+i.uv0);
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_2018, _Tex));
                float3 emissive = (_Mask_var.r*(i.vertexColor.rgb*_Tex_var.rgb*(_Color.rgb+_Color_Intensty)*i.vertexColor.a)*_Tex_var.a);
                float3 finalColor = emissive;
                return fixed4(finalColor,(_Tex_var.a*_Mask_var.r*i.vertexColor.a));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
