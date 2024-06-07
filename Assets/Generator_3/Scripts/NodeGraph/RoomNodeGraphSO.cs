using System;
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

    private void Awake()
    {
        LoadRoomNodeDicitionary();
    }
    /// <summary>
    /// Load the room node dictionary from the room node list.
    /// </summary>
    private void LoadRoomNodeDicitionary()
    {
        roomNodeDictionary.Clear();

        // Populate dictionary
        foreach (var node in roomNodeList)
        {
            roomNodeDictionary[node.id] = node;
        }
    }
    /// <summary>
    /// Get room node by roomNodeType
    /// </summary>
    public RoomNodeSO GetRoomNode(RoomNodeTypeSO roomNodeType)
    {
        foreach (var node in roomNodeList)
        {
            if (node.roomNodeType == roomNodeType)
            {
                return node;
            }
        }
        return null;
    }

    public RoomNodeSO GetRoomNode(string roomNodeID)
    {
        if (roomNodeDictionary.TryGetValue(roomNodeID,out RoomNodeSO roomNode))
        {
            return roomNode;
        }
        return null;
    }
    /// <summary>
    ///  Get child nodes for supplied parent room node 
    /// </summary>
    public IEnumerable<RoomNodeSO> GetChildRoomNodes(RoomNodeSO parentRoomNode)
    {
        foreach (var childNodeID in parentRoomNode.childRoomNodeIDList)
        {
            yield return GetRoomNode(childNodeID);
        }
    }
    #region Editor Code
#if UNITY_EDITOR

    [HideInInspector] public RoomNodeSO roomNodeToDrawLineFrom = null;
    [HideInInspector] public Vector2 linePosition;

    // Repopulate node dictionary every time a change is made in the editor 
    public void OnValidate()
    {
        LoadRoomNodeDicitionary();
    }

    public void SetNodeToDrawConnectionLineFrom(RoomNodeSO node,  Vector2 position)
    {
        roomNodeToDrawLineFrom = node;
        linePosition = position;
    }
#endif
#endregion
}
