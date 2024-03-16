using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CursorMove : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RightCursor;

    public BattleEnemyHUD battleEnemyHUD;
    public BattleSystem battleSystem;

    public GameObject ActionSelector;
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

    BattleState BattleState;
    ActionState ActionState;
    MovingState MovingState;
    // public int currentAction; //0なら戦う、1なら作戦、2ならいれかえ、3なら逃げる
    // int currentMove; //0ならこうげき、1ならまほう、2ならとくぎ、3ならぼうぎょ、
    // //4ならアイテム、5ならそうび
    // int currentMagic;//(4行2列)0は左上、1はその下、2その下、3は左下
    // //4は右上、5はその下、6はその更にした、7は右下
    // int currentSkill;//(4行2列)0は左上、1はその下、2その下、3は左下
    // //4は右上、5はその下、6はその更にした、7は右下
    // int currentItem;
    // int currentTarget;これらはBattleDialogBoxのなかにある

    public void ActionSelectorMoveCursor()
    {
        Vector2 actionCursor = new Vector2(-675,74);
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            actionCursor.y -= 56;
            transform.position = actionCursor;
            if(actionCursor.y == -38)
            {
                actionCursor.y = 74;
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            actionCursor.y += 56;
            transform.position = actionCursor;
            if(actionCursor.y == 74)
            {
                actionCursor.y = -90;
            }
        }
        
    }

    public void MoveSelectorMoveCursor()
    {
        Vector2 MoveCursor = new Vector2(-675,5);
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(battleSystem.currentMove == 0)
            {
                MoveCursor.y -= 50;
            }

        }
    }




}
