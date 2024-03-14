using UnityEngine;
using System.Collections;

public class DoTEffect : TimedEffect
{
    private float currentTick = 0f;
    private Health healthComponent;
    private Scriptable_DoT effectDoT;
    public DoTEffect(EffectBase _effect, GameObject _obj) : base(_effect, _obj)
    {
        effectDoT = (Scriptable_DoT)effect;
        sprite = effectDoT.sprite;
        //Get health component of attached object
        healthComponent = obj.GetComponent<Health>();
    }

    protected override void ApplyEffect()
    {

    }

    public override void Tick(float delta)
    {
        currentTick -= delta;
        if (currentTick <= 0)
        {
            DealDamage();
            currentTick = effectDoT.tickRate;
        }
        base.Tick(delta);
        
    }

    public override void Activate()
    {
        if (effect.stackable && effectStacks < effect.stackCap || duration <= 0)
        {
            ApplyEffect();
            effectStacks++;
        }
        if (effect.refreshable || duration <= 0)
        {
            currentTick = 0f;
            duration = effect.duration;
        }
    }

    public void DealDamage()
    {
        Random.Range(effectDoT.minDamage, effectDoT.maxDamage);
        //Debug.Log("Tick " + Mathf.RoundToInt((Random.Range(effectDoT.minDamage, effectDoT.maxDamage) * (1 + (effectDoT.stackDamageIncrease * (effectStacks - 1))))));

        healthComponent.TakeDamage(Mathf.RoundToInt((Random.Range(effectDoT.minDamage, effectDoT.maxDamage) * (1 + (effectDoT.stackDamageIncrease * (effectStacks - 1))))));
    }


    public override void End()
    {
        effectStacks = 0;
    }
}

