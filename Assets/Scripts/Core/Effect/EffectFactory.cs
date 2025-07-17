using Effects;
using Entities;
using Interfaces;
using UnityEngine;

namespace Core.Effect
{
    public static class EffectFactory
    {
        public static IEffect Create(EffectType effectType)
        {
            return effectType switch
            {
                EffectType.Burn => new GameObject("BurnEffect").AddComponent<BurnEffect>(),
                EffectType.Freeze => new GameObject("FreezeEffect").AddComponent<FreezeEffect>(),
                EffectType.Poison => new GameObject("PoisonEffect").AddComponent<PoisonEffect>(),
                _ => null
            };
        }
    }
}