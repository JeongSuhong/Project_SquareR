

public static class DataDefine
{
    public enum GAME_STATE
    {
        START,
        STAGE,
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

    public enum SKILL_BLOCK_TYPE
    {
        YELLOW,
        RED,
        GREEN,
        BLUE,
        RAINBOW
    }

    public enum ITEM_BLOCK_TYPE
    {
        ADD_TIME,
        ADD_SCORE,
    }

    public enum SKIN_TYPE
    {
        BASIC,
    }
    
}
