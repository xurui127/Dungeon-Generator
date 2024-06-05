using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLevel_", menuName = "Scriptable Objects/Dungeon/Gungeon Level")]
public class DungeonRoomSO : ScriptableObject
{
    [Space(10)]
    [Header("Basic Level Details")]
    [Tooltip("For Level name")]
    public string levelName;

    [Header("Room Templates for level")]
    [Tooltip("Populate the list with the room templates that you want to be part of the level." +
             "You need to ensure that room templates are included for all room node types that " +
             "are specified in the Room Node Graphs for the level.")]
    
    public List<RoomTemplateSO> roomTemplateList;

    [Header("Room Node Graphs For Level")]
    [Tooltip("Populate this list with the room node graphs which should be randomly selected from for the level.")]
    public List<RoomNodeGraphSO> roomNodeGraphList;

    #region
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(levelName), levelName);
        if (HelperUtilities.ValidateCheckEnumervaleValues(this,nameof(roomTemplateList),roomTemplateList))
        {
            return;
        }
        if (HelperUtilities.ValidateCheckEnumervaleValues(this, nameof(roomNodeGraphList), roomNodeGraphList))
        {
            return;
        }

        //Check to make sure that room templates are specified for all the node types in the specified the graphs

        //First check that north/south corridor, east/west corridor and entrance types have been specified
        bool isEWCorridor = false;
        bool isNSCorridor = false;
        bool isEntrance = false;

        foreach (var roomTemplateSO in roomTemplateList)
        {
            if (roomTemplateSO == null)
            {
                return;
            }
            if (roomTemplateSO.roomNodeType.isCorridorEW)
            {
                isEWCorridor = true;
            }
            if (roomTemplateSO.roomNodeType.isCorridorNS)
            {
                isNSCorridor = true;
            }
            if (roomTemplateSO.roomNodeType.isEntrance)
            {
                isEntrance = true;
            }



        }
            if (isEWCorridor == false)
            {
                Debug.Log("In " + this.name.ToString() + " No E/W Corridor Room type specified");
            }
            if (isNSCorridor == false)
            {
                Debug.Log("In " + this.name.ToString() + " No N/S Corridor Room type specified");
            }
            if (isEntrance == false)
            {
                Debug.Log("In " + this.name.ToString() + " No Entrance Room type specified");
            }

            foreach(var roomNodeGraph in roomNodeGraphList)
            {
                if (roomNodeGraph == null)
                {
                    return;
                }

                foreach (var roomNodeSO in roomNodeGraph.roomNodeList)
                {
                    if (roomNodeSO == null)
                    {
                        continue;
                    }
                    // Check that a room template has been specified for each roomNode type

                    // Corridors and entrance alrady check 
                    if (roomNodeSO.roomNodeType.isEntrance || roomNodeSO.roomNodeType.isCorridorEW || roomNodeSO.roomNodeType.isCorridorNS||
                        roomNodeSO.roomNodeType.isCorridor || roomNodeSO.roomNodeType.isNone)
                    {
                        continue;
                    }

                    bool isRoomNodeTypeFound = false;

                    // Loop through all room templates to check that this node type has been specified
                    foreach (RoomTemplateSO roomTemplateSO in roomTemplateList)
                    {

                        if (roomTemplateSO == null)
                            continue;

                        if (roomTemplateSO.roomNodeType == roomNodeSO.roomNodeType)
                        {
                            isRoomNodeTypeFound = true;
                            break;
                        }

                    }

                    if (!isRoomNodeTypeFound)
                    {
                        Debug.Log("In " + this.name.ToString() + " No room template " + roomNodeSO.roomNodeType.name.ToString() +
                                  " found for node graph " + roomNodeGraph.name.ToString());
                    }
                }

            }
        }
    }
#endif
    #endregion


