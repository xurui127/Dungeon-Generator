using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{
    [Space(10)]
    [Header("Dungeon Levels")]
    [Tooltip("Populate with the dungeon level scriptable objects")]
    
    [SerializeField] private List<DungeonLevelSO> dungeonLevelList;


    [Tooltip("Populate with the starting dungeon lebel for testing, first level = 0")]
    
    [SerializeField] private int currentDungeonLevelListIndex = 0;

    [HideInInspector] public GameState gameState;

    private void Start()
    {
        gameState = GameState.gameStart;
        PlayDungeonLevel(currentDungeonLevelListIndex);
    }

    private void Update()
    {
        HandleGameStates();
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameState = GameState.gameStart;

        }
    }

    private void HandleGameStates()
    {
        switch(gameState)
        {
            case GameState.gameStart:
                // play first level
                PlayDungeonLevel(currentDungeonLevelListIndex);
                gameState = GameState.playingLevel;
                break;

        }
    }

    private void PlayDungeonLevel(int dungeonLevelListIndex)
    {
        // Build dungeon for level 
        bool dungeonBuiltSuccessfuly = DungeonBuilder.Instance.GenerateDungeon(dungeonLevelList[dungeonLevelListIndex]);

        if (!dungeonBuiltSuccessfuly)
        {
            Debug.LogError("Could not build dungeon from specified roos and node graphs");
        }
    }

    #region  Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumervaleValues(this,nameof(dungeonLevelList),dungeonLevelList);
    }
#endif
    #endregion
}
