using UnityEngine;

public class Doorway
{
    public Vector2Int position;
    public Orientation orientation;
    public GameObject doorPrefab;

    [Header("The Upper Left Position To Start Copying From")]
    public Vector2Int doorwayStartCopyPosition;

    [Header("The width of tiles in the doorway to copy over")]
    public int doorwayCopyTileWidth;

    [Header("The height of tiles in the doorway to copy over")]
    public int doorwayCopyTileHeight;

    [HideInInspector] public bool isConnected = false;
    [HideInInspector] public bool isUnavailable = false;

}
