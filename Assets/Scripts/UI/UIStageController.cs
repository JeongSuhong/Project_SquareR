using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageController : UIControllerBase
{
    [SerializeField] private GameObject blockPrefab;

    private UIStageBlock[,] blockObjects;
    private RectTransform mapGridRect;

    protected override void Start()
    {
        base.Start();

        mapGridRect = transform.Find("Group_Map").GetComponentInChildren<GridLayoutGroup>().GetComponent<RectTransform>();
    }

    protected override void UpdateUI(DataDefine.GAME_STATE state)
    {
        if (state == DataDefine.GAME_STATE.STAGE)
        {
            SetStageUI();
            canvas.enabled = true;
        }
        else if (canvas.enabled)
            canvas.enabled = false;
    }

    private void SetStageUI()
    {
        DataDefine.BLOCK_TYPE[,] map = StageManager.Instance.MapData;

        int count = map.GetLength(0);

        if (mapGridRect.childCount == 0)
            blockObjects = InitBlocks(count);

        for (int x = 0; x < count; x++)
        {
            for (int y = 0; y < count; y++)
                blockObjects[x, y].SetBlockData(map[x, y]);
        }
    }

    private UIStageBlock[,] InitBlocks(int count)
    {
        UIStageBlock[,] result = new UIStageBlock[count, count];
        string blockName = "block_{0}_{1}";

        for(int x = 0; x < count; x++)
        {
            for (int y = 0; y < count; y++)
            {
                GameObject block = Instantiate(blockPrefab, mapGridRect);
                block.name = string.Format(blockName, x, y);
                result[x, y] = block.GetComponent<UIStageBlock>();
            }
        }

        return result;
    }
}
