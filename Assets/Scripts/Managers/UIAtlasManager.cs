using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UIAtlasManager : MonoBehaviour
{
    [SerializeField] private SpriteAtlas[] atlasDatas;

    public DataDefine.SKIN_TYPE UISkinType { get; private set; }

    public Sprite[] SkillSprites { get; private set; }
    public Sprite[] SkillCountSprites { get; private set; }

    public Sprite EmptyBlockSprite { get; private set; }
    public Sprite TargetBlockSprite { get; private set; }
    public Sprite TrapBlockSprite { get; private set; }

    private void Awake()
    {
        SpriteAtlas commonAtlas = atlasDatas[0];
        EmptyBlockSprite = commonAtlas.GetSprite("block_" + DataDefine.BLOCK_TYPE.EMPTY.ToString().ToLower());
        TargetBlockSprite = commonAtlas.GetSprite("block_" + DataDefine.BLOCK_TYPE.TARGET.ToString().ToLower());
        TrapBlockSprite = commonAtlas.GetSprite("block_" + DataDefine.BLOCK_TYPE.TRAP.ToString().ToLower());

        SkillSprites = new Sprite[(int)DataDefine.SKILL_TYPE.MAX];
        SkillCountSprites = new Sprite[(int)DataDefine.SKILL_TYPE.MAX];
        SetUISkin(DataDefine.SKIN_TYPE.BASIC);
    }

    private void SetUISkin(DataDefine.SKIN_TYPE changeType)
    {
        UISkinType = changeType;

        SpriteAtlas atlas = atlasDatas[(int)changeType];
        string skillSpriteName = "skill_{0}";
        string countSpriteName = "count_{0}";
        int index = 0;

        foreach (string type in System.Enum.GetNames(typeof(DataDefine.SKILL_TYPE)))
        {
            if (string.Equals(type, "MAX"))
                break;

            SkillSprites[index] = atlas.GetSprite(string.Format(skillSpriteName, type.ToLower()));
            SkillCountSprites[index] = atlas.GetSprite(string.Format(countSpriteName, type.ToLower()));
            index++;
        }
    }
}
