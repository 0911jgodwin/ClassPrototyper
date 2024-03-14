using UnityEngine;

public abstract class EffectBase : ScriptableObject
{
    //Name of our effect
    public string effectName;

    //How long the effect lasts
    public float duration;

    //Is the effect refreshed if reapplied?
    public bool refreshable;

    //Does the effect stack if reapplied?
    public bool stackable;

    //How many times can it stack?
    public int stackCap;

    public abstract IEffect InitialiseEffect(GameObject _obj);
}
