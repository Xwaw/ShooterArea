using System;
using Cinemachine;
using Entities;
using Entities.Interfaces;
using Interfaces;
using UnityEngine;

namespace Effects
{
    public class BurnEffect : MonoBehaviour, IEffect
    {
        public int LevelEffect { get; private set; }
        private int _tickIndex = 0;
        public float Duration { get; private set; } // its Tick per repeat. This mean "Number" times need to repeat to end that effect
        
        public GameObject InstantiateEffect { get; private set; }
        
        public EffectType Type { get; private set; }
        public bool Enabled { get; private set; }

        public void Init(GameObject effectPrefab, int levelEffect, float duration)
        {
            InstantiateEffect = Instantiate(effectPrefab);
            LevelEffect = Mathf.Min(levelEffect, 3);
            Enabled = true;
            
            Duration = duration; 
            
            Type = EffectType.Burn;
        }

        public void ApplyOnTick(IHealth health, IStats stats)
        {
            var baseDamage = GetBaseBurnDamage(_tickIndex, LevelEffect);
            var finalDamage = Mathf.Max(1, baseDamage - (int) stats.Defence);  
            
            if (finalDamage > 0)
            {
                health.TakeDamage(finalDamage); 
            }

            _tickIndex++;
        }
 
        public bool TryEndEffect()
        {
            if (!(_tickIndex >= Duration)) return false;
            Enabled = false;
            Destroy(InstantiateEffect);
            return true;

        }
        private static int GetBaseBurnDamage(int index, int lvl)
        {
            int power = (int)Mathf.Pow(2, Mathf.Min(index, 6));
            return lvl switch
            {
                1 => lvl * power,
                2 => lvl * power - 2,
                3 => lvl * power - 3,
                _ => lvl * power - 3
            };
        }

        public void Refresh(int level, float duration)
        {
            Duration += duration;
            LevelEffect += Mathf.Min(level, 3);
            if (LevelEffect > 3) LevelEffect = 3;
        }
    }
}