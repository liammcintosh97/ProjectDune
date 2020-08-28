using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Piercing, Blunt, Ballistic, Explosive, Slashing, Fire, Acid, Electric }

[System.Serializable]
public class DamageSource
{
  public float value;
  public DamageType type;

  #region Consturctors
  public DamageSource(DamageType t)
  {
    type = t;
  }
  #endregion
}

[System.Serializable]
public class DamageSources: Stats
{
  public DamageSource Piercing = new DamageSource(DamageType.Piercing);
  public DamageSource Blunt = new DamageSource(DamageType.Blunt);
  public DamageSource Ballistic = new DamageSource(DamageType.Ballistic);
  public DamageSource Explosive = new DamageSource(DamageType.Explosive);
  public DamageSource Slashing = new DamageSource(DamageType.Slashing);
  public DamageSource Fire = new DamageSource(DamageType.Fire);
  public DamageSource Acid = new DamageSource(DamageType.Acid);
  public DamageSource Electric = new DamageSource(DamageType.Electric);

  public Dictionary<DamageType, DamageSource> Dictionary { get { return _dictionary; } }

  private Dictionary<DamageType, DamageSource> _dictionary = new Dictionary<DamageType, DamageSource>();

  #region Init

  protected override void InitStats()
  {
    _dictionary.Add(DamageType.Piercing, Piercing);
    _dictionary.Add(DamageType.Blunt, Blunt);
    _dictionary.Add(DamageType.Ballistic, Ballistic);
    _dictionary.Add(DamageType.Explosive, Explosive);
    _dictionary.Add(DamageType.Slashing, Slashing);
    _dictionary.Add(DamageType.Fire, Fire);
    _dictionary.Add(DamageType.Acid, Acid);
    _dictionary.Add(DamageType.Electric, Electric);

  }

  #endregion

  #region Public Methods

  public float CalculateTotalDamage(){
    float total = 0;

    foreach (DamageSource dt in _dictionary.Values) {
      total += dt.value;
    }

    return total;
  }

  public override void Randomize() {
    foreach (DamageSource r in _dictionary.Values)
    {
      r.value = Random.Range(minStat, maxStat);
    }

  }

  #endregion
}

