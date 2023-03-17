using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject block;
    public const int sizeX = 34;
    const int sizeY = 19;
    
    [SerializeField]
    public Blocks[,] myGrid = new Blocks[sizeX, sizeY];
    
    void Awake()
    {
        InizializeMatrix();
    }

    void InizializeMatrix()
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
        else if (y == 17 && (x == 10||x == 11||x == 16||x == 17 ||x == 28)||
                y == 16 && (x == 11||x == 16||x == 21||x == 28)||
                y == 15 && (x == 5|| x == 11||x == 16||x == 20||x == 28||x == 31)||
                y == 14 && ((x >= 6 && x <= 9)||(x > 10 && x < 15)||x == 16||(x >= 22 && x < 25)||(x >= 28 && x < 32))||
                y == 13 && (x == 3||x == 5||x == 6||x == 16||x == 17||x == 19||x == 21||x == 22||x == 26||x == 27||x == 28||x == 31||x == 32)||
                y == 12 && (x == 4||x == 5||x == 13||x == 14||x == 16||x == 19||x == 20||x == 25||x == 26)||
                y == 11 && (x == 8||x == 11||x == 15||x == 16||x == 25||x == 26) ||
                y == 10 && (x == 3||x == 4||(x >= 6 && x <= 8)|| x == 11||(x >= 20 && x <= 22)||(x >= 26 && x <= 28)||(x >= 30 && x <= 32))||
                y == 9 && (x == 4||x == 10||x == 11||x == 13||x == 16||x == 17||x == 19||x == 20||x == 22||x == 28)||
                y == 8 && ((x >= 1 && x <= 4)||x == 13||x == 14||(x >= 17 && x <= 19)||x == 22||x == 28)||
                y == 7 && ((x >= 6 && x <= 10)||(x >= 25 && x <= 28)||(x >= 30 && x <= 32))||
                y == 6 && (x == 4||x == 10||x == 14||x == 22||x == 23||x == 26)||
                y == 5 && (x == 4||(x >= 7 && x <= 10)||x == 12||x == 14||x == 23||x == 26||x == 30)||
                y == 4 && (x == 4||x == 7||x == 12||(x >= 14 && x <= 18)||(x >= 21 && x <= 23)||(x >= 25 && x <= 28)||x == 30)||
                y == 3 && (x == 4||x == 7||x == 11||x == 16||x == 17||x == 25||x == 28)||
                y == 2 && (x == 6||x == 7||x == 11||x == 17||x == 25)||
                y == 1 && (x == 6||x == 11||x == 12||x == 25))
        {
            InstBlock(x, y);
            myGrid[x, y].type = BlockType.Destructible;
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];
            var NewObjName = myGrid[x, y];
            NewObjName.name = "Destructible";
        }
        else if(x==26&&y==3)
        {
            InstBlock(x, y);
            myGrid[x, y].type = BlockType.Door;
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];
            var NewObjName = myGrid[x, y];
            NewObjName.name = "Door";
        }
    }
    void InstBlock(int c, int r)
    {
        GameObject tmp = Instantiate(block);
        tmp.transform.SetParent(this.transform);
        tmp.transform.localPosition = new Vector3(c, r, 0);
        //tmp.name = c + " : " + r;
        Blocks blockTmp = tmp.GetComponentInChildren<Blocks>();

        //block.name = " " + myGrid[x, y].type;
        blockTmp.pos = new Vector2(c, r);
        myGrid[c, r] = blockTmp;
        //tmp.name = " " + myGrid[c,r].type;
    }

}
