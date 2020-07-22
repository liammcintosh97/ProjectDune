using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : Item
{
  public GameObject projectilePrefab;
  public ProjectileType projectileType;
  public int size;

  protected override void Awake()
  {
    base.Awake();
  }

  protected override void Start()
  {
    base.Start();
    InitProjectilesInMagazine();
  }

  #region Init Methods

  private void InitProjectilesInMagazine() {

    foreach (Projectile p in GetComponentsInChildren<Projectile>()) {
      p.PhysicsEnabled = false;
 
    }
  }

  #endregion

  #region Public Methods

  public void SetMagazine(int count) {

    Mathf.Clamp(count, 0, size);

    for (int i = 0; i < size; i++) {
      Projectile p = Instantiate(projectilePrefab).GetComponent<Projectile>();
      Load(p);
    }

  }

  public Projectile Cycle(FireArm fireArm) {

    if (transform.childCount == 0){
      Debug.Log("Run out of ammo");
      return null;
    }


    Transform child = transform.GetChild(0);
    Projectile p = transform.GetChild(0).GetComponent<Projectile>();

    if (p == null) {
      Debug.LogError(gameObject.name + "'s child " + child + " is not a projectile");
      return null;
    }

    p.transform.SetParent(null);
    p.transform.position = fireArm.Barrel.position;
    p.transform.rotation = fireArm.Barrel.rotation;


    return p;
  }

  public bool Load(Projectile p) {
    if (gameObject.transform.childCount == size) return false;

    p.transform.parent = gameObject.transform;
    p.PhysicsEnabled = false;
    p.enabled = false;

    return true;
  }

  #endregion
}
