using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyContainer", order = 1)]
    public class EnemyContainer : ScriptableObject
    {
        public List<Enemy> enemies;


        public EnemyData GetEnemyData(EnemyType _type)
        {
            return enemies.Find(e => e.type == _type).data;
        }

        [Serializable]
        public struct Enemy
        {
            public EnemyType type;
            public EnemyData data;
        }

        public enum EnemyType
        {
            Normal,
            Hard
        }
    }
}
