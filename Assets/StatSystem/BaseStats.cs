using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Base Stats")]
public class BaseStats : ScriptableObject
{
    [SerializeField] private List<BaseStat> stats = new List<BaseStat>();

    public List<BaseStat> Stats => stats;

    [Serializable]
    public class BaseStat
    {
        [SerializeField] private StatType statType = null;
        [SerializeField] private float value = 0f;

        public StatType StatType => statType;
        public float Value => value;
    }
}
