using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SO_BaseRoom", menuName = "Room/BaseRoom")]
public class BaseRoom : ScriptableObject
{
    public GameObject roomPrefab;
    public Vector2 spawPoint;
    public bool visited = false;
    public bool[] doors = new bool[4];
}
