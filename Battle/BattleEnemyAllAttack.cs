using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyAllAttack
{
    public List<Magic>magics{get;set;}
    public List<Skill>skills{get;set;}
    public BattleEnemyAllAttack battleEnemyAllAttack{get;set;}
    public DragonQuestPlayer DPlayer{get;set;}
    public EnemyBase EnemyBase{get;set;}
    public BattleSystem battleSystem{get;set;}
    public List<EnemyAttack> attack{get;set;}
    public EnemyAttack enemyAttack{get;set;}

    //コンストラクター
    public BattleEnemyAllAttack()
    {
        magics = new List<Magic>();
        skills = new List<Skill>();
        attack = new List<EnemyAttack>();
    }

    public void Attack(DragonQuestPlayer DPlayer,EnemyBase enemyBase)
    {
        enemyAttack.PerformEnemyAttack(0.7f);
    }
}

    /* public bool GetRandomAttack()
    {
        List<object> allattack = new List<object>();

        //MagicリストとSkillリストを統合して一つのリストにする
        allattack.AddRange(battleEnemyAllAttack.magics.ToArray());
        allattack.AddRange(battleEnemyAllAttack.skills.ToArray());

        int randomIndex = UnityEngine.Random.Range(0,allattack.Count);
        if(allattack[randomIndex] is Magic)
        {
            Magic selectedMagic = (Magic)allattack[randomIndex];
            //ダメージ計算
            return selectedMagic.CauseMagicDamagePlayer(DPlayer,EnemyBase);
        }
        if(allattack[randomIndex] is Skill)
        {
            Skill selectedSkill = (Skill)allattack[randomIndex];
            return selectedSkill.CauseSkillDamagePlayer(DPlayer,EnemyBase);
        }
        if(allattack[randomIndex] is EnemyAttack)
        {
            
        }
    }
}*/
