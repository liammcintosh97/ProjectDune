using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileType", menuName = "ProjectileType", order = 1)]
public class ProjectileType : ScriptableObject
{
  public float force;
  public float mass;

}
