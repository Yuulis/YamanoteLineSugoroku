using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;



public class StationsDataBase : ScriptableObject
{
    public StationsData[] data;
}


[System.Serializable]
public class StationsData
{
    public int station_cd;
    public int station_g_cd;
    public string station_name;
    public int pos_x;
    public int pos_y;
}