using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UIAtlasManager : MonoBehaviour
{
    [SerializeField] private SpriteAtlas[] atlasDatas;

    public Sprite TargetSprite { get; private set; }

    private void Awake()
    {
        TargetSprite = atlasDatas[0].GetSprite(DataDefine.BLOCK_TYPE.TARGET.ToString().ToLower());

    }
}
