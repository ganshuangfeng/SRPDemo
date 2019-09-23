using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

public class OpaqueRenderPipeline : RenderPipeline
{
    protected override void Render(ScriptableRenderContext context,Camera[] cameras) {
        //base.Render(context,cameras);

        //CommandBuffer cmd = new CommandBuffer();
        //cmd.ClearRenderTarget(true,true,Color.red,1.0f);

        //context.ExecuteCommandBuffer(cmd);
        //context.Submit();
        //cmd.Dispose();

        for (int i = 0; i < cameras.Length; i++) {
            RenderSingleCamera(context,cameras[i]);
        }
    }

    //单个相机的渲染结果
    private void RenderSingleCamera(ScriptableRenderContext context,Camera camera) {
        //剔除
        ScriptableCullingParameters cullingPrama;
        camera.TryGetCullingParameters(out cullingPrama);
        CullingResults cullRet = context.Cull(ref cullingPrama);
        context.SetupCameraProperties(camera);

        var cmd = new CommandBuffer();
        CameraClearFlags clearFlags = camera.clearFlags;
        cmd.ClearRenderTarget((CameraClearFlags.Color&clearFlags)!=0,(CameraClearFlags.Depth&clearFlags)!=0,camera.backgroundColor);
        context.ExecuteCommandBuffer(cmd);
        cmd.Release();

        //绘制
        SortingSettings sortSettings = new SortingSettings(camera);
        sortSettings.criteria = SortingCriteria.CommonOpaque;
        DrawingSettings drawSettings = new DrawingSettings(new ShaderTagId("BasicPass"),sortSettings);
        FilteringSettings filterSettings = new FilteringSettings(RenderQueueRange.opaque);
        context.DrawRenderers(cullRet,ref drawSettings,ref filterSettings);

        context.DrawSkybox(camera);
        context.Submit();
    }
}
