using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
  public EquipableType equipableType;

  private Equipment equipment;
  private EquipmentManager equipmentManager;

  protected override void Awake()
  {
    base.Awake();
    if (GameManager.Instance != null) {
      equipment = GameManager.Instance.Equipment;
      equipmentManager = GameManager.Instance.Actor.EquipmentManager;
    }
  }

  #region Public Methods

  public override void Slot(UIItem uIItem){

    Equipable ep = (Equipable)uIItem.LinkedItem;
    if (ep == null){
      Debug.Log("The item " + ep.name + " is not an equipable");
      return;
    }

    if (equipableType == ep.equipableType){
      base.Slot(uIItem);

      uIItem.RemoveFromWindow();
      equipment.AddGraphics(uIItem.Graphics);

      uIItem.RectTransform.SetParent(_rectTransform);
      uIItem.RectTransform.anchoredPosition = Vector3.zero;
      uIItem.SetState(this, true, true, true, false);

      equipmentManager.SetEquipmentParent(ep, equipableType, IsHand());
      if (IsStorageItem(ep)) equipmentManager.AddStorageItem((StorageItem)ep);

      if (IsHand()) uIItem.SizeUI(new Vector2Int((int)size, (int)size));
      Debug.Log("The item " + ep.name + " was sloted in equipment");
    }
    else{
      Debug.Log("The item " + ep.name + " is not of the equipment type " + equipableType.ToString());
    }

  }

  public override void DeSlot(UIItem uIItem){
    base.DeSlot(uIItem);

    Equipable ep = (Equipable)uIItem.LinkedItem;
    Vector2 newUISize = new Vector2(size * uIItem.UISize.x, size * uIItem.UISize.y);

    equipment.RemoveGraphics(uIItem.Graphics);

    uIItem.RectTransform.parent = uIItemManager.RectTransform;
    uIItem.SetState(null, false, false, false, false);

    if (IsStorageItem(ep)) equipmentManager.RemoveStorageItem((StorageItem)ep);

    if (IsHand()) uIItem.SizeUI(newUISize);

    Debug.Log("The item" + ep.name + " was de-sloted from Equipment");
  }

  public bool IsHand() {
    return equipableType == EquipableType.Hand || equipableType == EquipableType.BothHands || equipableType == EquipableType.RightHand || equipableType == EquipableType.LeftHand;
  }

  #endregion

  #region PrivateMethods

  private bool IsStorageItem(Equipable equipable) {
    try{
      StorageItem si = (StorageItem)equipable;
      return true;
    }
    catch (System.Exception e) {
      Debug.Log("The sloted item" + equipable.name + " is not a Storage Item");
      return false;
    }
  }

  #endregion
}
