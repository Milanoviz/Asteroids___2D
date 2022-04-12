using UnityEngine;

namespace CodeBase.Helpers
{
    public static class CameraSettings
    {
        public static Vector3 CameraResolution => Camera.main.ViewportToWorldPoint(Camera.main.rect.size);
    }
}