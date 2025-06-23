using UnityEngine;

public class GameManager : BasicSingleton<GameManager>
{
    [Header("�̺�Ʈ")]
    [SerializeField] private VoidEventChannelSO gameStartEvent;
    [SerializeField] private VoidEventChannelSO gameEndEvent;


    void OnEnable()
    {
        if (gameStartEvent != null)
        {
            gameStartEvent.AddListener(OnGameStart);
        }
        if (gameEndEvent != null)
        {
            gameEndEvent.AddListener(OnGameEnd);
        }
    }

    void OnDisable()
    {
        if (gameStartEvent != null)
        {
            gameStartEvent.RemoveListener(OnGameStart);
        }
        if (gameEndEvent != null)
        {
            gameEndEvent.RemoveListener(OnGameEnd);
        }
    }

    private void OnGameStart()
    {
        
    }

    private void OnGameEnd()
    {
        
    }
}
