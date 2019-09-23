using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

public class OpaqueRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new OpaqueRenderPipeline();
    }

    [MenuItem("MySRP/2-Create Opaque SrpAsset",false,12)]
    public static void CreateOpaqueSRPAsset() {
        RenderPipelineAsset pipeline = ScriptableObject.CreateInstance<OpaqueRenderPipelineAsset>();
        GraphicsSettings.renderPipelineAsset = pipeline; //创建的同时，将其设置为当前的渲染管线

        if (AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>("Assets/SRP-Learn/SRPs/OpacheSRP.asset")) {
            AssetDatabase.DeleteAsset("Assets/SRP-Learn/SRPs/OpacheSRP.asset");
        }
        AssetDatabase.CreateAsset(pipeline, "Assets/SRP-Learn/SRPs/OpacheSRP.asset");
    }

    [MenuItem("MySRP/2-Set Opaque SrpAsset",false,12)]
    public static void SetOpaqueSRPAsset() {
        RenderPipelineAsset pipeline = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>("Assets/SRP-Learn/SRPs/SRPLearn.asset");
        if (pipeline == null) {
            Debug.LogError("Opaque pipeline asset is null!");
            return;
        }
        GraphicsSettings.renderPipelineAsset = pipeline;
    }
}
