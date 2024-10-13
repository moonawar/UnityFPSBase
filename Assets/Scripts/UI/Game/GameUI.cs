using UnityEngine;

[RequireComponent(typeof(InteractText))]
public class GameUI : SingletonMB<GameUI>
{
    private InteractText interactText;

    protected override void Awake() {
        base.Awake();
        interactText = GetComponent<InteractText>();
    }

    public void SetInteractText(bool active, string value = "(E) Interact") {
        interactText.SetText(value);
        interactText.SetActive(active);
    }
}