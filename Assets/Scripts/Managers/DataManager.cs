using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
                instance = new DataManager();

            return instance;
        }
    }

    public List<BlockData> MapData { get; private set; }
    public Dictionary<DataDefine.SKILL_TYPE, int> SkillData { get; private set; }

    private void Awake()
    {
        instance = this;

        // Test : 서버에서 보내주는 Stage의 모든 정보를 받아와서 Parsing 해야함
        int allCount = DataDefine.BlockMaxCount * DataDefine.BlockMaxCount;
        MapData = new List<BlockData>();

        for (int i = 0; i < allCount; i++)
            MapData.Add(new BlockData());

        MapData[5] = new BlockData(DataDefine.BLOCK_TYPE.TARGET);
        MapData[12] = new BlockData(DataDefine.BLOCK_TYPE.TARGET);
        MapData[13] = new BlockData(DataDefine.BLOCK_TYPE.TARGET);
        MapData[22] = new BlockData(DataDefine.BLOCK_TYPE.TARGET);
        MapData[35] = new BlockData(DataDefine.BLOCK_TYPE.TARGET);

        MapData[18] = new BlockData(DataDefine.BLOCK_TYPE.TRAP);


        SkillData = new Dictionary<DataDefine.SKILL_TYPE, int>();
        SkillData.Add(DataDefine.SKILL_TYPE.BLUE, 1);
        SkillData.Add(DataDefine.SKILL_TYPE.YELLOW, 2);
        SkillData.Add(DataDefine.SKILL_TYPE.RED, 1);
        SkillData.Add(DataDefine.SKILL_TYPE.GREEN, 1);
        SkillData.Add(DataDefine.SKILL_TYPE.RAINBOW, 1);
    }
}
