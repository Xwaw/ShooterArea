using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Scriptable_Objects_Data.DataStores
{
    [CreateAssetMenu(fileName = "EffectPrefabs", menuName = "Effect")]
    public class EffectPrefabData : ScriptableObject
    {
        [System.Serializable]
        public class EffectPrefabEntry
        {
            public EffectType type;
            public GameObject prefab;
        }
        
        public EffectPrefabEntry[] effects;
        
        public GameObject GetPrefab(EffectType type)
        {
            foreach (var entry in effects)
            {
                if (entry.type == type)
                    return entry.prefab;
            }
            return null;
        }
    }
}