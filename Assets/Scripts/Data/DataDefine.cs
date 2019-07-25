

public static class DataDefine
{
    public const int BlockMaxCount = 6;

    public enum GAME_STATE
    {
        START,
        SELECT,
        STAGE,
        RESULT,
    }

    // Stage
    public enum BLOCK_TYPE
    {
        EMPTY,
        TARGET,
        TRAP,
        SKILL,
        ITEM,
    }

    public enum SKILL_TYPE
    {
        TEMP = -1,
        YELLOW,
        RED,
        GREEN,
        BLUE,
        RAINBOW,
        COUNT,
    }

    public enum ITEM_BLOCK_TYPE
    {
        ADD_TIME,
        ADD_SCORE,
    }

    public enum SKIN_TYPE
    {
        BASIC = 1,
    }

}
