using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EquipmentManager : MonoBehaviour
{
  public Transform head, face, torso, chest, rightHand, leftHand, waist, legs, back, feet;
  public Actor actor;

  public ResistanceManager resistanceManager;
  public AmmoReserve ammoReserve;
  public Storage storage;
  
  private void Awake()
  {
    InitTransforms();

    if (!HasRequiredTransforms()) { Debug.LogError("Equipment Manager does not have all it's required transforms"); }

    if (!actor) actor = GetComponentInParent<Actor>();

    resistanceManager =  new ResistanceManager();
    ammoReserve =  new AmmoReserve();
    storage = new Storage();
}

  #region Init

  private void InitTransforms() {

    head = transform.Find(EquipableType.Head.ToString());
    face = transform.Find(EquipableType.Face.ToString());
    torso = transform.Find(EquipableType.Torso.ToString());
    chest = transform.Find(EquipableType.Chest.ToString());
    rightHand = transform.Find(EquipableType.RightHand.ToString());
    leftHand = transform.Find(EquipableType.LeftHand.ToString());
    waist = transform.Find(EquipableType.Waist.ToString());
    legs = transform.Find(EquipableType.Legs.ToString());
    back = transform.Find(EquipableType.Back.ToString());
    feet = transform.Find(EquipableType.Feet.ToString());
  }

  #endregion

  #region Public Methods

  public void Equip(Equipable ep) {

    EquipableType equipableType = ep.equipableType;

    resistanceManager.AddResistances(ep.resistances);
    SetEquipmentParent(ep, equipableType, Equipable.IsHand(equipableType));

    if (StorageItem.IsStorageItem(ep)) storage.AddStorageItem((StorageItem)ep);
  }

  public void Unequip(Equipable ep) {
    resistanceManager.SubtractResistances(ep.resistances);

    if (StorageItem.IsStorageItem(ep)) storage.RemoveStorageItem((StorageItem)ep);
  }

  public Item GetEquipment(EquipableType type) {
    Transform parent = InterprateName(type);
    return parent.GetChild(0).GetComponent<Item>();
  }


  #endregion

  #region Private Methods

  private bool HasRequiredTransforms() {

    if (!head || !face || !torso || !chest || !rightHand || !leftHand || !waist | !legs || !back || !feet) return false;
    return true;
  }

  private Transform InterprateName(EquipableType type) {
    switch (type) {

      case EquipableType.Head: return head;
      case EquipableType.Face: return face;
      case EquipableType.Torso: return torso;
      case EquipableType.Chest: return chest;
      case EquipableType.RightHand: return rightHand;
      case EquipableType.LeftHand: return leftHand;
      case EquipableType.Waist: return waist;
      case EquipableType.Legs: return legs;
      case EquipableType.Back: return back;
      case EquipableType.Feet: return feet;
      default: return null;
  }
  }

  private void SetEquipmentParent(Equipable equipable, EquipableType type, bool visible)
  {
    Transform parent = InterprateName(type);

    equipable.SetRenderes(visible);
    equipable.transform.SetParent(parent);
    equipable.transform.localPosition = Vector3.zero;
    equipable.transform.localRotation = Quaternion.identity;

    if (Equipable.IsHand(type)) actor.SetInhand(equipable);
  }

  #endregion
}
