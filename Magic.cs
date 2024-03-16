using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO:英語版13回最後を見てconsumeMP分MPを減らすようにする

public enum BattleMagicElements
{
    None,
    Mela,//メラ
    Ghila,//ギラ属性
    Ice,//ヒャド属性
    Wind,//バギ属性
    Io,//イオ属性
    Electric,//デイン属性
    Explosion,//メガンテ属性
    Death,//ザキ属性
    Blind,//マヌーサ属性
    Sleep,//ラリホー属性
    Confuse,//メダパニ属性
    Bunish,//二フラム属性
    MPabsorb,//マホトラ属性
    Heal,//回復属性
    Revival,//蘇生まほう
    Move,//移動専用魔法
    Buff,//バフ魔法
    Debuff,//デバフ魔法
    All,//全体魔法
    Group,
    Solo
}

public enum MovingMagic
{
    Rula,
    Toheros,
    Tramana,
    Fromi,
    Heal,
    Revival,
    All,
    Solo,
}

public class Magic
{
    //魔法を使うときの魔法データ
    //魔法のマスターデータをMagicBase.csから取得する
    //使いやすいようにするために消費MPも設定する

    //ここのBaseはMagicBase.csから取得しているMagicBaseである
    public MagicBase MBase { get; set; }
    public int ConsumeMP { get; set; }
    public EnemyBase enemyBase { get; set; }
    public BattleEnemyUnit enemyUnit { get; set; }
    public BattlePlayerUnit playerUnit { get; set; }
    public int MinPower { get; set; }
    public int MaxPower { get; set; }
    public ElementChart elementChart{get;set;}
    public BattleMagicElements element{get;set;}
    public EnemyBase.EnemyType enemyType{get;}
    public DamageDetail damageDetail{get;set;}

    //初期設定
    //ここのMbaseはMagicBase.csを引数としているMagicBaseだから上とは別の名前にしなければならない
    public Magic(MagicBase Mbase, int consumemp)
    {
        MBase = Mbase;
        consumemp = Mbase.ConsumeMP;//ここは同じ名前の物でもいいらしい
    }
    //敵に魔法ダメージを与えるときのダメージ計算(固定値)
    public bool CauseMagicDamageEnemy(DragonQuestPlayer attacker, EnemyBase.EnemyType enemy)
    {
        //相性
        float Magictype = ElementChart.MagicGetEffectiveness(element/*魔法属性*/,enemyType/*敵のタイプ*/);
        int minDamage = MinPower;
        int maxDamage = MaxPower;
        int actualDamage = Mathf.RoundToInt(Random.Range(0.85f, 1.0f) * (Random.Range(minDamage, maxDamage))* Magictype);
        enemyUnit.EnemyBase.hp -= actualDamage;
        if (enemyUnit.EnemyBase.hp <= 0)
        {
            return true;
        }
        return false;
    }

    //味方が魔法ダメージを受けるときのダメージ計算(固定値)
    public DamageDetail CauseMagicDamagePlayer(DragonQuestPlayer attacker, EnemyBase enemy)
    {
        var damageDetail = new DamageDetail()
        {
            Fainted = false
        };
        int minDamage = MinPower;
        int maxDamage = MaxPower;

        int actualDamage = Mathf.RoundToInt(Random.Range(0.85f, 1.0f) * (Random.Range(minDamage, maxDamage)));
        attacker.hp -= actualDamage;
        if (attacker.hp <= 0)
        {
            attacker.hp = 0;
            damageDetail.Fainted = true;
        }
        return damageDetail;
    }
}
