using UnityEngine;

public class PauseController : MonoBehaviour
{
    public bool gamePaused;
    [SerializeField] GameObject GamepadUI;
    [SerializeField] GameObject PauseMenuUI;

    public void TogglePauseMenu() {
        gamePaused = !gamePaused;

        UpdatePauseMenuUI();
    }

    void UpdatePauseMenuUI() {
        GamepadUI.SetActive(!gamePaused);
        PauseMenuUI.SetActive(gamePaused);

        Cursor.lockState = CursorLockMode.Locked;
        if (gamePaused) {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public bool GameIsPaused() {
        return gamePaused;
    }
}