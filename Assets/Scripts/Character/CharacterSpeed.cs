using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedType { Walk,Run }

[System.Serializable]
public class CharacterSpeed
{
  public float walk;
  public float run;

  public float Current
  {
    get { return current; }
  }

  private float current;


  public void Set(SpeedType type)
  {
    if (type == SpeedType.Walk) current = walk;
    else if (type == SpeedType.Run) current = run;
  }

}
