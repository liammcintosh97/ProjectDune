using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class ResistanceManager{
  public Resistances totalResistances = new Resistances();

  private List<Resistances> resistances = new List<Resistances>();

  public void AddResistances(Resistances newResistances){
    if (!resistances.Contains(newResistances)){
      resistances.Add(newResistances);
      totalResistances += newResistances;
    }
  }

  public void SubtractResistances(Resistances subtraction){
    if (resistances.Contains(subtraction)){
      resistances.Remove(subtraction);
      totalResistances -= subtraction;
    }
  }

}
