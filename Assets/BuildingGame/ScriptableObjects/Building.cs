using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Bootcamp/Building")]
public class Building : ScriptableObject
{
    public string BuildingName;
    public int Cost;
    public GameObject Prefab;
}
