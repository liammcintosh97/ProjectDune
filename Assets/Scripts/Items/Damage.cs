using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damage{

  public DamageTypes sources;

  public void Deal(Health health,Resistances resistances) {

    DamageTypes filteredDamageTypes = Filter(resistances);
    float totalDamage = filteredDamageTypes.CalculateTotalDamage();

    health.Subtract(totalDamage);
  }

  #region Private Methods
  private DamageTypes Filter(Resistances resistances){

    DamageTypes filtered = sources;

    filtered.Piercing -= (resistances.Piercing / 100) * filtered.Piercing;
    filtered.Blunt -= (resistances.Blunt / 100) * filtered.Blunt;
    filtered.Ballistic -= (resistances.Ballistic / 100) * filtered.Ballistic;
    filtered.Explosive -= (resistances.Explosive / 100) * filtered.Explosive;
    filtered.Slashing -= (resistances.Slashing / 100) * filtered.Slashing;
    filtered.Fire -= (resistances.Fire / 100) * filtered.Fire;
    filtered.Acid -= (resistances.Acid / 100) * filtered.Acid;
    filtered.Electric -= (resistances.Electric / 100) * filtered.Electric;

    return filtered;
  }

  #endregion
}
