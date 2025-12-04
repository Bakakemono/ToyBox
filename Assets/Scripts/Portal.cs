using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour {
    [SerializeField] Camera _portalCamera;
    [SerializeField] Transform _pairPortal;

    private void OnEnable() {
        RenderPipelineManager.beginCameraRendering += UpdateCamera;
    }

    private void OnDisable() {
        RenderPipeline.beginCameraRendering -= UpdateCamera;
    }

    void UpdateCamera(Camera camera) {
        if((camera.cameraType == CameraType.Game || camera.cameraType == CameraType.SceneView) && camera.tag != "Portal Camera") {
            _portalCamera.projectionMatrix = camera.projectionMatrix;

            var relativePosition = transform.InverseTransformPoint(camera.transform.position);
            relativePosition = Vector3.Scale(relativePosition, new Vector3(-1, 1, -1));
            _portalCamera.transform.position = _pairPortal.TransformPoint(relativePosition);

            var relativeRotation = transform.InverseTransformDirection(camera.transform.forward);
            relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, 1, -1));
            _portalCamera.transform.forward = _pairPortal.TransformPoint(relativeRotation);
        }
    }


}