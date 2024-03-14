using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantEffect : IEffect
{
    public EffectBase effect { get; }
    public Scriptable_Damage effectDamage;
    protected readonly GameObject obj;
    private Health healthComponent;

    public InstantEffect(EffectBase _effect, GameObject _obj)
    {
        effect = _effect;
        obj = _obj;
        healthComponent = obj.GetComponent<Health>();
    }

    public virtual void PerformEffect()
    {
        effectDamage = (Scriptable_Damage)effect;
        DealDamage();
    }

    public void DealDamage()
    {
        Random.Range(effectDamage.minDamage, effectDamage.maxDamage);
        Debug.Log("Damage: " + Mathf.RoundToInt((Random.Range(effectDamage.minDamage, effectDamage.maxDamage))));
        healthComponent.TakeDamage(Mathf.RoundToInt((Random.Range(effectDamage.minDamage, effectDamage.maxDamage))));
    }
}
