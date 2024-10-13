using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float jumpHeight = 0.7f;

    // Getters
    public float BaseSpeed => baseSpeed;
    public float JumpHeight => jumpHeight;
}