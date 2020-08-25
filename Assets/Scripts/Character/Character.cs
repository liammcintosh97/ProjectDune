using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(Transform))]

[RequireComponent(typeof(Rigidbody2D))]

public class Character : MonoBehaviour
{
  public CharacterSpeed characterSpeed =  new CharacterSpeed();
  public Endurance endurance = new Endurance();
  public Health health = new Health();
  public Resistances resistances = new Resistances();

}
