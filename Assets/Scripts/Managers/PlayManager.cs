using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    private static PlayManager instance;
    public static PlayManager Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayManager();

            return instance;
        }
    }

    public List<BlockData> CurrentMap { get; private set; }
    public Dictionary<DataDefine.SKILL_TYPE, int> CurrentSkill { get; private set; }

    private int targetBlockCount;

    public bool IsWin { get; private set; }
    public int Score { get; private set; }

    public delegate void OnUpdateMapDelegate(List<BlockData> updateBlocks);
    public OnUpdateMapDelegate OnUpdateMap;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        WorldManager.Instance.OnChangeGameState += ChangeGameState;
    }

    private void ChangeGameState(DataDefine.GAME_STATE state)
    {
        switch (state)
        {
            case DataDefine.GAME_STATE.STAGE:
                StartPlay();
                break;
        }
    }

    private void StartPlay()
    {
        //Test : DataManager 에서 현재 Play Stage의 Data를 가져와야함
        CurrentMap = new List<BlockData>();
        List<BlockData> originData = DataManager.Instance.MapData;

        for (int i = 0; i < originData.Count; i++)
        {
            if (originData[i].BlockType == DataDefine.BLOCK_TYPE.TARGET)
                targetBlockCount++;

            CurrentMap.Add(new BlockData(originData[i]));
        }

        CurrentSkill = new Dictionary<DataDefine.SKILL_TYPE, int>(DataManager.Instance.SkillData);


        OnUpdateMap?.Invoke(CurrentMap);
    }

    public bool SetMapDataForSkill(Dictionary<int, DataDefine.SKILL_TYPE> targets)
    {
        foreach (KeyValuePair<int, DataDefine.SKILL_TYPE> target in targets)
        {
            DataDefine.BLOCK_TYPE originType = CurrentMap[target.Key].BlockType;

            if (originType == DataDefine.BLOCK_TYPE.TRAP)
            {
                EndPlay(false, 0);
                return false;
            }
            else if (originType == DataDefine.BLOCK_TYPE.TARGET)
            {
                targetBlockCount--;

                if (targetBlockCount == 0)
                {
                    EndPlay(false, 99999);
                    return false;
                }
            }

            CurrentMap[target.Key].SetSkillBlock(target.Value);
        }

        return true;
    }

    public void EndPlay(bool isWin, int score)
    {
        IsWin = isWin;
        Score = score;

        WorldManager.Instance.ChangeGameState(DataDefine.GAME_STATE.RESULT);
    }

    public BlockData GetBlockData(int index)
    {
        if (CurrentMap == null)
            return null;

        return CurrentMap[index];
    }
}
