using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField] StationsDataBase dataBase;
    [SerializeField] private GameObject stationPrefab;
    private TextMeshProUGUI stationPrefabText;

    void Start()
    {
        stationPrefabText = stationPrefab.transform.GetChild(0).GetComponent<Transform>().GetChild(0).GetComponent<TextMeshProUGUI>();
        GenerateField();
    }

    void Update()
    {
        
    }

    public void GenerateField()
    {
        foreach (var station in dataBase.data)
        { 
            stationPrefabText.SetText(station.station_name);
            GameObject stationObj = Instantiate(stationPrefab, new Vector3(station.pos_x * 50.0f, station.pos_y * -50.0f, 0), Quaternion.identity);
            stationObj.name = station.station_name;
        }
    }
}
