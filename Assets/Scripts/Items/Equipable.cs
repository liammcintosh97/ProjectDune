﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipableType { Null,RightHand,LeftHand,Hand,BothHands,Head,Face,Torso,Chest,Legs,Waist,Feet,Back}

public abstract class Equipable : Item, IUsages
{
  public EquipableType equipableType;
  public abstract void Use(object o);

  protected Usage UUse;

  protected override void Awake()
  {
    base.Awake();
    SetUsages();
  }

  
  public new void SetUsages()
  {
    string useString = "Use";

    if (!usages.ContainsKey(useString)) {
      UUse = Use;
      usages.Add("Use", UUse);
    }
  }

  public static bool IsHand(EquipableType e) {
    return e == EquipableType.RightHand || e == EquipableType.LeftHand || e == EquipableType.Hand || e == EquipableType.BothHands;
  }
}
