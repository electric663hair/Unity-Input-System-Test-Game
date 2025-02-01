using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuButtonManager : MonoBehaviour
{
    [Header("MainPauseMenu")]
    [SerializeField] private Button BackToGameBtn;

    [SerializeField] private Button OptionsBtn;
    [SerializeField] private Button CloseOptionsMenuBtn;

    [SerializeField] private Button ExitGameBtn;

    [Header("GameObject Panels")]
    [SerializeField] private GameObject PauseMenuPanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject MainOptionsPanel;

    [Header("First selected Btns")]
    [SerializeField] private Button FirstSelectedBtnPauseMenu;
    [SerializeField] private Button FirstSelectedBtnOptionsMenu;
    [SerializeField] private Slider FirstSelectedSliderSensitivityMenu;

    [Header("AudioSettings")]
    [SerializeField] private Button AudioSettingsBtn;
    [SerializeField] private Button CloseAudioSettingsBtn;
    [SerializeField] private GameObject AudioSettingsMenu;

    [Header("VideoSettings")]
    [SerializeField] private Button VideoSettingsBtn;
    [SerializeField] private Button CloseVideoSettingsBtn;
    [SerializeField] private GameObject VideoSettingsMenu;

    [Header("InputSettings")]
    [SerializeField] private Button InputSettingsBtn;
    [SerializeField] private Button CloseInputSettingsBtn;
    
    [SerializeField] private GameObject InputSettingsMenu;
    [SerializeField] private GameObject SensitivityMenu;

    [SerializeField] private Button SensitivitySettingsBtn;
    [SerializeField] private Button CloseSensitivityMenuBtn;
    [SerializeField] private Slider MouseSensitivity;
    [SerializeField] private Slider ControllerSensitivity;

    private PauseController pauseController;
    private void Awake() {
        pauseController = GetComponent<PauseController>();
        BackToGameBtn.onClick.AddListener(ContinueGame);

        OptionsBtn.onClick.AddListener(OpenOptionsMenu);
        CloseOptionsMenuBtn.onClick.AddListener(CloseOptionsMenu);

        ExitGameBtn.onClick.AddListener(ExitGame);

        PauseMenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);

        // settingsButtons assignment
        // AudioSettingsBtn.onClick.AddListener(() => SetAudioSettingsVisibility(true));
        // CloseAudioSettingsBtn.onClick.AddListener(() => SetAudioSettingsVisibility(false));

        // VideoSettingsBtn.onClick.AddListener(() => SetVideoSettingsVisibility(true));
        // CloseVideoSettingsBtn.onClick.AddListener(() => SetVideoSettingsVisibility(false));

        InputSettingsBtn.onClick.AddListener(() => SetInputSettingsVisibility(true));
        CloseInputSettingsBtn.onClick.AddListener(() => SetInputSettingsVisibility(false));

        SensitivitySettingsBtn.onClick.AddListener(() => SensitivityTab(true));
        CloseSensitivityMenuBtn.onClick.AddListener(() => SensitivityTab(false));
    }

    private void Update() {
        Debug.Log(MouseSensitivity.value + " mouse | controller " + ControllerSensitivity.value);
    }

    void SetAudioSettingsVisibility(bool visibilityState) {
        AudioSettingsMenu.SetActive(visibilityState);

        if (visibilityState) {
            CloseOptionsMenu();
        } else {
            OpenOptionsMenu();
        }
    }

    void SetVideoSettingsVisibility(bool visibilityState) {
        VideoSettingsMenu.SetActive(visibilityState);

        if (visibilityState) {
            CloseOptionsMenu();
        } else {
            OpenOptionsMenu();
        }
    }

    void SetInputSettingsVisibility(bool visibilityState) {
        InputSettingsMenu.SetActive(visibilityState);
        OptionsPanel.SetActive(!visibilityState);
    }

    void SensitivityTab(bool visibility) {
        SensitivityMenu.SetActive(visibility);
        InputSettingsMenu.SetActive(!visibility);
    }

    void ContinueGame() {
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuPanel.SetActive(false);
        if (pauseController != null) {
            pauseController.gamePaused = false;
        } else {
            Debug.LogError("PauseController component not found in scene");
        }
    }

    void OpenOptionsMenu() {
        EventSystem.current.SetSelectedGameObject(FirstSelectedBtnOptionsMenu.gameObject);
        PauseMenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);

        // Making all children of OptionsPanel invisible
        for (int i = 0; i<OptionsPanel.transform.childCount; i++) {
            OptionsPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        // Making the main OptionPanel visible
        MainOptionsPanel.SetActive(true);
    }

    void CloseOptionsMenu() {
        EventSystem.current.SetSelectedGameObject(FirstSelectedBtnPauseMenu.gameObject);
        PauseMenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }

    void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
