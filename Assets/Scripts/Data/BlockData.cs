using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData
{
    public DataDefine.BLOCK_TYPE BlockType { get; private set; }
    public DataDefine.SKILL_TYPE SkillType { get; private set; }
    
    public BlockData()
    {
        BlockType = DataDefine.BLOCK_TYPE.EMPTY;
        SkillType = DataDefine.SKILL_TYPE.TEMP;
    }

    public BlockData(BlockData clone)
    {
        BlockType = clone.BlockType;
        SkillType = clone.SkillType;
    }

    public BlockData(DataDefine.BLOCK_TYPE blockType)
    {
        BlockType = blockType;
        SkillType = DataDefine.SKILL_TYPE.TEMP;
    }

    public void SetSkillBlock(DataDefine.SKILL_TYPE skillType)
    {
        BlockType = DataDefine.BLOCK_TYPE.SKILL;
        SkillType = skillType;
    }
}
