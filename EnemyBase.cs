using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework.Constraints;
[CreateAssetMenu(menuName = "EnemyData")]
public class EnemyBase : ScriptableObject
{
    //名前、説明、画像、ステータス
    //名前
    [SerializeField] new string name;
    //説明
    [SerializeField] string description;
    [SerializeField] EnemyType enemyType;
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
    //経験値
    [SerializeField] public int exp;
    //おとすおかね
    [SerializeField] private int gold;
    //敵画像
    [SerializeField] public Sprite enemyimage;
    //使う技
    [SerializeField] public List<Magic> magic;
    [SerializeField] public List<Skill> skill;

    public BattleEnemyAllAttack battleEnemyAllAttack;

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Hp { get => hp; set => hp = value; }
    public int MaxMp { get => maxMp; set => maxMp = value; }
    public int Mp { get => mp; set => mp = value; }
    public int Power { get => power; set => power = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Toughness { get => toughness; set => toughness = value; }
    public int Agility { get => agility; set => agility = value; }
    public int Physical { get => physical; set => physical = value; }
    public int Wisdom { get => wisdom; set => wisdom = value; }
    public int Fortune { get => fortune; set => fortune = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public Sprite Enemyimage { get => enemyimage; set => enemyimage = value; }
    public List<Magic> Magics { get; set; }
    public List<Skill> Skills { get; set; }
    public int Exp { get; set; }
    public int Gold { get; set; }

    public enum EnemyType
    {
        Human,
        Slime,
        Metal,
        Beast,
        Material,
        Devil,
        Machine,
        Bird,
        Dragon,
        Zombie,
        Warlock,
        Natural,
        God,
    }

    // コンストラクターに引数を追加する
    public EnemyBase(EnemyBase ebase)
    {
        // コピー元の EnemyBase からデータをコピーする
        this.Name = ebase.Name;
        this.MaxHp = ebase.MaxHp;
        this.MaxMp = ebase.MaxMp;
        this.hp = ebase.hp;
        this.mp = ebase.mp;
        this.Attack = ebase.Attack;
        this.Defense = ebase.Defense;
        this.Agility = ebase.Agility;
        this.physical = ebase.physical;
        this.wisdom = ebase.wisdom;
        this.fortune = ebase.fortune;
        this.Description = ebase.Description;
        this.Enemyimage = ebase.Enemyimage;
    }


    //覚える技クラス：どのレベルで魔法を覚えるか
    [Serializable]
    public class LearnableMagic
    {
        [SerializeField] MagicBase _base;
        [SerializeField] int level;
        //ヒエラルキーで設定するからsetは要らない
        public MagicBase Base { get => _base; }
        public int Level { get => level; }
    }
    public class LearnableSkill
    {
        [SerializeField] SkillBase SBase;
        [SerializeField] int level;
        public SkillBase skillBase { get => SBase; }
        public int Level { get => level; }
    }
}