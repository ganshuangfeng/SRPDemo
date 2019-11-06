using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

public class BasicRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new BasicRenderPipeline();
    }

    [MenuItem("MySRP/1-Create Basic SrpAsset",false,0)]
    public static void CreateBasicSRPAsset() {
        RenderPipelineAsset pipeline = ScriptableObject.CreateInstance<BasicRenderPipelineAsset>();
        GraphicsSettings.renderPipelineAsset = pipeline; //创建的同时，将其设置为当前的渲染管线

        if (AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>("Assets/SRP-Learn/SRPs/BasicSRP.asset"))
        {
            AssetDatabase.DeleteAsset("Assets/SRP-Learn/SRPs/BasicSRP.asset");
        }
        AssetDatabase.CreateAsset(pipeline, "Assets/SRP-Learn/SRPs/BasicSRP.asset"); 
    }

    [MenuItem("MySRP/1-Set Basic SrpAsset",false,0)]
    public static void SetBasicSRPAsset() {
        RenderPipelineAsset pipeline = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>("Assets/SRP-Learn/SRPs/SRPLearn.asset");
        if (pipeline == null) {
            Debug.LogError("basic pipeline asset is null!");
            return;
        }
        GraphicsSettings.renderPipelineAsset = pipeline;
    }
}
