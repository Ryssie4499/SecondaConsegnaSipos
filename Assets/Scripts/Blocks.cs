using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Default,    //fatto
    Boundary,   //fatto
    Door,       
    PowerUp,
    Destructible
}
public class Blocks : MonoBehaviour
{
    const int enumSize = 5;
    public Vector2 pos;
    public MeshRenderer ms;

    [SerializeField] public BlockType type;

    public Color[] blockColors = new Color[enumSize];
    void Awake()                
    {
        //InitializeBlock();
    }

    //void InitializeBlock()
    //{
    //    type = (BlockType)Random.Range(0, 5);
    //    //ms.material.color = blockColors[(int)type];

    //    //ms.material.color = Color.white;
    //}
}
