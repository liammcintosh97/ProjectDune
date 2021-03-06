﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resistances : Stats
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

  protected override void InitStats(){
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

  #region Public Static Methods

  public static Resistances operator +(Resistances a, Resistances b){
    Resistances sum = new Resistances();

    Dictionary<DamageType, DamageSource>.KeyCollection keys = a.Dictionary.Keys;

    foreach (DamageType rt in keys) {

      float newValue = a.Dictionary[rt].value + b.Dictionary[rt].value;
      sum.Dictionary[rt].value = Mathf.Clamp(newValue, minStat, maxStat);
    }

    return sum;
  }

  public static Resistances operator -(Resistances a, Resistances b){
    Resistances sum = new Resistances();

    Dictionary<DamageType, DamageSource>.KeyCollection keys = a.Dictionary.Keys;

    foreach (DamageType rt in keys){
      float newValue = a.Dictionary[rt].value - b.Dictionary[rt].value;
      sum.Dictionary[rt].value = Mathf.Clamp(newValue, minStat, maxStat);
    }

    return sum;
  }

  public DamageSources FilterDamage(DamageSources toFilter) {

    Dictionary<DamageType, DamageSource>.KeyCollection keys = toFilter.Dictionary.Keys;

    foreach (DamageType rt in keys) {
      toFilter.Dictionary[rt].value -= (_dictionary[rt].value / 100) * toFilter.Dictionary[rt].value;
    }

    return toFilter;
  }

  public override void Randomize() {

    foreach (DamageSource r in _dictionary.Values){
      r.value = Random.Range(minStat, maxStat);
    }

  }

  #endregion
}
