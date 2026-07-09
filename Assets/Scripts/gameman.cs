using UnityEngine;
using UnityEngine.InputSystem; // ✅ Use the new Input System

public class GameOverManager : MonoBehaviour
{
    public GameObject player;        // Reference to the player
    public GameObject gameOverScreen; // Reference to the Game Over UI

    private Rigidbody playerRigidbody;

    void Start()
    {
        // Get the Rigidbody component of the player
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody>();
        }

        // Hide the Game Over screen at the start
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        // ✅ Fix: Use the New Input System
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        if (playerRigidbody != null)
        {
            // Enable gravity to make the player fall
            playerRigidbody.useGravity = true;
            playerRigidbody.constraints = RigidbodyConstraints.None;
        }

        // Show the Game Over UI
        gameOverScreen.SetActive(true);

        // ✅ Fix: Stop all movement in the game
        Time.timeScale = 0f;
    }
}