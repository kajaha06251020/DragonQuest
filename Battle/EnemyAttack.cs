using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] BattleEnemyUnit battleEnemyUnit;
    [SerializeField] BattlePlayerUnit battlePlayerUnit;
    [SerializeField] List<MagicBase> まほうList;
    [SerializeField] List<SkillBase> とくぎList;
    [SerializeField] List<EnemyAttack> こうげき;

    public BattlePlayerUnit Player;
    public BattleEnemyUnit Enemy;
    public BattleDialogBox dialogBox;
    public BattleState battleState;
    public BattleSystem battleSystem;
    public BattlePlayerHUD battlePlayerHUD{get;set;}

    public List<Magic> MagicList { get; set; }
    public List<Skill> SkillList { get; set; }
    public float Critical = 1f;
    public PlayerParty playerParty;

    //コンストラクター
    public EnemyAttack(BattlePlayerUnit battlePlayerUnit,BattleEnemyUnit battleEnemyUnit)
    {
        Player = battlePlayerUnit;
        Enemy = battleEnemyUnit;
    }

    //敵が味方にダメージを与える時のダメージ計算
    public DamageDetail CauseDamagePlayer(DragonQuestPlayer attacker, Enemy enemy)
    {
        var DamageDetail = new DamageDetail()
        {
            Fainted = false
        };
        float BaseDamage = enemy.EnemyBase.attack / 2 - attacker.defense / 4;
        int DamageRange = (int)(BaseDamage / 16) + 1;
        int minDamage = (int)BaseDamage - DamageRange;
        int maxDamage = (int)BaseDamage + DamageRange;
        int actualDamage = Mathf.RoundToInt(Random.Range(0.85f, 1.0f) * (Random.Range(minDamage, maxDamage)));
        attacker.hp -= actualDamage;
        battlePlayerHUD.UpdateHP(playerParty.playerIndex);

        if (attacker.hp <= 0)
        {
            DamageDetail.Fainted = true;
        }
        return DamageDetail;
    }

    public IEnumerator PerformEnemyAttack(float waiting)
    {
        battleState = BattleState.EnemyMove;
        yield return dialogBox.TypeDialog($"{battleEnemyUnit.Enemy.EnemyBase.Name}のこうげき！");
        yield return new WaitForSeconds(waiting);
        //ダメージ計算をする
        battleSystem.CauseDamageEnemy(battlePlayerUnit.DPlayer, battleEnemyUnit.Enemy);
        yield return dialogBox.TypeDialog($"{battlePlayerUnit.DPlayer.Name}は{battleEnemyUnit.Enemy.EnemyBase.Name}にダメージを与えた！");
        var damageDetail = CauseDamagePlayer(battlePlayerUnit.DPlayer, battleEnemyUnit.Enemy);
        if (damageDetail.Fainted)
        {
            yield return dialogBox.TypeDialog($"{battlePlayerUnit.DPlayer.Name}はちからつきた。");
        }
        else
        {
            //TODO:じぶんたちがやられなかったらじぶんのターンに移行
        }
    }
}