using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class DragonQuestPlayer : PlayerBase
{
    [SerializeField] PlayerBase playerBase;
    [SerializeField] int level;
    //ベースとなるデータ
    public PlayerBase Base{get;set;}
    public int HP{get;set;}
    public int MP{get;set;}
    //使える技
    public List<Magic> Magics{get;set;}
    public List<Skill> Skills{get;set;}
    
    public int Level { get;set; }
    public DragonQuestPlayer dragonQuestPlayer { get;set; }

    //レベルアップ時に増加するステータスの上限値
    public const int MaxStatIncrease = 4;

    //レベルに応じたステータスを返すメソッド
    //コンストラクター：生成時の初期設定
    public void Init()
    {
        HP = MaxHp;
        MP = MaxMp;
        Magics = new List<Magic>();
        Skills = new List<Skill>();
    }

    public void LevelUpStats()
    {   
        //レベルアップ時にランダムで増加するステータスを計算
        int hpIncrease = Random.Range(0,MaxStatIncrease +1);
        int mpIncrease = Random.Range(0,MaxStatIncrease +1);
        int powerIncrease = Random.Range(0,MaxStatIncrease +1);
        int toughnessIncrease = Random.Range(0,MaxStatIncrease +1);
        int agilityIncrease = Random.Range(0,MaxStatIncrease +1);
        int physicalIncrease = Random.Range(0,MaxStatIncrease +1);
        int wisdomIncrease = Random.Range(0,MaxStatIncrease +1);
        int fortuneIncrease = Random.Range(0,MaxStatIncrease +1);

        //ステータスを増加
        maxHp += hpIncrease;
        maxMp += mpIncrease;
        power += powerIncrease;
        toughness += toughnessIncrease;
        agility += agilityIncrease;
        physical += physicalIncrease;
        wisdom += wisdomIncrease;
        fortune += fortuneIncrease;
    }
}
