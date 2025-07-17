using System;
using Entities;
using Entities.Interfaces;
using UnityEngine;

namespace Interfaces
{
    public interface IEffect
    {
        GameObject InstantiateEffect { get;}
        EffectType Type { get; }
        float Duration { get; }
        int LevelEffect { get; }
        bool Enabled { get; }
    
        void Init(GameObject effectPrefab, int levelEffect, float duration);
        void ApplyOnTick(IHealth health, IStats stats);
        bool TryEndEffect(); 
        
        void Refresh(int levelEffect, float duration);
    }
}