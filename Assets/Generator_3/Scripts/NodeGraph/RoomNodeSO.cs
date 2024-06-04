using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class RoomNodeSO : ScriptableObject
{
    // current room node id, id will be system generated id -> GUID. 
    [HideInInspector] public string id;

    // Save current RoomNode parent Nodes.
    [HideInInspector] public List<string> parentRoomNodeIDList = new();

    // Save current RoomNode child Nodes.
    [HideInInspector] public List<string> childRoomNodeIDList = new();

    // Save current Node Graph
    [HideInInspector] public RoomNodeGraphSO roomNodeGraph;

    // Save all types of room nodes
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;

    // Save current RoomNode type.
    public RoomNodeTypeSO roomNodeType;

    #region Editor Code

#if UNITY_EDITOR

    [HideInInspector] public Rect rect;
 
    public void Initialise(Rect rect, RoomNodeGraphSO nodeGraph, RoomNodeTypeSO roomNodeType)
    {
        this.rect = rect;
        this.id = Guid.NewGuid().ToString();
        this.name = "RoomNode";
        this.roomNodeGraph = nodeGraph;
        this.roomNodeType = roomNodeType;

        //Load room node type list
        roomNodeTypeList = GameResources.Instance.roomNodetypeList;
    }

    public void Draw(GUIStyle nodeStyle)
    {
        //Draw Node Box Using Begin Area 
        GUILayout.BeginArea(rect, nodeStyle);

        // Start Region to detect Popup Selection Changes 
        EditorGUI.BeginChangeCheck();

        //Display a popup using the roomnode type name values that can be selected from (default to the currently set roomNodeType)\
        int selected = roomNodeTypeList.list.FindIndex(x => x == roomNodeType);

        int selection = EditorGUILayout.Popup("", selected, GetRoomNodeTypesToDisplay());

        roomNodeType = roomNodeTypeList.list[selection];

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(this);
        }
        //close the draw node
        GUILayout.EndArea();

    }

    //Populate a string array with the room node types to display that can be selected
    private string[] GetRoomNodeTypesToDisplay()
    {
        var roomArray = new string[roomNodeTypeList.list.Count];

        for (int i = 0; i < roomNodeTypeList.list.Count; i++)
        {
            if (roomNodeTypeList.list[i].displayInNodeGraphEditor)
            {
                roomArray[i] = roomNodeTypeList.list[i].roomNodeTypeName;
            }
        }
        return roomArray;
    }
#endif

    #endregion

}
