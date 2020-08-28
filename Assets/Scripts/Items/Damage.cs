using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damage{

  public DamageSources sources;

  public void Deal(Health health,Resistances resistances) {

    DamageSources filteredDamageTypes = resistances.FilterDamage(sources);
    float totalDamage = filteredDamageTypes.CalculateTotalDamage();

    health.Subtract(totalDamage);
  }
}
