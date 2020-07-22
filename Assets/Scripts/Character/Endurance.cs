using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endurance
{
  [SerializeField]
  public float max;
 
  [SerializeField]
  public float Current {
    get { return Mathf.Clamp(current, 0, max); }
    set
    {
      current = value;
      Mathf.Clamp(current, 0, max);
    }
  }

  private float current;

  public Endurance(float _max) {
    this.max = _max;
    this.Current = _max;
  }

  public float Drain(float amount) {

    if (current <= 0) return Current;

    return Current -= amount * Time.deltaTime;
  }

  public float Heal(float amount)
  {
    if (current >= max) return Current;

    return Current += amount * Time.deltaTime;
  }
}

