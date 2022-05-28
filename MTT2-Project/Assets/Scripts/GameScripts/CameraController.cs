using Cinemachine;
using Nebby.UnityUtils;
using UnityEngine;

namespace MTT2
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : SingletonBehaviour<CameraController>
    {
        public Camera Camera { get => GetComponent<Camera>(); }
        public CinemachineVirtualCamera virtualCamera;
        public void SetFollow(GameObject obj) => SetFollow(obj.transform);
        public void SetFollow(Transform transform) => virtualCamera.Follow = transform;
    }
}
