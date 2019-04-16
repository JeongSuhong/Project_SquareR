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

    public DataDefine.BLOCK_TYPE[,] MapData { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MapData = new DataDefine.BLOCK_TYPE[6, 6];

        // Test
        MapData[0, 3] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[1, 3] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[1, 2] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[2, 5] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[3, 1] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[4, 5] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[5, 0] = DataDefine.BLOCK_TYPE.TARGET;
        MapData[3, 3] = DataDefine.BLOCK_TYPE.TRAP;
    }

    
}
