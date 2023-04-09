using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Matrix")]
    public const int sizeX = 31;                            //do una quantità di colonne in una riga
    const int sizeY = 31;                                   //e una quantità di righe in una colonna

    [Header("Blocks")]
    public GameObject block;                                //assegno da inspector il prefab del blocco al generatore di mappe
    public Blocks[,] myGrid = new Blocks[sizeX, sizeY];     //e creo un array multidimensionale che contenga le informazioni di righe e colonne della mappa
    
    void Awake()
    {
        InitializeMatrix();                                 //inizializzo la matrice formata dai due cicli for
    }

    void InitializeMatrix()                                 //la matrice è formata da due cicli for innestati, uno per colonne e uno per righe
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                BlockSpawn(x, y);                           //richiamo il controllo dei tipi di blocchi
            }
        }
    }
    void BlockSpawn(int x, int y)
    {
        if (x == 0 || x == sizeX - 1)                       //la prima colonna a sinistra e l'ultima a destra è formata interamente da blocchi di tipo boundary
        {
            InstBlock(x, y);                                //richiamo l'istanza del blocco
            myGrid[x, y].type = BlockType.Boundary;         //gli assegno un tipo
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];      //assegno un colore alla mesh
            var NewObjName = myGrid[x,y];                                                           //creo una variabile temporanea a cui assegnare il nome del blocco
            NewObjName.name = "Boundary";
        }
        else if (y == 0 || y == sizeY - 1)                  //la prima riga dal basso e l'ultima in alto sono formate interamente da blocchi di tipo boundary
        {
            InstBlock(x, y);                                //richiamo l'istanza del blocco
            myGrid[x, y].type = BlockType.Boundary;         //gli assegno un tipo
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];      //assegno un colore alla mesh
            var NewObjName = myGrid[x, y];                                                          //creo una variabile temporanea a cui assegnare il nome del blocco
            NewObjName.name = "Boundary";
        }
        else if (x % 3 == 0 && y % 3 == 0 && x != 0 && y != 0 && x != sizeX - 1 && y != sizeY - 1) //tutti i blocchi alle posizioni multiple di 3 che non siano i contorni, saranno indistruttibili di default
        {
            InstBlock(x, y);                                //richiamo l'istanza del blocco
            myGrid[x, y].type = BlockType.Default;         //gli assegno un tipo
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];      //assegno un colore alla mesh
            var NewObjName = myGrid[x, y];                                                          //creo una variabile temporanea a cui assegnare il nome del blocco
            NewObjName.name = "Default";
        }
        else if ((y == (int)(Mathf.Sin(2 * x) + 10) * 2 ||             //tutti i blocchi che si trovano sulle sinusoidi descritte saranno di tipo destructible
            y == (int)(Mathf.Sin(2 * x) + 4) * 4||
            y == (int)(Mathf.Cos(2 * x) + 2) * 8||
            y == (int)(Mathf.Cos(2 * x) + 14) * 2||
            x == (int)(Mathf.Sin(2 * y) + 10) * 2||
            x == (int)(Mathf.Sin(2 * y) + 4) * 4||
            x == (int)(Mathf.Cos(2 * y) + 2) * 8||
            x == (int)(Mathf.Cos(2 * y) + 14) * 2))
        {
            InstBlock(x, y);                                    //richiamo l'istanza del blocco
            myGrid[x, y].type = BlockType.Destructible;         //gli assegno un tipo
            myGrid[x, y].ms.material.color = myGrid[x, y].blockColors[(int)myGrid[x, y].type];      //assegno un colore alla mesh
            var NewObjName = myGrid[x, y];                                                          //creo una variabile temporanea a cui assegnare il nome del blocco
            NewObjName.name = "Destructible";
        }
    }
    void InstBlock(int c, int r)                                        //istanzio i blocchi in determinate posizioni su righe e colonne
    {
        GameObject tmp = Instantiate(block);                            //creo un oggetto temporaneo e ci istanzio il blocco
        tmp.transform.SetParent(this.transform);                        //l'oggetto diventerà figlio del generatore di mappa
        tmp.transform.localPosition = new Vector3(c, r, 0);             //gli assegno una posizione sulla griglia
        Blocks blockTmp = tmp.GetComponentInChildren<Blocks>();         //e ricerco il component Blocks dal figlio per assegnarglielo

        blockTmp.pos = new Vector2(c, r);                               //la posizione del blocco equivarrà a quella data in questa classe
        myGrid[c, r] = blockTmp;                                        //ora la mia griglia contiene le informazioni date all'oggetto temporaneo
    }

}
