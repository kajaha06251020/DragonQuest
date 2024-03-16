using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy:MonoBehaviour
{
    [SerializeField] BattleEnemyAllAttack battleEnemyAllAttack;
    [SerializeField] EnemyBase ebase;
    [SerializeField] int level;
    //ベースとなるデータ
    EnemyBase enemyBase;
    public int EnemyNumber;

    public EnemyBase EnemyBase { get; set; }
    public List<Magic> Magics { get; set; }
    public List<Skill> Skills { get; set; }
    public List<EnemyMoves> EnemyMoves { get; set; }

    public DragonQuestPlayer DPlayer { get; set; }


    //コンストラクター：生成時の初期設定(一番最初に実行されるもの)
    public void Init()
    {
        EnemyNumber = Random.Range(1, 5);
        ebase.hp = ebase.MaxHp;
        ebase.mp = ebase.MaxMp;
        Magics = new List<Magic>();
        Skills = new List<Skill>();
    }

    public EnemyMoves GetRandomMove()
    {
        int r = Random.Range(0, EnemyMoves.Count);
        return EnemyMoves[r];
    }

    public void Die()
    {
        if(EnemyBase.Hp <1)
        {
            Destroy(gameObject);
        }
    }


    public DamageDetail GetRandomAttack()
    {
        List<object> allattack = new List<object>();

        //まほうととくぎのリストを統合して一つのリストにする
        allattack.AddRange(battleEnemyAllAttack.magics.ToArray());
        allattack.AddRange(battleEnemyAllAttack.skills.ToArray());

        //ランダムで選択される
        int randomIndex = UnityEngine.Random.Range(0, allattack.Count);
        if (allattack[randomIndex] is Magic)
        {
            Magic selectedMagic = (Magic)allattack[randomIndex];
            return selectedMagic.CauseMagicDamagePlayer(DPlayer, EnemyBase);
        }
        else
        {
            return null;
        }
    }
}
