using UnityEngine;

public class SceneInputMap : MonoBehaviour {
    [SerializeField] private InputMapType inputMapType = InputMapType.Gameplay;    

    private void Start() {
        InputContainer.Instance.ChangeInputMap(inputMapType);
    }
}