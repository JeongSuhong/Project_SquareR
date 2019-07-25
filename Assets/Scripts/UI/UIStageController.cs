using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageController : UIControllerBase
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject skillPrefab;

    private UIStageBlock[,] mapBlocks;
    private GridLayoutGroup mapGrid;

    private UIStageSkill[] skills;
    private RectTransform skillGridRect;

    private int[] mapIndexForSkill;
    private UIDragSkill dragSkill;

    private delegate void OnUpdateMapBlockDelegate(DataDefine.SKILL_TYPE type, List<UIStageBlock> targetBlockList, bool isPreview);
    private OnUpdateMapBlockDelegate OnUpdateMapBlock;
    private delegate void OnUpdateSkillDelegate(int addCount);
    private OnUpdateSkillDelegate OnUpdateSkill;

    protected override void Start()
    {
        base.Start();

        mapGrid = transform.Find("Group_Map").GetComponentInChildren<GridLayoutGroup>();
        skillGridRect = transform.Find("Group_Skills").GetComponentInChildren<HorizontalLayoutGroup>().GetComponent<RectTransform>();

        mapIndexForSkill = new int[2];
        dragSkill = GetComponentInChildren<UIDragSkill>(true);

        PlayManager.Instance.OnUpdateMap += SetBlocksUI;
    }

    protected override void UpdateUI(DataDefine.GAME_STATE state)
    {
        if (state == DataDefine.GAME_STATE.STAGE)
        {
            SetSkillsUI();
            canvas.enabled = true;
        }
        else if (canvas.enabled)
            canvas.enabled = false;
    }

    #region Set Map

    private void SetBlocksUI(List<BlockData> mapData)
    {
        int index = 0;

        if (mapGrid.transform.childCount == 0)
            mapBlocks = InitMapBlocks(DataDefine.BlockMaxCount);

        for (int column = 0; column < DataDefine.BlockMaxCount; column++)
        {
            for (int row = 0; row < DataDefine.BlockMaxCount; row++)
            {
                mapBlocks[column, row].SetBlock(index, mapData[index]);
                index++;
            }
        }
    }

    private UIStageBlock[,] InitMapBlocks(int rowCount)
    {
        UIStageBlock[,] result = new UIStageBlock[rowCount, rowCount];
        string blockObjectName = "block_{0}_{1}";

        for (int column = 0; column < rowCount; column++)
        {
            for (int row = 0; row < rowCount; row++)
            {
                GameObject blockObj = Instantiate(blockPrefab, mapGrid.transform);
                blockObj.name = string.Format(blockObjectName, column, row);
                UIStageBlock block = blockObj.GetComponent<UIStageBlock>();
                result[column, row] = block;
                OnUpdateMapBlock += block.UpdateBlock;
            }
        }

        return result;
    }

    #endregion

    #region Set Skill

    private void SetSkillsUI()
    {
        Dictionary<DataDefine.SKILL_TYPE, int> skill = PlayManager.Instance.CurrentSkill;
        int skillCount = skill.Count;
        int index = 0;

        if (skill.Count < 4)
            skillCount = 4;

        if (skills == null)
            skills = InitSkills(skillCount);

        foreach (DataDefine.SKILL_TYPE skillType in skill.Keys)
        {
            skills[index].SetSkill(skillType, skill[skillType], StartDragSkill);
            index++;
        }

        for (int i = index; i < skills.Length; i++)
            skills[i].SetEmptySkill();
    }

    private UIStageSkill[] InitSkills(int count)
    {
        UIStageSkill[] result = new UIStageSkill[count];
        string skillObjectName = "skill_{0}";
        int createCount = count - skillGridRect.childCount;

        for (int i = 0; i < createCount; i++)
        {
            GameObject skill = Instantiate(skillPrefab, skillGridRect);
            skill.name = string.Format(skillObjectName, i);
            result[i] = skill.GetComponent<UIStageSkill>();
        }

        return result;
    }

    private void StartDragSkill(UIStageSkill targetSkill)
    {
        OnUpdateSkill = targetSkill.SetCount;

        dragSkill.OnDrag = PreviewSkill;
        dragSkill.OnEndDrag = EndDragSkill;
        dragSkill.View(targetSkill.Type, targetSkill.SkillBG.sprite);
    }

    private void PreviewSkill(Vector2 movePos)
    {
        int[] moveIndex = GetMapIndexForPosition(movePos);

        if (mapIndexForSkill == moveIndex)
            return;
        else if (mapIndexForSkill != null && moveIndex != null && mapIndexForSkill[0] == moveIndex[0] && mapIndexForSkill[1] == moveIndex[1])
            return;

        mapIndexForSkill = moveIndex;
        SetMapForSkill(dragSkill.CurrentType, true);
    }

    private void EndDragSkill()
    {
        if (mapIndexForSkill == null)
            OnUpdateSkill?.Invoke(1);

        SetMapForSkill(dragSkill.CurrentType, false);
        OnUpdateSkill = null;
    }

    private int[] GetMapIndexForPosition(Vector2 movePos)
    {
        Vector2 gridPos = mapGrid.transform.InverseTransformPoint(movePos);
        Vector2 moveIndex = gridPos / (mapGrid.cellSize + mapGrid.spacing);
        moveIndex.y *= -1;

        if (moveIndex.x < 0 || moveIndex.y < 0 || moveIndex.x >= DataDefine.BlockMaxCount || moveIndex.y >= DataDefine.BlockMaxCount)
            return null;
        else
        {
            int[] result = new int[2] { (int)moveIndex.y, (int)moveIndex.x };
            return result;
        }
    }

    private void SetMapForSkill(DataDefine.SKILL_TYPE skillType, bool isPreivew)
    {
        List<UIStageBlock> targetList = new List<UIStageBlock>();

        if (mapIndexForSkill == null)
            OnUpdateMapBlock?.Invoke(DataDefine.SKILL_TYPE.TEMP, targetList, isPreivew);
        else
        {
            targetList.Add(mapBlocks[mapIndexForSkill[0], mapIndexForSkill[1]]);
            Dictionary<string, int[]> rangeDic = new Dictionary<string, int[]>();
            int[] rangeIndex = new int[2];

            // Debug.Log(string.Format("Target Block {0} / {1}", mapIndexForSkill[0], mapIndexForSkill[1]));

            switch (skillType)
            {
                case DataDefine.SKILL_TYPE.YELLOW:
                    rangeDic.Add("row", new int[2] { -1, 1 });
                    rangeDic.Add("column", new int[2] { -1, 1 });
                    break;
                case DataDefine.SKILL_TYPE.RED:
                    rangeDic.Add("column", new int[2] { -5, 5 });
                    break;
                case DataDefine.SKILL_TYPE.GREEN:
                    rangeDic.Add("row", new int[2] { -5, 5 });
                    break;
                case DataDefine.SKILL_TYPE.BLUE:
                    rangeDic.Add("diagonalL", new int[2] { -1, 1 });
                    rangeDic.Add("diagonalR", new int[2] { -1, 1 });
                    break;
                case DataDefine.SKILL_TYPE.RAINBOW:
                    rangeDic.Add("row", new int[2] { -5, 5 });
                    rangeDic.Add("column", new int[2] { -5, 5 });
                    break;
                default:
                    break;
            }

            foreach (KeyValuePair<string, int[]> range in rangeDic)
            {
                for (int i = range.Value[0]; i <= range.Value[1]; i++)
                {
                    if (i == 0)
                        continue;

                    rangeIndex[0] = mapIndexForSkill[0];
                    rangeIndex[1] = mapIndexForSkill[1];

                    switch (range.Key)
                    {
                        case "row":
                            rangeIndex[1] += i;
                            break;

                        case "column":
                            rangeIndex[0] += i;
                            break;

                        case "diagonalR":
                            rangeIndex[0] += i;
                            rangeIndex[1] += i;
                            break;

                        case "diagonalL":
                            rangeIndex[0] -= i;
                            rangeIndex[1] += i;
                            break;
                    }

                    if (rangeIndex[0] < 0 || rangeIndex[0] >= DataDefine.BlockMaxCount || rangeIndex[1] < 0 || rangeIndex[1] >= DataDefine.BlockMaxCount)
                        continue;

                    targetList.Add(mapBlocks[rangeIndex[0], rangeIndex[1]]);
                }
            }

            if (!isPreivew)
            {
                Dictionary<int, DataDefine.SKILL_TYPE> datas = new Dictionary<int, DataDefine.SKILL_TYPE>();
                for (int i = 0; i < targetList.Count; i++)
                    datas.Add(targetList[i].Index, skillType);

                PlayManager.Instance.SetMapDataForSkill(datas);
            }

            OnUpdateMapBlock?.Invoke(skillType, targetList, isPreivew);
        }
    }
    #endregion

    public void OnClickViewOption()
    {
        UIManager.Instance.Option.View();
    }
}
