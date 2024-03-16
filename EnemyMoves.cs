using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoves : ScriptableObject
{
    [SerializeField] List<Magic> magics;
    [SerializeField] List<Skill> skills;
    [SerializeField] DragonQuestPlayer dragonQuestPlayer;
    [SerializeField] EnemyBase enemyBase;

    public DragonQuestPlayer DragonQuestPlayer { get; set; }
    public EnemyBase EnemyBase { get; set; }
}
