using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creo un enum dei tipi di blocchi spawnabili
public enum BlockType
{
    Default,            //il blocco di default è indistruttibile e fisso nelle posizioni multiple di 3
    Boundary,           //i boundaries sono indistruttibili, insuperabili anche con lo scudo e fanno da contorno alla mappa di gioco
    Destructible        //i destructible sono blocchi distruttibili e posizionati su più serie di sinusoidi
}
public class Blocks : MonoBehaviour
{
    [Header("Block Color")]
    public MeshRenderer ms;                             //da inspector inserisco la mesh del blocco
    public Color[] blockColors = new Color[enumSize];   //assegno un colore ad ogni tipo (secondo l'ordine Default, Boundary e Destructible)

    [Header("Block Position & Type")]
    public Vector2 pos;                                 //do una posizione al blocco
    public BlockType type;                              //gli assegno un tipo
    const int enumSize = 3;                             //e la quantità di colori assegnabili ai tipi di blocchi sono esattamente quanti sono i tipi di blocchi
}
