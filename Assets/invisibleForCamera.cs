using UnityEngine;

public class InvisibleForCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask invisibleMask;

    private void Awake() {
        if (!cam) {
            cam = GetComponentInParent<Camera>();
        }

        int maskValue = invisibleMask.value;

        cam.cullingMask &= ~maskValue;

        foreach (Camera otherCam in Camera.allCameras) {
            if (otherCam != cam) {
                otherCam.cullingMask |= maskValue;
            }
        }

        // Set the GameObject to the target layer
        gameObject.layer = Mathf.RoundToInt(Mathf.Log(maskValue, 2));
    }
}
