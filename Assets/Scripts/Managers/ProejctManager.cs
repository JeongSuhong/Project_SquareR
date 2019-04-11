using UnityEngine;

public class ProejctManager : MonoBehaviour
{
    private static ProejctManager instance;
    public static ProejctManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ProejctManager();

            return instance;
        }
    }

    [SerializeField]
    private DefineDatas.GAME_STATE GameState;

    public delegate void ChangeGameStateDelegate(DefineDatas.GAME_STATE gameState);
    public event ChangeGameStateDelegate OnChangeGameState;

    private void Awake()
    {
        instance = this;
    }
}
