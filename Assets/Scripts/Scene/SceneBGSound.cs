using UnityEngine;

public class SceneBGSound : MonoBehaviour {
    [SerializeField] private string[] bgNames;

    private void Start() {
        foreach (string bgName in bgNames) {
            if (string.IsNullOrEmpty(bgName)) continue;
            AudioManager.Instance.Play(bgName);
        }
    }
}