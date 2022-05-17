using Cinemachine;
using Nebby.UnityUtils;
using UnityEngine;

namespace MTT2
{
    public class CameraController : SingletonBehaviour<CameraController>
    {
        public CinemachineVirtualCamera virtualCamera;
        public void SetFollow(GameObject obj) => SetFollow(obj.transform);
        public void SetFollow(Transform transform) => virtualCamera.Follow = transform;
    }
}
