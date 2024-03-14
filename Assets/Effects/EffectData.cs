using UnityEngine;

[System.Serializable]
public class EffectData
{
    [SerializeField]
    public EffectBase effect;
    [SerializeField]
    public EffectTarget target;
    [SerializeField]
    public float applicationChance;
}

