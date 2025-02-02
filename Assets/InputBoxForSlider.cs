using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputBoxForSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_InputField InputField;

    public void SetInputFieldValue() {
        InputField.text = slider.value.ToString();
    }
}