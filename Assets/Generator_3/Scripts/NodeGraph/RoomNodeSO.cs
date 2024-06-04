using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class RoomNodeSO : ScriptableObject
{
    // current room node id, id will be system generated id -> GUID. 
     [HideInInspector]public string id;

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
    [HideInInspector] public bool isLeftClickDragging = false;
    [HideInInspector] public bool isSelected = false;
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

    public void ProcessEvents(Event currentEvent)
    {
        switch(currentEvent.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;
            case EventType.MouseUp:
                ProcessMouseUpEvent(currentEvent); 
                break;
            case EventType.MouseDrag:
                ProcessMouseDragEvent(currentEvent);
                break;
        }
    }
    #region Process Mouse Drag Events
    private void ProcessMouseDragEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftMouseDragEvent(currentEvent);
        }
    }

    private void ProcessLeftMouseDragEvent(Event currentEvent)
    {
       isLeftClickDragging = true;
        DragNode(currentEvent.delta);
        GUI.changed = true;
    }

    private void DragNode(Vector2 delta)
    {
        rect.position += delta;
        EditorUtility.SetDirty(this);
    }
    #endregion
    #region Process Mouse Up Events
    private void ProcessMouseUpEvent(Event currentEvent)
    {
        // left click up
        if (currentEvent.button == 0)
        {
            ProcessLeftClickUpEvent();
        }
    }

    private void ProcessLeftClickUpEvent()
    {
        if (isLeftClickDragging)
        {
            isLeftClickDragging = false;
        }
    }
    #endregion
    #region Process Mouse Down Events
    // Process mouse down events
    private void ProcessMouseDownEvent(Event currentEvent)
    {
        // left click down
        if (currentEvent.button == 0)
        {
            ProcessLeftClickDownEvent();
        }
        // right click down
        else if (currentEvent.button == 1)
        {
            ProcessRightClickDownEvent(currentEvent);
        }
    }
    /// <summary>
    /// Process right click down
    /// </summary>
    /// <param name="currentEvent"></param>
    private void ProcessRightClickDownEvent(Event currentEvent)
    {
        roomNodeGraph.SetNodeToDrawConnectionLineFrom(this, currentEvent.mousePosition);
    }

    /// <summary>
    /// process left click down event
    /// </summary>
    private void ProcessLeftClickDownEvent()
    {
        // Returns the actual object selection. Includes Prefabs, non-modifiable objects.
        Selection.activeObject = this;

        // toggle node selection
        // isSelected = !isSelected
        if (isSelected == true)
        {
            isSelected = false;
        }
        else
        {
            isSelected = true;
        }
    }
    #endregion

    public bool AddChildRoomNodeIDToRoomNode(string childID)
    {
        childRoomNodeIDList.Add(childID);
        return true;
    }
    public bool AddParentRoomNodeIDToRoomNode(string parentID)
    {
        parentRoomNodeIDList.Add(parentID);
        return true;
    }

#endif

    #endregion

}
