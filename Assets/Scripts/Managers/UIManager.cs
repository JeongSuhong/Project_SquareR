using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new UIManager();

            return instance;
        }
    }

    public UIAtlasManager AtlasManager { get; private set; }

    public delegate void UpdateUIDelegate(DataDefine.GAME_STATE gameState);
    public event UpdateUIDelegate OnUpdateUI;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AtlasManager = GetComponentInChildren<UIAtlasManager>();
        WorldManager.Instance.OnChangeGameState += ChangeGameState;
    }

    private void ChangeGameState(DataDefine.GAME_STATE changeState)
    {
        OnUpdateUI?.Invoke(changeState);
    }
}
