using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattlePlayerHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI mpText;
    [SerializeField] BattleDialogBox dialogBox;

    public BattleDialogBox DialogBox { get; set; }
    public List<DragonQuestPlayer> DPlayers;
    Enemy enemy{get;}
    EnemyAttack enemyAttack{get;set;}

    Image image;
    public int MaxPlayers = 4;

    public void setData(DragonQuestPlayer dragonQuest)
    {
        nameText.text = dragonQuest.Base.Name;//DragonQuestの8行目を利用
        levelText.text = "LV:"+dragonQuest.Level;
        hpText.text = "HP:"+dragonQuest.Hp;
        mpText.text = "MP:"+dragonQuest.Mp;
    }

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    public void UpdateHP(int playerIndex)
    {
        hpText.text = "HP:"+DPlayers[playerIndex].Hp;
    }

    public void PlayerDamagedHUDAnimation()
    {
        image.transform.DOShakePosition(0.7f,1f,18,20,false,true);
    }
}
