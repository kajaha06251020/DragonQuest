using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattleDialogBox : MonoBehaviour
{
    //役割：dialogのTextを取得して、変更する
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] int letterPerSecond;

    [SerializeField] BattleSystem battlesystem;

    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;//こうげき、とくぎ、まほう、どうぐ、そうび、ぼうぎょの欄
    [SerializeField] GameObject StrategySelector;//さくせんをへんこうする
    [SerializeField] GameObject ChangeMemberSelector;//いれかえ、そうがえの選択
    [SerializeField] GameObject RunSelector;
    [SerializeField] GameObject MagicDetail;
    [SerializeField] GameObject SkillDetail;

    [SerializeField] List<TextMeshProUGUI> actionTexts;//たたかう、さくせん、いれかえ、にげる
    [SerializeField] List<TextMeshProUGUI> SkillTexts;//とくぎ欄
    [SerializeField] List<TextMeshProUGUI> MagicTexts;//まほう欄
    [SerializeField] List<TextMeshProUGUI> ItemTexts;//どうぐ欄
    [SerializeField] List<TextMeshProUGUI> EquipTexts;//そうび欄

    [SerializeField] List<TextMeshProUGUI> UsableMagicText;
    [SerializeField] List<TextMeshProUGUI> UsableSkillText;
    [SerializeField] TextMeshProUGUI ConsumeMPText;//消費MPの表示：X/プレイヤーMP
    [SerializeField] TextMeshProUGUI MagicDetailText;//じゅもんのせつめい
    [SerializeField] TextMeshProUGUI SkillDetailText;//とくぎのせつめい

    //ActionSelectorの行動選択時のテキスト
    [SerializeField] TextMeshProUGUI StrategyText;//さくせん
    [SerializeField] TextMeshProUGUI ChangeMemberText;//いれかえ、そうがえ
    [SerializeField] TextMeshProUGUI RunText;//にげる

    [SerializeField] Text RightCursor;
    [SerializeField] BattleEnemyUnit enemyUnit;
    [SerializeField] BattlePlayerUnit playerUnit;

    [SerializeField] TextMeshProUGUI dyingMessage;

    public TextMeshProUGUI DyingMessage { get; set; }
    public BattleEnemyUnit EnemyUnit { get; set; }
    public BattleSystem battleSystem{get;set;}
    public DragonQuestPlayer DPlayer{get;}
    public Enemy Enemy{get;set;}

    Image image;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    //Textを変更するための関数
    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    //画面に文字を表示して消えるまでの関数
    public void DisplayMessage(string message)
    {
        //メッセージを表示
        //一定時間待機してからメッセージを消すコルーチンを開始
        StartCoroutine(ClearMessageAfterDelay());
    }

    //タイプ形式で文字を表示する(ドラクエのメッセージ的な)
    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach(char letter in dialog)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/letterPerSecond);
        }
    }

    //指定した時間が経過したらメッセージが消える
    public IEnumerator ClearMessageAfterDelay()
    {
        //指定した時間待機
        yield return new WaitForSeconds(0.5f);
        //メッセージをクリアする
        ClearMessage();
    }

    public void ClearMessage()
    {
        dialogText.text = "";
    }

    public IEnumerator DyingDialog()
    {
        if(EnemyUnit.Enemy.EnemyBase.Hp < 1)
        {
            yield return TypeDialog($"{EnemyUnit.Enemy.EnemyBase.Name}をやっつけた！");
            StartCoroutine(ClearMessageAfterDelay());
        }
        if(playerUnit.DPlayer.Base.Hp < 1)
        {
            yield return TypeDialog($"{playerUnit.DPlayer.Base.Name}はちからつきた！");
            StartCoroutine(ClearMessageAfterDelay());
        }
    }

    //
    IEnumerator ShowDamageDialog()
    {
        battleSystem.CauseDamageEnemy(DPlayer,Enemy);
        //ダメージ表示
        yield return TypeDialog($"{EnemyUnit.Enemy.EnemyBase.Name}に${battleSystem.CauseDamageEnemy(DPlayer,Enemy)}のダメージを与えた。");
    }

    public void DamageDialoxBoxAnimation()
    {
        image.transform.DOShakePosition(0.7f,1f,18,20,false,true);
    }

    //UIの表示・非表示を行う関数を作成

    //dialogTextの表示管理
    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    //actionSelectorの表示管理
    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    //moveSelectorの表示管理
    public void EnableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
    }

    public void EnableActionChangeMember(bool enabled)
    {
        ChangeMemberSelector.SetActive(enabled);
    }

    public void EnableStrategySelector(bool enabled)
    {
        StrategySelector.SetActive(enabled);
    }

    public void EnableActionRun(bool enabled)
    {
        RunSelector.SetActive(enabled);
    }



    //最初のたたかうとかを選択するときのカーソルを表示させる。
    public void UpdateRightCursorPosition(int selectAction)
    {
        
    }

    //SerializeFieldでMagicを作らなくても引数でMagicとMagicBaseの要素を持っているから大丈夫
    public void SetMagicNames(List<Magic>magic)
    {
        if (magic != null && magic.Count > 0)
        {
        // magic リストの最初の要素から Base プロパティを取得し、その Name プロパティを使用する
        UsableMagicText[0].text = magic[0].MBase.Name;
        }
        for(int i = 0;i < UsableMagicText.Count;i++)
        {
          //覚えている魔法だけ反映
          if(i < magic.Count)
          {
            UsableMagicText[i].text = magic[i].MBase.Name;
          }
          //魔法を覚えていない場合
          else
          {
            UsableMagicText[i].text = "";
          }
        }
    }

    public void SetSkillNames(List<Skill>skill)
    {
        if(skill != null && skill.Count > 0)
        {
            //特技(skillListの最初の要素から参照されるときに初期化する必要がある。？)
            UsableSkillText[0].text = skill[0].SBase.Name;
        }
        for(int i = 0; i <skill.Count; i++)
        {
            //覚えている特技だけ反映
            if(i < skill.Count)
            {
                UsableSkillText[i].text = skill[i].SBase.Name;
            }
            else
            {
                UsableSkillText[i].text = "";
            }
        }
    }
}
