using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public enum BattleSkillType
    {
        None,
        Mela,
        Gila,
        Io,
        Ice,
        Wind,
        Thunder,
        Explosion,
        Twice,//二回はやぶさぎり
        Moroba,
        Holy,
        Dragon,
        Metal,
        Hober,//浮いている敵に倍率補正
        Double,//2倍せいけんづき
        Random,//みなごろし
        Critical,//まじんぎり
        Group,//グループ
        All,//全体
        GradualDecrease,//ムーンサルト、ブーメラン的な
        Flame,
        Blizzard,
        Death,
        Wipe,//パシルーラ系
        Sleep,
        Heal,
        Confuse,
        Palanoyze,
        Poison,
        SuperPoison,//猛毒
        Blind,
        Break,//1ターン休み
        MagicResistanceDecrease,//ぶきみなひかり
        MPDecrease,
        MPAbcorb,
        ItetsukuHadou,
        NextDamageUp,
        ManeMane,
        BreathReflect,
        DodgeUp,
        NiohDachi,
        Daibougyo,
        summon,
        Water,
        Rock,
        Revival,
        FirstAttack,
    }

    public enum MovingSkillType
    {
        KuchiBue,
        Toheross,
        ShinobiAshi,
        SearchItem,
        SummonShop
    }

    //特技を使うときの特技データ
    //SkillBase.csから取得する
    //使いやすいようにするために消費MPも設定している

    public SkillBase SBase { get; set; }
    public int ConsumeMP { get; set; }
    public EnemyBase enemyBase;
    public DragonQuestPlayer DPlayer;
    public int MinPower { get; set; }
    public int MaxPower { get; set; }
    public ElementChart ElementChart { get; set; }
    public BattleSkillType battleSkillType{get;}
    public EnemyBase.EnemyType enemyType{get;}

    //初期設定(コンストラクタ)
    public Skill(SkillBase Sbase, int Consumemp)
    {
        SBase = Sbase;
        Consumemp = Sbase.ConsumeMP;
    }

    //敵にとくぎダメージを与えるときのダメージ計算(固定値)
    public bool CauseSkillDamageEnemy(DragonQuestPlayer attacker, EnemyBase enemy)
    {
        float SkillType = ElementChart.SkillGetEffectiveness(battleSkillType,enemyType);
        int minDamage = SBase.MinPower;
        int maxDamage = SBase.MaxPower;
        int actualDamage = Mathf.RoundToInt(Random.Range(0.85f, 1.0f) * (Random.Range(minDamage, maxDamage))*SkillType);
        enemyBase.hp -= actualDamage;
        if (enemyBase.hp <= 0)
        {
            return true;
        }
        return false;
    }

    //味方がとくぎダメージを受けるときのダメージ計算(固定値)
    public DamageDetail CauseSkillDamagePlayer(DragonQuestPlayer attacker, EnemyBase enemy)
    {
        var damageDetail = new DamageDetail()
        {
            Fainted = false
        };
        int minDamage = MinPower;
        int maxDamage = MaxPower;

        int actualDamage = Mathf.RoundToInt(Random.Range(0.65f, 1.0f) * (Random.Range(minDamage, maxDamage)));
        DPlayer.hp -= actualDamage;
        if (DPlayer.hp <= 0)
        {
            DPlayer.hp = 0;
            damageDetail.Fainted = true;
        }
        return damageDetail;
    }
}
