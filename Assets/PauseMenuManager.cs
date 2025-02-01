using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private Button backButtton;
    [SerializeField] private GameObject backPanel;

    private void Awake() {
        for (int i = 0; i < buttons.Count; i++) {
            Button btn = buttons[i];
            GameObject panel = panels[i];
            if (panel == null) {
                Debug.LogError("Gameobject to activate not defined on index " + i + "| " + transform.name);
                continue;
            }

            btn.onClick.AddListener(() => {
                panel.SetActive(true);
                gameObject.SetActive(false);
            });
        }

        backButtton.onClick.AddListener(() => {
            backPanel.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}