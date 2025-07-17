using Entities;
using Entities.Interfaces;
using Interfaces;
using UnityEngine;

namespace Effects
{
    public class FreezeEffect : MonoBehaviour, IEffect
    {
        private GameObject _effectPrefab;
        public int LevelEffect { get; private set; }
        private int _tickIndex = 0;
        public float Duration { get; private set; } // its Tick per repeat. This mean "Number" times need to repeat to end that effect
        public GameObject InstantiateEffect { get; private set; }
        public EffectType Type { get; private set; }
        public bool Enabled { get; private set; }

        private float _freezeStrength = 0.9f;
        private float _preFreezeAttackSpeed;
        private float _preFreezeSpeed;
        private IStats _stats;

        public void Init(GameObject effectPrefab, int levelEffect, float duration)
        {
            InstantiateEffect = Instantiate(effectPrefab);
            LevelEffect = Mathf.Min(levelEffect, 3);
            Enabled = true;
        
            Duration = duration; 
        
            Type = EffectType.Freeze;
        }
        public void ApplyOnTick(IHealth health, IStats stats)
        {
            if (_tickIndex == 0)
            {
                _stats = stats;
            
                _preFreezeSpeed = _stats.Speed;
                _preFreezeAttackSpeed = _stats.AttackSpeed;
            
            }
        
            stats.AttackSpeed = _preFreezeAttackSpeed * (_freezeStrength * 1/LevelEffect);
            stats.Speed = _preFreezeSpeed * (_freezeStrength * 1/LevelEffect);

            _tickIndex++;
        }
        public bool TryEndEffect()
        {
            if (!(_tickIndex >= Duration)) return false;
        
            _stats.Speed = _preFreezeSpeed;
            _stats.AttackSpeed = _preFreezeAttackSpeed;
        
            Enabled = false;
            Destroy(InstantiateEffect);
        
            return true;

        }
        public void Refresh(int level, float duration)
        {
            Duration += duration;
            LevelEffect += Mathf.Min(level, 3);
            if (LevelEffect > 3) LevelEffect = 3;
        }
    }
}
