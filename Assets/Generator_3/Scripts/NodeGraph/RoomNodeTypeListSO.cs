using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RoomNodeTypeListSO",menuName = "Scriptable Objects/Dungeon/Room Node type List")]
public class RoomNodeTypeListSO : ScriptableObject
{
    [Space(10)]
    [Header("Room Node Type List")]
    [Tooltip("This list should be use with all the RoomNodeTypeSO for the game - it is used instead of an enum")]
    public List<RoomNodeTypeSO> list;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumervaleValues(this, nameof(list), list);
    }
#endif
    #endregion
}
