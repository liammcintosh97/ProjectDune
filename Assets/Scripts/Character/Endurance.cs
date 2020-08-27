using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Endurance {

  public float max;

  public float Current {
    get
    {
      return Mathf.Clamp(current, 0, max);
    }
    set
    {
      current = value;
      Mathf.Clamp(current, 0, max);
    }
  }

  private float current;


  #region Public Methods

  public float Drain(float amount) {

    if (current <= 0) return Current;

    return Current -= amount * Time.deltaTime;
  }

  public float Heal(float amount)
  {
    if (current >= max) return Current;

    return Current += amount * Time.deltaTime;
  }

  #endregion
}

