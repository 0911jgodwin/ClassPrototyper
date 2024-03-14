using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Damage/DoT")]
public class Scriptable_DoT : EffectBase
{
    public float minDamage;
    public float maxDamage;
    public float stackDamageIncrease;
    public float tickRate;
    public Sprite sprite;

    public override IEffect InitialiseEffect(GameObject obj)
    {
        DoTEffect effect = new DoTEffect(this, obj);
        return effect;
    }
}
