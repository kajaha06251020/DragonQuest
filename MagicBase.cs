//TODO:ポケモン風の英語版13回を見て特別な計算式の攻撃を実装する
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MagicBase : ScriptableObject
{
    //魔法のマスターデータ
    //名前、詳細、属性、威力、消費MP
    [SerializeField] new string name;
    [TextArea]
    [SerializeField] List<BattleMagicElements> elements;
    [SerializeField] List<MovingMagic> Movingmagic;
    [SerializeField] int minPower;
    [SerializeField] int maxPower;
    [SerializeField] int consumeMP;
    [SerializeField] EnemyBase enemyBase;
    [SerializeField] DragonQuestPlayer player;

    //他ファイルから[SerializeField]の内容を参照するためにプロパティを取得する
    //[SerializeField]はprivateなので[SerializeField]のみだと他ファイルから参照できない
    public string Name { get => name; }
    public int MinPower { get => minPower; }
    public int MaxPower { get => maxPower; }
    public int ConsumeMP { get => consumeMP; }
    public Enemy EnemyBase { get; set; }
    public DragonQuestPlayer Player { get; set; }

    public bool ContainsBattleElement(BattleMagicElements element)
    {
        return elements.Contains(element);
    }

    public bool ContainsMovingElement(MovingMagic elements)
    {
        return Movingmagic.Contains(elements);
    }

    public bool IsRula
    {
        get
        {
            return ContainsMovingElement(MovingMagic.Rula);
        }
    }

    public bool IsToheros
    {
        get
        {
            return ContainsMovingElement(MovingMagic.Toheros);
        }
    }

    public bool IsTramana
    {
        get
        {
            return ContainsMovingElement(MovingMagic.Tramana);
        }
    }

    public bool IsFromi
    {
        get
        {
            return ContainsMovingElement(MovingMagic.Fromi);
        }
    }

    public bool IsHeal
    {
        get
        {
            return ContainsMovingElement(MovingMagic.Heal);
        }
    }

    public bool IsRevival
    {
        get
        {
            return ContainsMovingElement(MovingMagic.Revival);
        }
    }

    public bool IsMovingAll
    {
        get
        {
            return ContainsMovingElement(MovingMagic.Rula);
        }
    }

    public bool IsMovingSolo
    {
        get
        {
            return ContainsMovingElement(MovingMagic.Solo);
        }
    }
    
    //バトル時の魔法設定
    public bool IsAllFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.All);
        }
    }
    
    public bool IsGroupFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Group);
        }
    }

    public bool IsSoloFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Solo);
        }
    }

    public bool IsDebuffFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Debuff);
        }
    }

    public bool IsBuffFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Buff);
        }
    }

    public bool IsMoveFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Move);
        }
    }

    public bool IsRevivalFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Revival);
        }
    }

    public bool IsHealFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Heal);
        }
    }

    public bool IsMPAbsorbFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.MPabsorb);
        }
    }

    public bool IsBunishFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Bunish);
        }
    }

    public bool IsConfuseFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Confuse);
        }
    }

    public bool IsSleepFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Sleep);
        }
    }

    public bool IsBlindFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Blind);
        }
    }

    public bool IsDeathFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Death);
        }
    }

    public bool IsExplosionFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Explosion);
        }
    }

    public bool IsElectricFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Electric);
        }
    }

    public bool IsIoFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Io);
        }
    }

    public bool IsWindFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Wind);
        }
    }

    public bool IsIceFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Ice);
        }
    }

    public bool IsGhilaFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Ghila);
        }
    }

    public bool IsMelaFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.Mela);
        }
    }

    public bool IsNoneFlag
    {
        get
        {
            return ContainsBattleElement(BattleMagicElements.None);
        }
    }
}

