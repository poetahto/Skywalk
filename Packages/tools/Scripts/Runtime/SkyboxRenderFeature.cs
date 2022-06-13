// using UnityEngine.Rendering.Universal;
//
// public class SkyboxRenderFeature : ScriptableRendererFeature
// {
//     private DrawSkyboxPass _skyboxPass;
//
//     public override void Create()
//     {
//         _skyboxPass = new DrawSkyboxPass(RenderPassEvent.AfterRenderingSkybox)
//         {
//             // Configures where the render pass should be injected.
//             renderPassEvent = RenderPassEvent.AfterRenderingOpaques
//         };
//     }
//
//     // Here you can inject one or multiple render passes in the renderer.
//     // This method is called when setting up the renderer once per-camera.
//     public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
//     {
//         renderer.EnqueuePass(_skyboxPass);
//     }
// }