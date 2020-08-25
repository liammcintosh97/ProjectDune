using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageTypes
{
  public float Piercing;
  public float Blunt;
  public float Ballistic;
  public float Explosive;
  public float Slashing;
  public float Fire;
  public float Acid;
  public float Electric;

  public float CalculateTotalDamage()
  {
    float total = 0;

    total += Piercing;
    total += Blunt;
    total += Ballistic;
    total += Explosive;
    total += Slashing;
    total += Fire;
    total += Acid;
    total += Electric;

    return total;
  }
}

