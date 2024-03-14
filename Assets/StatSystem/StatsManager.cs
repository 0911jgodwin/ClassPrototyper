using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private BaseStats baseStats = null;
    [SerializeField] private StatType test;
    public StatModifier statMod;
    public StatModifier statMod2;

    private StatsSystem statsSystem;

    private void Start()
    {
        statsSystem = new StatsSystem(baseStats);
        statMod = new StatModifier(StatModType.Flat, 0.1f);
        statMod2 = new StatModifier(StatModType.PercentMultiplicative, 0.1f);
    }

    public StatsSystem StatsSystem
    {
        get
        {
            if (statsSystem != null) { return statsSystem; }

            statsSystem = new StatsSystem(baseStats);
            return statsSystem;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            statsSystem.AddModifier(test, statMod2);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            statsSystem.RemoveModifier(test, statMod2);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log(statsSystem.GetStatValue(test));
        }
    }
}
