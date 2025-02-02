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
        if (panels != null) {
            foreach (GameObject panel in panels) {
                panel.SetActive(false);
            }
        }

        for (int i = 0; i < buttons.Count; i++) {
            Button btn = buttons[i];
            if (panels[i] != null) {
                GameObject panel = panels[i];

                btn.onClick.AddListener(() => {
                    if (panel != null && panel.transform.name.ToLower() != "null") {
                        panel.SetActive(true);
                        gameObject.SetActive(false);
                    }
                });
            } else {
                Debug.LogWarning("Panel " + i + " is not defined on " + transform.name);
            }
        }

        backButtton.onClick.AddListener(() => {
            if (backPanel != null) {
                backPanel.SetActive(true);
                gameObject.SetActive(false);
            } else {
                Debug.LogWarning("UI for backbutton not defined");
            }
        });
    }
}