using UnityEngine;
using System;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState { get; private set; }
    public event Action<GameState> OnStateChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetState(GameState.MainMenu);
    }

    public void SetState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(newState);

        Debug.Log("Game State Changed To: " + newState);

        switch (newState)
        {
            case GameState.MainMenu:
                // Logic for main menu
                break;
            case GameState.Playing:
                // Start gameplay
                break;
            case GameState.Paused:
                // Pause game
                break;
            case GameState.GameOver:
                // End game
                break;
        }
    }

    public void StartGame()
    {
        SetState(GameState.Playing);
    }

    public void PauseGame()
    {
        SetState(GameState.Paused);
    }

    public void ResumeGame()
    {
        SetState(GameState.Playing);
    }
}
