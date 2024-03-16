using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BattleEnemyUnit : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] EnemyBase enemyBase;
    [SerializeField] int level;
    [SerializeField] TextMeshProUGUI RightCursor;


    public Enemy Enemy { get; set; }
    public EnemyBase EnemyBase { get; set; }
    public int Level { get; set; }

    Image image;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    //バトルで使うキャラを保持
    public void Setup(Enemy enemy)
    {
        Enemy = enemy;
        //Enemyからレベルに応じた仲間と主人公を生成する
        GetComponent<Image>().sprite = Enemy.EnemyBase.Enemyimage;
        //BattleSystemで使うからプロパティにいれる
    }

    //ダメージを受けた時のアニメーション(4回点滅(周期))
    public void DamageAnimation()
    {
        image.DOFade(endValue:0f,duration:0.125f).SetLoops(4,LoopType.Restart);
    }
}