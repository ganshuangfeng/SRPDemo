using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

public class TransparentRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new TransparentRenderPipeline();
    }

    [MenuItem("MySRP/3-Create Transparent SrpAsset",false,24)]
    public static void CreateTransparentSRPAsset() {
        RenderPipelineAsset pipeline = ScriptableObject.CreateInstance<TransparentRenderPipelineAsset>();
        GraphicsSettings.renderPipelineAsset = pipeline; //创建的同时，将其设置为当前的渲染管线

        if (AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>("Assets/SRP-Learn/SRPs/OpacheSRP.asset")) {
            AssetDatabase.DeleteAsset("Assets/SRP-Learn/SRPs/OpacheSRP.asset");
        }
        AssetDatabase.CreateAsset(pipeline, "Assets/SRP-Learn/SRPs/OpacheSRP.asset");
    }

    [MenuItem("MySRP/3-Set Transparent SrpAsset",false,24)]
    public static void SetTransparentSRPAsset() {
        RenderPipelineAsset pipeline = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>("Assets/SRP-Learn/SRPs/SRPLearn.asset");
        if (pipeline == null) {
            Debug.LogError("Transparent pipeline asset is null!");
            return;
        }
        GraphicsSettings.renderPipelineAsset = pipeline;
    }
}
