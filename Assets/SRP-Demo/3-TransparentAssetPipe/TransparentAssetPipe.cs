using System;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class TransparentAssetPipe : RenderPipelineAsset
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("SRP-Demo/03 - Create Transparent Asset Pipeline")]
    static void CreateBasicAssetPipeline()
    {
        var instance = ScriptableObject.CreateInstance<TransparentAssetPipe>();
        GraphicsSettings.renderPipelineAsset = instance;
        UnityEditor.AssetDatabase.CreateAsset(instance, "Assets/SRP-Demo/3-TransparentAssetPipe/TransparentAssetPipe.asset");
    }
#endif

    protected override RenderPipeline CreatePipeline()
    {
        return new TransparentAssetPipeInstance();
    }
}

public class TransparentAssetPipeInstance : RenderPipeline
{
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        //base.Render(context, cameras);

        foreach (var camera in cameras)
        {
            // Culling
            ScriptableCullingParameters cullingParams;
            if (!camera.TryGetCullingParameters(out cullingParams))
                continue;

            CullingResults cull = context.Cull(ref cullingParams);

            // Setup camera for rendering (sets render target, view/projection matrices and other
            // per-camera built-in shader variables).
            context.SetupCameraProperties(camera);

            // clear depth buffer
            var cmd = new CommandBuffer();
            cmd.ClearRenderTarget(true, false, Color.black);
            context.ExecuteCommandBuffer(cmd);
            cmd.Release();

            // Draw opaque objects using BasicPass shader pass
            SortingSettings sort = new SortingSettings(camera);
            sort.criteria= SortingCriteria.CommonOpaque;
            var settings = new DrawingSettings(new ShaderTagId("BasicPass"),sort);


            var filterSettings = new FilteringSettings(RenderQueueRange.opaque);
            context.DrawRenderers(cull, ref settings, ref filterSettings);

            // Draw skybox
            context.DrawSkybox(camera);

            // Draw transparent objects using BasicPass shader pass
            sort.criteria = SortingCriteria.CommonTransparent;
            settings.sortingSettings = sort;
            filterSettings.renderQueueRange = RenderQueueRange.transparent;
            context.DrawRenderers(cull, ref settings,ref filterSettings);

            context.Submit();
        }
    }
}
