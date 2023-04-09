using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Default,
    Boundary,
    Destructible
}
public class Blocks : MonoBehaviour
{
    [Header("Block Color")]
    public MeshRenderer ms;
    public Color[] blockColors = new Color[enumSize];

    [Header("Block Position & Type")]
    public Vector2 pos;
    public BlockType type;
    const int enumSize = 3;
}
