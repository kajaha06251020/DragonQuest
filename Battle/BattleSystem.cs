//TODO:ポケモン風講座の第九回の敵のランダム攻撃実装
//TODO:第12回の敵を複数出現するメソッド、EndBattleの実装
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public enum BattleState
{
    Start,//○○があらわれた！
    PlayerAction,//たたかう、さくせん、いれかえ、にげる
    PlayerMove,//技選択
    EnemyMove,//敵の攻撃
    Busy,//コマンド選択後、メッセージが垂れ流しになっている
    End//まものをたおした！けいけんちとゴールドの処理、レベルアップの処理
}

public enum MovingState
{
    Attack,
    Magic,
    Skill,
    Defend,
    Item,
}

public enum ActionState
{
    Start,
    Fight,
    Strategy,
    ChangeMember,
    Run
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleEnemyUnit enemyUnit;
    [SerializeField] BattlePlayerHUD playerHud;
    [SerializeField] BattleEnemyHUD enemyHud;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] BattlePlayerUnit playerUnit;
    [SerializeField] TextMeshProUGUI RightCursor;

    [SerializeField] EnemyMoves Enemymoves;
    [SerializeField] BattleEnemyAllAttack battleEnemyAllAttack;
    //[SerializeField] GameController gameController;
    public UnityAction BattleEnd;

    [SerializeField] EnemyBase EnemyBase;
    [SerializeField] Enemy Enemy;
    [SerializeField] DragonQuestPlayer DPlayer;
    [SerializeField] EnemyAttack EnemyAttack;

    BattleState BattleState;
    ActionState ActionState;
    MovingState MovingState;
    public int currentAction; //0なら戦う、1なら作戦、2ならいれかえ、3なら逃げる
    public int currentMove; //0ならこうげき、1ならまほう、2ならとくぎ、3ならぼうぎょ、
    //4ならアイテム、5ならそうび
    public int currentMagic;//(4行2列)0は左上、1はその下、2その下、3は左下
    //4は右上、5はその下、6はその更にした、7は右下
    public int currentSkill;//(4行2列)0は左上、1はその下、2その下、3は左下
    //4は右上、5はその下、6はその更にした、7は右下
    public int currentItem;
    public int currentTarget;

    public BattleEnemyUnit battleEnemyUnit{get;set;}
    public BattlePlayerUnit PlayerUnit { get; set; }
    public EnemyMoves EnemyMoves { get; set; }
    public BattleEnemyAllAttack BattleEnemyAllAttack { get; set; }
    public BattleDialogBox BattleDialogBox { get; set; }
    public DamageDetail DamageDetail{get;set;}
    public PlayerBase pBase;

    PlayerParty playerParty;
    Enemy enemy;

    private int escapeCount = 0;
    private const float baseEscapeChance = 0.50f;
    private const float maxEscapeChance = 0.875f;

    //メッセージ

    public void StartBattle(PlayerParty playerParty,Enemy enemy)
    {
        this.playerParty = playerParty;
        this.enemy = enemy;
        StartCoroutine(SetUpBattle());
    }

    public void HandleUpdate()
    {
        if (BattleState == BattleState.PlayerAction)
        {
            HandleActionSelector();
        }
        if (BattleState == BattleState.PlayerMove)
        {
            HandleMoveSelector();
        }
    }

    public IEnumerator SetUpBattle()
    {
        BattleState = BattleState.Start;
        //?dialogBox.DyingDialog();
        //モンスターの生成と描画
        enemyUnit.Setup(enemy);
        playerUnit.Setup(playerParty.GetLivingPlayers());
        //HUDの描画
        playerHud.setData(playerUnit.DPlayer);
        enemyHud.setData(enemyUnit.Enemy.EnemyBase);

        yield return dialogBox.TypeDialog($"{enemyUnit.Enemy.EnemyBase.Name}があらわれた!");
        yield return new WaitForSeconds(1);
        PlayerAction();
    }
    IEnumerator DisplayNoLearnedMagicMessageDialog(float duration)
    {
        dialogBox.EnableDialogText(true);
        yield return dialogBox.TypeDialog("なにもおぼえていません！");
        yield return new WaitForSeconds(duration);
        dialogBox.EnableDialogText(false);
    }
    IEnumerator DisplayNoLearnedSkillMessageDialog(float duration)
    {
        dialogBox.EnableDialogText(true);
        yield return dialogBox.TypeDialog("なにもおぼえていません！");
        yield return new WaitForSeconds(duration);
        dialogBox.EnableDialogText(false);
    }

    IEnumerator ShowDamageDetail(DamageDetail damagedetail)
    {
        if(damagedetail.Critical > 1f)
            yield return dialogBox.TypeDialog("会心の一撃！");
    }

    void StartFightAction()
    {
        ActionState = ActionState.Start;
        dialogBox.EnableActionSelector(true);
    }

    void PlayerAction()
    {
        BattleState = BattleState.PlayerAction;
        dialogBox.EnableMoveSelector(true);
        ActionState = ActionState.Fight;
    }

    void ActionStrategy()
    {
        dialogBox.EnableStrategySelector(true);
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            dialogBox.EnableStrategySelector(false);
            PlayerAction();
        }
    }

    void ActionChangeMember()
    {
        dialogBox.EnableActionChangeMember(true);
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            dialogBox.EnableActionChangeMember(false);
            PlayerAction();
        }
    }

    void ActionRun()
    {
        dialogBox.EnableActionRun(true);
        PerformPlayerRun();
    }

    public void PlayerActionStrategy()
    {
        ActionState = ActionState.Strategy;
        dialogBox.EnableStrategySelector(enabled);
        ActionStrategy();
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            dialogBox.EnableStrategySelector(false);
            PlayerAction();
        }
    }

    public void PlayerActionChangeMember()
    {
        ActionState = ActionState.ChangeMember;
        dialogBox.EnableActionChangeMember(enabled);
        ActionChangeMember();
    }

    IEnumerator PlayerActionRun()
    {
        ActionState = ActionState.Run;
        dialogBox.EnableActionRun(enabled);
        ActionRun();
        yield return new WaitForSeconds(1);
    }

    //PerformPlayerAttack(攻撃を選択した時の実行)
    public IEnumerator PerformPlayerMoveAttack(float waiting)
    {
        BattleState = BattleState.Busy;
        yield return dialogBox.TypeDialog($"{DPlayer.Name}のこうげき！");
        battleEnemyUnit.DamageAnimation();
        yield return new WaitForSeconds(waiting);
        yield return ShowDamageDetail(DamageDetail);
        //ダメージ計算をする
        var damageDetail = CauseDamageEnemy(DPlayer, Enemy);
        yield return dialogBox.TypeDialog("${enemyUnit.Enemy.EnemyBase.Name}は${PlayerUnit.DPlayer.Name}にダメージを与えた！");
        if (damageDetail.Fainted)
        {
            Enemy.Die();
            yield return dialogBox.TypeDialog($"{Enemy.EnemyBase.Name}をたおした！");
            
        }
        else
        {
            //TODO:それ以外ならEnemyMoveが始まる
        }
    }

    public IEnumerator PerformPlayerRun()
    {
        BattleState = BattleState.Busy;
        yield return dialogBox.TypeDialog("にげだした！");

    }

    void PlayerMove()
    {
        BattleState = BattleState.PlayerMove;
        dialogBox.EnableDialogText(false);
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableMoveSelector(true);
        if (PlayerUnit.DPlayer.Magics.Count == 0)
        {
            StartCoroutine(DisplayNoLearnedMagicMessageDialog(1.3f));
        }
        if ((PlayerUnit.DPlayer.Skills.Count == 0))
        {
            StartCoroutine(DisplayNoLearnedSkillMessageDialog(1.3f));
        }
    }

    //ActionSelectorの処理(最初の画面の処理)
    void HandleActionSelector()
    {
        //currentActionは0から3の間
        currentAction = Mathf.Clamp(currentAction,0,3);
        //一番上を入力すると、戦うコマンド
        //上から二番目を入力すると作戦コマンド
        //下から二番目を入力すると入れ替えコマンド
        //一番下を入力すると逃げるコマンド
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
            {
                currentAction--;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 3)
            {
                currentAction++;
            }
            if (currentAction == 3)
            {
                currentAction -= 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (currentAction == 0)
            {
                PlayerMove();
            }
            if (currentAction == 1)
            {
                PlayerActionStrategy();
            }
            if (currentAction == 2)
            {
                PlayerActionChangeMember();
            }
            if (currentAction == 3)
            {
                PlayerActionRun();
            }
        }
    }

    //ActionSelectorの処理(最初の画面の処理)
    void HandleMoveSelector()
    {
        //currentMoveは0から5の間
        currentMove = Mathf.Clamp(currentMove,0,5);
        //0:左一番上を入力すると、こうげきコマンド
        //1:左上から二番目を入力するととくぎコマンド
        //2:左一番下を入力するとぼうぎょコマンド
        //3:右一番上を押すととくぎコマンド
        //4:右一番上から二番目を入力するとアイテムコマンド
        //5:右一番下を入力するとそうびコマンド
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 0)
            {
                currentMove--;
            }
            if (currentMove == 0)
            {
                currentMove += 2;
            }
            if (currentMove == 3)
            {
                currentMove += 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < 6)
            {
                currentMove++;
            }
            if (currentMove == 5)
            {
                currentMove -= 2;
            }
            if (currentMove == 2)
            {
                currentMove -= 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 3)
            {
                currentMove -= 3;
            }
            if (currentMove < 2)
            {
                currentMove += 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < 3)
            {
                currentMove += 3;
            }
            if (currentMove > 2)
            {
                currentMove -= 3;
            }
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            dialogBox.EnableMoveSelector(false);
            PlayerAction();
        }

        //行動決定
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            dialogBox.EnableMoveSelector(false);
            //攻撃を選択した時
            if (currentMove == 0)
            {
                PerformPlayerMoveAttack(0.2f);
            }
            if (currentMove == 1)
            {
                //じゅもん欄を開く

            }
            if (currentMove == 2)
            {
                //ぼうぎょする
                playerUnit.PerformDefend();
            }
            if (currentMove == 3)
            {
                //とくぎ欄を開く
            }
            if (currentMove == 4)
            {
                //どうぐインベントリを開く
                //しようしたりできる。
                //メソッド内でアイテムを消去したりするのを追加しておく
            }
            if (currentMove == 5)
            {
                //どうぐインベントリを開いてそうびの変更をする
            }
            //メッセージ再開
            dialogBox.EnableDialogText(true);
            //コマンド決定の処理
        }
    }

    //敵にダメージを与える時のダメージ計算
    public DamageDetail CauseDamageEnemy(DragonQuestPlayer attacker, Enemy enemy)
    {
        float critical = 1f;
        //5%で会心の一撃
        if (Random.value * 100 <= 6.25f)
        {
            critical = 2f;
        }

        var DamageDetail = new DamageDetail()
        {
            Fainted = false,
            Critical = critical
        };

        float BaseDamage = attacker.Attack / 2 - enemy.EnemyBase.defense / 4;

        int DamageRange = (int)(BaseDamage / 16) + 1;
        int minDamage = (int)BaseDamage - DamageRange;
        int maxDamage = (int)BaseDamage + DamageRange;

        int actualDamage = Mathf.RoundToInt(Random.Range(0.85f, 1.0f) * (Random.Range(minDamage, maxDamage)) * critical);
        enemyUnit.EnemyBase.hp -= actualDamage;

        if (enemy.EnemyBase.hp <= 0)
        {
            enemy.EnemyBase.hp = 0;
            DamageDetail.Fainted = false;
        }
        return DamageDetail;
    }


    //防御時に敵が味方にダメージを与える時のダメージ計算
    public DamageDetail CauseDamagePlayerDefended(DragonQuestPlayer attacker, Enemy enemy)
    {
        var DamageDetail = new DamageDetail
        {
            Fainted = false
        };
        float BaseDamage = enemy.EnemyBase.attack / 3 - attacker.defense / 4;
        int DamageRange = (int)(BaseDamage / 16) + 1;
        int minDamage = (int)BaseDamage - DamageRange;
        int maxDamage = (int)BaseDamage + DamageRange;
        int actualDamage = Mathf.RoundToInt(Random.Range(0.45f, 0.55f) * (Random.Range(minDamage, maxDamage)));
        attacker.hp -= actualDamage;

        if (attacker.hp <= 0)
        {
            attacker.hp = 0;
            DamageDetail.Fainted = true;
        }
        return DamageDetail;
    }

    public bool SuccessRun(PlayerBase pBase)
    {
        escapeCount++;
        //闘争の試行回数に応じて成功率を計算
        float escapeChance = Mathf.Min(baseEscapeChance * Mathf.Pow(1.5f,escapeCount - 1),maxEscapeChance);

        //ランダムに成功するかどうかを決定
        bool success = Random.Range(0,1f)< escapeChance;

        return success;

        // if(Random.Range(0,1f) > escapeChance)
        // {
        //     //TODO:敵のターンに移行
        // }
    }
}