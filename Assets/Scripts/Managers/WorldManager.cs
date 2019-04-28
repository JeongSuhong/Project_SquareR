using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private static WorldManager instance;
    public static WorldManager Instance
    {
        get
        {
            if (instance == null)
                instance = new WorldManager();

            return instance;
        }
    }

    [SerializeField] private DataDefine.GAME_STATE gameState;

    public delegate void ChangeGameStateDelegate(DataDefine.GAME_STATE gameState);
    public event ChangeGameStateDelegate OnChangeGameState;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeGameState(DataDefine.GAME_STATE changeState)
    {
        gameState = changeState;
        OnChangeGameState?.Invoke(gameState);
    }
}