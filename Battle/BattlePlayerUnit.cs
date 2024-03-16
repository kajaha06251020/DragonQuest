using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class BattlePlayerUnit : MonoBehaviour
{
    [SerializeField] DragonQuestPlayer dPlayer;
    [SerializeField] int Level;
    [SerializeField] Enemy enemy;
    [SerializeField] BattleSystem battlesystem;
    [SerializeField] BattleDialogBox dialogBox;

    public DragonQuestPlayer DPlayer { get; set; }
    public Enemy Enemy { get; set; }
    public BattleSystem BattleSystem { get; set; }
    public BattleDialogBox DialogBox{get;set;}
    PlayerParty playerparty;

    //バトルで使うキャラを保持
    public void Setup(DragonQuestPlayer dPlayer)
    {
        //dplayerからレベルに応じた仲間と主人公を生成する
        this.dPlayer = dPlayer;
        //BattleSystemで使うからプロパティにいれる
    }

    public void PerformDefend()
    {
        dialogBox.TypeDialog($"{dPlayer.Name}はみをまもっている。");
        //TODO:敵の攻撃を実装し、計算は下の式で行う。
        BattleSystem.CauseDamagePlayerDefended(DPlayer, enemy);
    }
}
