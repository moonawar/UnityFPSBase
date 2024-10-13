using TMPro;
using UnityEngine;

public class InteractText : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;

    private void Awake() {
        text.gameObject.SetActive(false);
    }

    public void SetText(string value) {
        text.text = value;
    }

    public void SetActive(bool isActive) {
        text.gameObject.SetActive(isActive);
    }
}