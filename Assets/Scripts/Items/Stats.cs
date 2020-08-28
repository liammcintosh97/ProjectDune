using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats
{

  public Stats() {
    InitStats();
  }

  public static float minStat = 0;
  public static float maxStat = 100;

  protected abstract void InitStats();

  public abstract void Randomize();
}
