using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Equipment : Window {

  [Space(5)]
  [Header("Slots")]
  public EquipmentSlot head;
  public EquipmentSlot face;
  public EquipmentSlot torso;
  public EquipmentSlot chest;
  public EquipmentSlot back;
  public EquipmentSlot rightHand;
  public EquipmentSlot leftHand;
  public EquipmentSlot legs;
  public EquipmentSlot waist;
  public EquipmentSlot feet;

  private List<EquipmentSlot> slots = new List<EquipmentSlot>();

  protected override void Awake()
  {
    base.Awake();
    InitSlots();
  }

  #region Init Methods

  private void InitSlots() {
    slots.Add(head);
    slots.Add(face);
    slots.Add(torso);
    slots.Add(chest);
    slots.Add(back);
    slots.Add(rightHand);
    slots.Add(leftHand);
    slots.Add(legs);
    slots.Add(waist);
    slots.Add(feet);
  }

  #endregion

  #region Public Methods

  public void EquipRightHand(Equipable e) {
    rightHand.Slot(e.UIItem);
  }

  public void EquipLeftHand(Equipable e){
    leftHand.Slot(e.UIItem);
  }

  public void EquipBothHands(Equipable e) {
    EquipRightHand(e);
    EquipLeftHand(e);
  }

  public EquipmentSlot CheckEquipmentSlots(UIItem toSlot) {

    try{
      Equipable e = (Equipable)toSlot.LinkedItem;

      foreach (EquipmentSlot slot in slots){
        if (slot.SlotedItem == null){
          if (slot.equipableType == e.equipableType)return slot;     
        }
      }
    }catch (Exception e) { return null; }

    return null;
}

  #endregion

}
