using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Room_", menuName = "Scriptable Objects/Dungeon/Room")]

public class RoomTempleSO : ScriptableObject
{
    [HideInInspector] public string guid;

    [Space(10)]
    [Header("Room Prefab")]
    [Tooltip("The gameobject prefab for the room (this will contain all" +
    " the tilemaps for the room and environment game objects)")]
    public GameObject prefab;

    //this is used to regenerate the guid if the so is copied and the prefab is changed
    [HideInInspector] public GameObject previousPrefab;

    [Space(10)]
    [Header("Room Configuration")]
    [Tooltip("The room node type SO. The room types correspond to the room nodes used in the room node graph." +
             "The exceptions being with corridors. In the room node graph there is just one corridor type 'Corridor'." +
             "For the room templates there are 2 corridor node types - CorridorNS and CorridorEW.")]
    public RoomNodeTypeSO roomNodeType;

    [Tooltip("If you imagine a rectangle around the room tilemap that just completely encloses it, the room lower bounds" +
             "represent the bottoim left corner of the rectangle. This should be determined from the tilemap for the room" +
             "(using the coodinate brush pointer to get the tilemap grid position for that bottom left corner(Note: this is " +
             "the local tilemap position and NOT world position))")]
    public Vector2Int lowerBounds;

    [Tooltip("If you imagine a rectangle around the room tilemap that just completely encloses it, the room upper bounds" +
             "represent the bottoim right corner of the rectangle. This should be determined from the tilemap for the room" +
             "(using the coodinate brush pointer to get the tilemap grid position for that bottom right corner(Note: this is " +
             "the local tilemap position and NOT world position))")]
    public Vector2Int upperBounds;

    [SerializeField] public List<Doorway> doorwayList;

    [Tooltip("Each possible spaw position(used for enemies and chests) for the room in tilemap coordinates should be to this array")]
    public Vector2Int[] spawnPositionArray;


    /// <summary>
    /// return the list of Entrances for the room template
    /// </summary>
    /// <returns></returns>
    public List<Doorway> GetDoorwayList()
    {
        return doorwayList; 
    }

    #region Editor Validation

#if UNITY_EDITOR
    // Validate SO fields
    private void OnValidate()
    {
        if (guid =="" || previousPrefab != prefab)
        {
            guid = GUID.Generate().ToString();
            previousPrefab = prefab;
            EditorUtility.SetDirty(this);
        }

        HelperUtilities.ValidateCheckEnumervaleValues(this,nameof(doorwayList),doorwayList);

        //Check spawn positions populated
        HelperUtilities.ValidateCheckEnumervaleValues(this, nameof(spawnPositionArray),spawnPositionArray);
    }
#endif
    #endregion
}

