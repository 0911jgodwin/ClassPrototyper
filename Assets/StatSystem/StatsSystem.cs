using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSystem
{
    private readonly Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();

    public StatsSystem(BaseStats baseStats)
    {
        foreach (var stat in baseStats.Stats)
        {
            stats.Add(stat.StatType, new Stat(stat.Value));
        }
    }

    public void AddModifier(StatType type, StatModifier modifier)
    {
        if (!stats.TryGetValue(type, out Stat stat))
        {
            stat = new Stat(type);
            stats.Add(type, stat);
        }

        stat.AddModifier(modifier);
    }

    public Stat GetStat(StatType type)
    {
        if(!stats.TryGetValue(type, out Stat stat))
        {
            stat = new Stat(type);
            stats.Add(type, stat);
        }

        return stat;
    }

    public float GetStatValue(StatType type)
    {
        if (!stats.TryGetValue(type, out Stat stat))
        {
            stat = new Stat(type);
            stats.Add(type, stat);
        }

        return stat.Value;
    }

    public void RemoveModifier(StatType type, StatModifier modifier)
    {
        if (!stats.TryGetValue(type, out Stat stat))
        {
            return;
        }

        stat.RemoveModifier(modifier);
    }
}
