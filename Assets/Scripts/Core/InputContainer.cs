public enum InputMapType {
    Gameplay = 0,
    PauseMenu = 1
}

public class InputContainer : SingletonMB<InputContainer>
{
    public Controls Controls { get; private set; }

    protected override void Awake() {
        base.Awake();
        Controls = new Controls();
    }

    public void ChangeInputMap(InputMapType inputMapType) {
        switch (inputMapType) {
            case InputMapType.Gameplay:
                Controls.Gameplay.Enable();
                Controls.PauseMenu.Disable();
                break;
            case InputMapType.PauseMenu:
                Controls.Gameplay.Disable();
                Controls.PauseMenu.Enable();
                break;
        }
    }

    private void OnEnable() {
        if (Instance == this) Controls.Enable();
    }

    private void OnDisable() {
        if (Instance == this) Controls.Disable();
    }
}
