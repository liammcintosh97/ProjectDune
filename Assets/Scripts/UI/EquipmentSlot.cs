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

      equipmentManager.Equip(ep);

      if (Equipable.IsHand(equipableType)) uIItem.SizeUI(new Vector2Int((int)size, (int)size));
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

    equipmentManager.Unequip(ep);

    Debug.Log("The item" + ep.name + " was de-sloted from Equipment");
  }

  #endregion

}
