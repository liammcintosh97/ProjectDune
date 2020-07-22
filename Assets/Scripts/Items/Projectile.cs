using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ParticleSystem))]

public class Projectile : Item
{
  public ProjectileType projectileType;
  public Rigidbody2D rigidbody2D;

  [Header("Visual Effects")]

  public Light2D projectileLight;

  private Actor shooter;


  protected override void Awake(){
    base.Awake();
    rigidbody2D = GetComponent<Rigidbody2D>();

    collider.isTrigger = true;


    projectileLight.gameObject.SetActive(false);
  }

  public void Shoot(Vector3 up, Actor _shooter){

    shooter = _shooter;

    PhysicsEnabled = true;
    SetRenderes(true);
    rigidbody2D.velocity = up * projectileType.force;
    collider.isTrigger = false;


    projectileLight.gameObject.SetActive(true);
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    Actor colliededActor

    if(collision.gameObject != )

    Debug.Log(" Projectile: " + ID + " Hit " + collision.gameObject.name);
    Destroy(gameObject);
  }

}
