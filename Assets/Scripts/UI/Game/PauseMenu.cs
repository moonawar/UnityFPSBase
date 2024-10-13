using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private Image dimmer;
    [SerializeField] private RectTransform main;
    
    private bool startFlag = false;

    private void OnEnable() {
        if (!startFlag) return;
        InputContainer.Instance.Controls.Gameplay.Pause.performed += PauseCallback;
        InputContainer.Instance.Controls.PauseMenu.Resume.performed += ResumeCallback;
    }

    private void Start() {
        InputContainer.Instance.Controls.Gameplay.Pause.performed += PauseCallback;
        InputContainer.Instance.Controls.PauseMenu.Resume.performed += ResumeCallback;

        startFlag = true;
    }

    private void OnDisable() {
        InputContainer.Instance.Controls.Gameplay.Pause.performed -= PauseCallback;
        InputContainer.Instance.Controls.PauseMenu.Resume.performed -= ResumeCallback;
    }

    public void Pause() { PauseCallback(default); }
    public void Resume() { ResumeCallback(default); }

    public void PauseCallback(InputAction.CallbackContext _) {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        InputContainer.Instance.ChangeInputMap(InputMapType.PauseMenu);

        dimmer.gameObject.SetActive(true);
        main.gameObject.SetActive(true);
    }

    public void ResumeCallback(InputAction.CallbackContext _) {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InputContainer.Instance.ChangeInputMap(InputMapType.Gameplay);
    }

    public void QuitToMenu() {
        SceneLoader.Instance.LoadScene("MainMenu", loader: false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}