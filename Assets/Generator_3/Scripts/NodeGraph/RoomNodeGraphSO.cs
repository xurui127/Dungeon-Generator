using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeGraph" ,menuName = "Scriptable Objects/Dungeon/Room Node Graph")]
public class RoomNodeGraphSO : ScriptableObject
{
    // For save each room type
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;
   
    // For save each room node in the editor 
    [HideInInspector] public List<RoomNodeSO> roomNodeList = new();
    
    // For save each  id and room node
    [HideInInspector] public Dictionary<string,RoomNodeSO> roomNodeDictionary = new();
}
