using UnityEngine;
using UnityEngine.UI;

public class Inputs : MonoBehaviour
{
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Slider controllerSensitivitySlider;
    public static float mouseSens;
    public static float controllerSens;

    private void Update() {
        mouseSens = mouseSensitivitySlider.value;
        controllerSens = controllerSensitivitySlider.value;
    }
}