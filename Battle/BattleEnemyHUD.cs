using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleEnemyHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EnemyNameText;
    [SerializeField] TextMeshProUGUI EnemyNumberText;
    [SerializeField] Enemy enemy;

    public void setData(EnemyBase enemyBase)
    {
        EnemyNameText.text = enemyBase.Name;
        EnemyNumberText.text = enemy.EnemyNumber.ToString()+"ひき";
    }
}
