using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.Rendering.LWRP;

public enum FireRate {Semi, Auto}

public class FireArm : Weapon, IUsages
{
  public GameObject magazinePrefab;
  public ProjectileType projectileType;
  public FireRate fireRate;
  public bool varibableFireRate;
  public Magazine loadedMagazine;
  public float zoomMultiplyer = 2;

  [Header("Fire Effects")]
  public ParticleSystem fireParticles;
  public Light2D fireLight;
  public float flashTime = 0.05f;

  public Transform Barrel { get { return _barrel; } }

  protected Usage UReload;
  protected Usage USwitchFireRate;
  protected Usage UChamber;

  private Projectile chamberedProjectile;
  private Transform _barrel;


  protected override void Awake()
  {
    base.Awake();

    _barrel = transform.Find("barrel");

    if (_barrel == null) {
      Debug.LogError("The Fire Arm " + name + " does not have a barrel transform");
      Debug.Break();
    }

    SetUsages();
    fireLight.enabled = false;
  }

  #region Public Methods

  public bool SwitchFireRate() {
    if (!varibableFireRate) return false;

    if (fireRate == FireRate.Semi) fireRate = FireRate.Auto;
    else if (fireRate == FireRate.Auto) fireRate = FireRate.Semi;

    return true;
  }

  public override void Use(object o) {

    Actor shooter = null;

    try{
      shooter = (Actor)o;
    }
    catch (Exception e) {
      Debug.LogError("The shooter is null");
      return;
    }

    if (shooter == null) return;

    if (loadedMagazine)
    {
      if (chamberedProjectile) chamberedProjectile.Shoot(transform.up, shooter);
      else {
        Chamber(loadedMagazine.Cycle(this));
        if (chamberedProjectile){
          chamberedProjectile.Shoot(transform.up, shooter);
          FireEffects();
        }    
      }
    }
    else Debug.Log("There is no loaded Magazine");
  }

  public void Reload(object[] o) {

    Magazine newMagazine;
    Vector3 pos;

    try {
      newMagazine = (Magazine)o[0];
      pos = (Vector3)o[1];

      if (loadedMagazine != null) loadedMagazine.Drop(new object[] { pos });

      CheckMagazineStorage(newMagazine);

      loadedMagazine = newMagazine;
      loadedMagazine.transform.parent = gameObject.transform;
    }
    catch (InvalidCastException) {
      Debug.LogError("The passed variables into Reload were not cast correctly. Please check the position of elements in the passed object array");
    }

  }

  public Projectile Chamber(Projectile p) {

    Projectile rp = null;

    if (chamberedProjectile != null) rp = chamberedProjectile;
    chamberedProjectile = p;


    return rp;
  }

  public new void SetUsages() {

    string reloadString = "Reload";

    if (!usages.ContainsKey(reloadString)) {
      UReload = Reload;
      usages.Add("Reload", UReload);
    }
  }

  #endregion

  #region Private Methods

  private void CheckMagazineStorage(Magazine m) {
    UIItem uIItem = m.UIItem;
    StorageWindow sw = m.UIItem.StorageWindow;
    StorageSlot ss = (StorageSlot)m.UIItem.Slot;

    if (sw && ss && uIItem) { sw.DeSlot(ss, uIItem); }
  }

  private void FireEffects() {
    fireParticles.Play();

    StopCoroutine(Flash());
    StartCoroutine(Flash());
  }

  private IEnumerator Flash() {
    fireLight.enabled = true;
    yield return new WaitForSeconds(flashTime);
    fireLight.enabled = false;
  }

  #endregion
}
