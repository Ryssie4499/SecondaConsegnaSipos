using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Matrix")]
    public const int sizeX = 31;
    const int sizeY = 31;

    [Header("Blocks")]
    public GameObject block;
    public Blocks[,] myGrid = new Blocks[sizeX, sizeY];
    
    void Awake()
    {
        InitializeMatrix();
    }

    void InitializeMatrix()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                BlockSpawn(x, y);
            }
        }
    }
    void BlockSpawn(int x, int y)//posizione di spawn
    {
        if (x == 0 || x == sizeX - 1)
        {
            InstBlock(x, y);
            myGrid[x, y].type = BlockType.Boundary;
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];
            var NewObjName = myGrid[x,y];
            NewObjName.name = "Boundary";
        }
        else if (y == 0 || y == sizeY - 1)
        {
            InstBlock(x, y);
            myGrid[x, y].type = BlockType.Boundary;
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];
            var NewObjName = myGrid[x, y];
            NewObjName.name = "Boundary";
        }
        else if (x % 3 == 0 && y % 3 == 0 && x != 0 && y != 0 && x != sizeX - 1 && y != sizeY - 1)
        {
            InstBlock(x, y);
            myGrid[x, y].type = BlockType.Default;
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];
            var NewObjName = myGrid[x, y];
            NewObjName.name = "Default";
        }
        else if ((y == (int)(Mathf.Sin(2 * x) + 10) * 2 ||
            y == (int)(Mathf.Sin(2 * x) + 4) * 4||
            y == (int)(Mathf.Cos(2 * x) + 2) * 8||
            y == (int)(Mathf.Cos(2 * x) + 14) * 2||
            x == (int)(Mathf.Sin(2 * y) + 10) * 2||
            x == (int)(Mathf.Sin(2 * y) + 4) * 4||
            x == (int)(Mathf.Cos(2 * y) + 2) * 8||
            x == (int)(Mathf.Cos(2 * y) + 14) * 2))
        {
            InstBlock(x, y);
            myGrid[x, y].type = BlockType.Destructible;
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];
            var NewObjName = myGrid[x, y];
            NewObjName.name = "Destructible";
        }
    }
    void InstBlock(int c, int r)
    {
        GameObject tmp = Instantiate(block);
        tmp.transform.SetParent(this.transform);
        tmp.transform.localPosition = new Vector3(c, r, 0);
        Blocks blockTmp = tmp.GetComponentInChildren<Blocks>();

        blockTmp.pos = new Vector2(c, r);
        myGrid[c, r] = blockTmp;
    }

}
