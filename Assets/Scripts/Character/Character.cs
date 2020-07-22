using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(Transform))]

[RequireComponent(typeof(Rigidbody2D))]

public class Character : MonoBehaviour
{
  [SerializeField]
  public CharacterStats characterStats;
  public CharacterSpeed speed;
  public Endurance endurance;

  public Transform Transform { get { return _transform; } }

  private Transform _transform;

  // Start is called before the first frame update
  void Start()
  {
    InitCharacterStats();
  }

  private void InitCharacterStats() {
    speed = new CharacterSpeed(characterStats.walkSpeed, characterStats.runSpeed);
    endurance = new Endurance(characterStats.maxEndurance);
  }


}
