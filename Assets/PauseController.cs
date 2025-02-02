using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public bool gamePaused;
    [SerializeField] private GameObject GamepadUI;
    [SerializeField] private List<GameObject> UIMenus;

    public void TogglePauseMenu() {
        gamePaused = !gamePaused;

        if ( !gamePaused ) {
            foreach (GameObject menu in UIMenus) {
                Debug.Log(menu.name);
                menu.SetActive(false);
            }
        }

        UpdatePauseMenuUI();
    }

    private void Update() {
        UpdatePauseMenuUI();
    }

    void UpdatePauseMenuUI() {
        GamepadUI.SetActive(!gamePaused);
        gameObject.SetActive(gamePaused);

        Cursor.lockState = CursorLockMode.Locked;
        if (gamePaused) {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public bool GameIsPaused() {
        return gamePaused;
    }
}