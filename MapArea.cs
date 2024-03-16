using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;
    [SerializeField] List<MapLevel> level = new List<MapLevel>();

    public Enemy GetRandomEnemy()
    {
        var enemy = enemies[Random.Range(0, enemies.Count)];
        enemy.Init();
        return enemy;
    }

    [System.Serializable]
    class MapLevel
    {
        public List<int> mapLevel = new List<int>();
    }
}
