using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Default,    //fatto
    Boundary,   //fatto       
    PowerUp,
    Destructible
}
public class Blocks : MonoBehaviour
{
    const int enumSize = 4;
    public Vector2 pos;
    public MeshRenderer ms;

    [SerializeField] public BlockType type;

    public Color[] blockColors = new Color[enumSize];
    

}
