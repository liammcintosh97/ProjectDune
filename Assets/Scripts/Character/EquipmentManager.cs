using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EquipmentManager : MonoBehaviour
{
  public Transform head, face, torso, chest, rightHand, leftHand, waist, legs, back, feet;
  public Actor actor;
  public List<Magazine> reservedMagazines;
  public List<StorageItem> storageItems;

  private void Awake()
  {
    InitTransforms();

    if (!HasRequiredTransforms()) { Debug.LogError("Equipment Manager does not have all it's required transforms"); }

    if (!actor) actor = GetComponentInParent<Actor>();
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

  public void AddStorageItem(StorageItem si) {
    if (!storageItems.Contains(si)) storageItems.Add(si);
  }

  public void AddMagazineToReserve(Magazine m) {
    if (!reservedMagazines.Contains(m)) reservedMagazines.Add(m);
  }

  public void RemoveStorageItem(StorageItem si) {
    if (storageItems.Contains(si)) storageItems.Remove(si);
  }

  public void RemoveMagazineFromReserve(Magazine m)
  {
    if (reservedMagazines.Contains(m)) reservedMagazines.Remove(m);
  }

  public Magazine GetMagazineFromReserve(ProjectileType pType) {
    foreach (Magazine m in reservedMagazines) {
      if (m.projectileType == pType) {
        Magazine toReturn = m;
        reservedMagazines.Remove(m);
        return toReturn;
      }
    }

    return null;
  }

  public void SetEquipmentParent(Equipable equipable, EquipableType type, bool visible) {
    Transform parent = InterprateName(type);

    equipable.SetRenderes(visible);
    equipable.transform.SetParent(parent);
    equipable.transform.localPosition = Vector3.zero;
    equipable.transform.localRotation = Quaternion.identity;

    if (Equipable.IsHand(type)) actor.SetInhand(equipable);
  }

  public Item GetEquipment(EquipableType type) {
    Transform parent = InterprateName(type);
    return parent.GetChild(0).GetComponent<Item>();
  }

  public bool StoreItem(Item item) {
    foreach (StorageItem si in storageItems){

      Vector2Int storageSize = si.StorageWindow.Size;
      StorageWindow sw = si.StorageWindow;
      StorageSlot[,] slots = sw.storageSlotMatrix;

      for (int x = 0; x < storageSize.x; x++){
        for (int y = 0; y < storageSize.y; y++){
          if (sw.Slot(slots[x, y], item.UIItem,false)) return true;
        }
      }
    }
      return false;
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

  #endregion
}
