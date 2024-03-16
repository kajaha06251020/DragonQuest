using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Roam,
    Battle
}

public class GameController : MonoBehaviour
{
    [SerializeField] CharaMove charaMove;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    GameState state;
    MapArea mapArea;

    public PlayerParty playerParty;
    Enemy enemy;
    public CharaMove CharaMove;

    private void Start()
    {
        charaMove.OnEncounted += StartBattle;
        battleSystem.BattleEnd += EndBattle;
    }

    public void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
        var playerParty = CharaMove.GetComponent<PlayerParty>();
        var mapArea = Object.FindFirstObjectByType<MapArea>();
        var enemy = mapArea.GetRandomEnemy();
        battleSystem.StartBattle(playerParty,enemy);
    }

    public void EndBattle()
    {
        state = GameState.Roam;
        battleSystem.gameObject.SetActive(false);
        battleSystem.gameObject.SetActive(true);
    }

    public void Update()
    {
        if(state == GameState.Roam)
        {
            charaMove.HandleUpdate();
        }
        else if(state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
    }
}