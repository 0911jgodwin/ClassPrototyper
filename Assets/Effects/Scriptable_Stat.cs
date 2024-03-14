using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Utility/StatChange")]
public class Scriptable_Stat : EffectBase
{
    public StatType stat;
    public float modifier;
    public StatModType modifierType;
    [HideInInspector]
    public StatModifier statModifier;
    public override IEffect InitialiseEffect(GameObject obj)
    {
        statModifier = new StatModifier(modifierType, modifier);
        StatEffect effect = new StatEffect(this, obj);
        return effect;
    }
}
