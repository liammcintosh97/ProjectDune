using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedType { Walk,Run }

public class CharacterSpeed
{
  [SerializeField]
  public float walk;
  [SerializeField]
  public float run;

  [SerializeField]
  public float Current
  {
    get { return current; }
  }

  private float current;

  public CharacterSpeed(float _walk,float _run) {
    this.walk = _walk;
    this.run = _run;
    this.current = walk;
  }

  public void Set(SpeedType type)
  {
    if (type == SpeedType.Walk) current = walk;
    else if (type == SpeedType.Run) current = run;
  }

}
