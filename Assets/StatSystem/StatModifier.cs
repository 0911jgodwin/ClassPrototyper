using UnityEngine;

public class StatModifier
{
    [SerializeField] private StatModType modType;
    [SerializeField] private float value;

    public StatModifier(StatModType _modType, float _value)
    {
        modType = _modType;
        value = _value;
    }

    public float Value => value;

    public StatModType ModType => modType;
}

public enum StatModType
{
    Flat = 1,
    PercentAdditive = 2,
    PercentMultiplicative = 3,
}
