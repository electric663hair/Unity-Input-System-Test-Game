using UnityEngine;
using UnityEngine.UI;

public class MainPauseMenuManager : MonoBehaviour
{
    [SerializeField] private Button BackToGameBtn;
    [SerializeField] private Button OptionsBtn;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private Button ExitGameBtn;

    private PauseController pauseController;
    private void Awake() {
        pauseController = GetComponent<PauseController>();
        BackToGameBtn.onClick.AddListener(ContinueGame);
        OptionsBtn.onClick.AddListener(OpenOptionsMenu);
        ExitGameBtn.onClick.AddListener(ExitGame);
    }

    void ContinueGame() {
        Cursor.lockState = CursorLockMode.Locked;
        if (pauseController != null) {
            pauseController.gamePaused = false;
        } else {
            Debug.LogError("PauseController component not found in scene");
        }
        gameObject.SetActive(false);
    }

    void OpenOptionsMenu() {
        OptionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
