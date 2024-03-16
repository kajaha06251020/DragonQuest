using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "PlayerBase")]
public class PlayerBase : ScriptableObject
{
    //名前、説明、画像、ステータス
    [SerializeField] new string name;
    [SerializeField] string description;
    //最大HP
    [SerializeField] public int maxHp;
    //現在HP
    [SerializeField] public int hp;
    //最大MP
    [SerializeField] public int maxMp;
    //現在MP
    [SerializeField] public int mp;
    [SerializeField] public int power;
    //ちから
    [SerializeField] public int attack;
    //攻撃力
    [SerializeField] public int defense;
    //守備力
    [SerializeField] public int toughness;
    //みのまもり
    [SerializeField] public int agility;
    //すばやさ
    [SerializeField] public int physical;
    //かしこさ
    [SerializeField] public int wisdom;
    //うんのよさ
    [SerializeField] public int fortune;
    //これまで獲得した経験値
    [SerializeField] public int exp;
    //覚える魔法
    [SerializeField] public List<LearnableMagic> learnableMagics;
    //覚えるとくぎ
    [SerializeField] private List<LearnableSkill> learnableSkill;
    

    //SerializeFieldで決めた値を取得する。値の設定はSerializeFieldで行っているため不要(set)
    public int MaxHp { get => maxHp; }
    public int Hp { get => hp; }
    public int MaxMp { get => maxMp; }
    public int Mp { get => mp; }
    public int Power { get => power; }
    public int Attack { get => attack; }
    public int Defense { get => defense; }
    public int Toughness { get => toughness; }
    public int Agility { get => agility; }
    public int Physical { get => physical; }
    public int Wisdom { get => wisdom; }
    public int Fortune { get => fortune; }
    public int Exp { get => exp; }
    public List<LearnableMagic> LearnableMagics { get => learnableMagics; }
    public string Name { get => name; }
    public string Description { get => description; }
    public List<LearnableSkill> LearnableSkill { get; }
}

//覚える技クラス：どのレベルで魔法を覚えるか
[Serializable]
public class LearnableMagic
{
    [SerializeField] MagicBase _base;
    [SerializeField] int level;
    //ヒエラルキーで設定するからsetは要らない
    public MagicBase Base { get; }
    public int Level { get; }
}
[Serializable]
public class LearnableSkill
{
    [SerializeField] SkillBase SBase;
    [SerializeField] int level;
    public SkillBase skillBase { get; }
    public int Level { get => level; }
}