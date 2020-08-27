using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(Transform))]

[RequireComponent(typeof(Rigidbody2D))]

public class Character : MonoBehaviour
{
  public CharacterStats characterStats = new CharacterStats();
  public CharacterSkills characterSkills = new CharacterSkills();
  public CharacterSpeed characterSpeed =  new CharacterSpeed();
  public Endurance endurance = new Endurance();
  public Health health = new Health();
  public Resistances baseResistances = new Resistances();

  public ResistanceManager resistanceManager = new ResistanceManager();

  private void Awake(){
    resistanceManager.AddResistances(baseResistances);
  
    characterSpeed.Set(SpeedType.Walk);
    endurance.Current = endurance.max;

    characterStats.Randomize();
    characterSkills.Randomize();
    baseResistances.Randomize();
  }



}
