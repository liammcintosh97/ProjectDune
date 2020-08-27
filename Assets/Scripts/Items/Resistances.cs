using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resistances
{
  public float Piercing;
  public float Blunt;
  public float Ballistic;
  public float Explosive;
  public float Slashing;
  public float Fire;
  public float Acid;
  public float Electric;

  private static float minResistance;
  private static float maxResistance;

  #region Public Static Methods

  public static Resistances operator +(Resistances a, Resistances b){
    Resistances sum = new Resistances();

    sum.Piercing = a.Piercing + b.Piercing;
    sum.Blunt = a.Blunt + b.Blunt;
    sum.Ballistic = a.Ballistic + b.Ballistic;
    sum.Explosive = a.Explosive + b.Explosive;
    sum.Slashing = a.Slashing + b.Slashing;
    sum.Fire = a.Fire + b.Fire;
    sum.Acid = a.Acid + b.Acid;
    sum.Electric = a.Electric + b.Electric;

    sum = Clamp(sum);

    return sum;
  }

  public static Resistances operator -(Resistances a, Resistances b){

    Resistances sum = new Resistances();

    sum.Piercing = a.Piercing - b.Piercing;
    sum.Piercing = a.Blunt - b.Blunt;
    sum.Piercing = a.Ballistic - b.Ballistic;
    sum.Piercing = a.Explosive - b.Explosive;
    sum.Piercing = a.Slashing - b.Slashing;
    sum.Piercing = a.Fire - b.Fire;
    sum.Piercing = a.Acid - b.Acid;
    sum.Piercing = a.Electric - b.Electric;

    sum = Clamp(sum);

    return sum;
  }

  public void Randomize() {
    Piercing = Random.Range(minResistance, maxResistance);
    Blunt = Random.Range(minResistance, maxResistance);
    Ballistic = Random.Range(minResistance, maxResistance);
    Explosive = Random.Range(minResistance, maxResistance);
    Slashing = Random.Range(minResistance, maxResistance);
    Fire = Random.Range(minResistance, maxResistance);
    Acid = Random.Range(minResistance, maxResistance);
    Electric = Random.Range(minResistance, maxResistance);
}

  #endregion

  #region Private Static Methods
  private  static Resistances Clamp(Resistances r){

    r.Piercing = Mathf.Clamp(r.Piercing, minResistance, maxResistance);
    r.Blunt = Mathf.Clamp(r.Blunt, minResistance, maxResistance);
    r.Ballistic = Mathf.Clamp(r.Ballistic, minResistance, maxResistance);
    r.Explosive = Mathf.Clamp(r.Explosive, minResistance, maxResistance);
    r.Slashing = Mathf.Clamp(r.Slashing, minResistance, maxResistance);
    r.Fire = Mathf.Clamp(r.Fire, minResistance, maxResistance);
    r.Acid = Mathf.Clamp(r.Acid, minResistance, maxResistance);
    r.Electric = Mathf.Clamp(r.Electric, minResistance, maxResistance);

    return r;
  }


  #endregion
}
