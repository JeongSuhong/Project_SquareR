using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
                instance = new StageManager();

            return instance;
        }
    }

    public readonly int BlockMaxCount = 6;

    public DataDefine.BLOCK_TYPE[,] MapData { get; private set; }
    public Dictionary<DataDefine.SKILL_TYPE, int> SkillData { get; private set; }

    public bool IsWin { get; private set; }
    public int Score { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MapData = new DataDefine.BLOCK_TYPE[BlockMaxCount, BlockMaxCount];

        // Test
        MapData[0, 3] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[1, 3] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[1, 2] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[2, 5] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[3, 1] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[4, 5] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[5, 0] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[3, 3] = DataDefine.BLOCK_TYPE.TRAP;

        SkillData = new Dictionary<DataDefine.SKILL_TYPE, int>();
        SkillData.Add(DataDefine.SKILL_TYPE.BLUE, 1);
        SkillData.Add(DataDefine.SKILL_TYPE.YELLOW, 2);
        SkillData.Add(DataDefine.SKILL_TYPE.RED, 1);
        SkillData.Add(DataDefine.SKILL_TYPE.GREEN, 1);
        SkillData.Add(DataDefine.SKILL_TYPE.RAINBOW, 1);
    }


    public void EndPlay(bool isWin, int score)
    {
        IsWin = isWin;
        Score = score;

        WorldManager.Instance.ChangeGameState(DataDefine.GAME_STATE.RESULT);
    }

}
