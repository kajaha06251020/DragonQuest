using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class DamageDetail
{
    public Skill Skill { get; set; }
    public Magic Magic{get;set;}
    public BattleDialogBox battleDialogBox{get; set; }

    public bool Fainted{get;set;}

    public float Critical{get;set;}

    public float SkillType{get;set;}

    public float MagicType{get;set;}
}
