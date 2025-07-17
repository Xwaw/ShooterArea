using System;
using System.Collections.Generic;
using System.Linq;
using Core.Effect;
using Effects;
using Entities.Interfaces;
using Interfaces;
using Scriptable_Objects_Data.DataStores;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Entities
{
    public enum EffectType
    {
        Burn, Freeze, Poison
    }
    public class EffectEntityManager : MonoBehaviour
    {
        [SerializeField] private EffectPrefabData effectsPrefab;
        
        private List<IEffect> _effects = new ();
        private Transform _objectTransform;
        private IHealth _health;
        private IStats _stats;

        private const float TickInterval = 0.5f;
        private float _timer;
        private void Awake()
        {
            _timer = TickInterval;
            
            _stats = GetComponent<IStats>();
            _health = GetComponent<IHealth>();
            
            _objectTransform = GetComponent<Transform>();
        }
        private void Update()
        {
            if (_effects == null) return;
            
            RenderEffect();

            _timer -= Time.deltaTime;

            if (!(_timer <= 0f)) return;
            _timer = TickInterval;
                
            for (var i = _effects.Count - 1; i >= 0; i--)
            {
                var effect = _effects[i];
                effect.ApplyOnTick(_health, _stats);

                effect.TryEndEffect();
                    
                if (!effect.Enabled)
                {
                    _effects.RemoveAt(i);
                }
            }
        }

        public void AddEffect(EffectType type, float duration, int level)
        {
            var existing = _effects.FirstOrDefault(e => e.Type == type);
            existing?.Refresh(Mathf.Min(level, 3), duration);

            var effect = EffectFactory.Create(type);
            if (effect == null)
            {
                Debug.LogWarning($"Effect type {type} not implemented.");
                return;
            }
            if (existing != null) return;
            var prefabEffect = effectsPrefab.GetPrefab(type);
            
            effect.Init(prefabEffect, level, duration);
            _effects.Add(effect);
        }

        private void RenderEffect()
        {
            foreach (var effect in _effects)
            {
                var prefabEffect = effect.InstantiateEffect;
                prefabEffect.transform.position = _objectTransform.transform.position;
            }
        }
    }
}