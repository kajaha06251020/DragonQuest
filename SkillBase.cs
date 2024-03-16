using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillBase", menuName = "SkillBase", order = 0)]
public class SkillBase : ScriptableObject
{
    //スキルのマスターデータ
    //名前、詳細、属性、威力、消費MP
    [SerializeField] new string name;
    [TextArea]
    [SerializeField] List<Skill.BattleSkillType> SkillType;
    [SerializeField] int minPower;
    [SerializeField] int maxPower;
    [SerializeField] int consumeMP;

public string Name { get => name; }
public int MinPower { get => minPower; }
public int MaxPower { get => maxPower; }
public int ConsumeMP { get => ConsumeMP; }

public bool ContainsElement(Skill.BattleSkillType skillType)
{
    return SkillType.Contains(skillType);
}



}
