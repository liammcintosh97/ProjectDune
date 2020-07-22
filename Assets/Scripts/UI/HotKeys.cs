using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HotKeys : StaticUI
{
  public HotKey[] Keys { get { return _keys; } }
  private HotKey[] _keys = new HotKey[10];

  private UIItemManager uIItemManager;
  private Equipment equipment;

  protected override void Awake()
  {
    base.Awake();
    if (GameManager.Instance != null) {
      uIItemManager = GameManager.Instance.UIItemManager;
      equipment = GameManager.Instance.Equipment;
    }
    Init();
  }

  void Update() {

    if (userInput != null){
      int key = userInput.PressHotKey();

      if (key != -1){
        if (equipment.State == WindowState.Open) SwitchHotKey(key);
        else UseKeyedEquipable(key);
      }
    }

  }

  #region Init

  private void Init() {
    HotKey[] ks = GetComponentsInChildren<HotKey>();

    for (int i = 0; i < ks.Length; i++) {
      ks[i].Init(i + 1);
      Keys[i] = ks[i];
    }
  }

  #endregion

  #region Public Methods

  public void UseKeyedEquipable(int key) {
    Equipable e = GetEquipable(key);

    if (e.equipableType == EquipableType.Hand) equipment.EquipRightHand(e);
    else if (e.equipableType == EquipableType.BothHands) equipment.EquipBothHands(e);
    else e.Use(null);
  }

  public Equipable GetEquipable(int key) {
    return (Equipable)Keys[key-1].KeyedItem.LinkedItem; 
  }

  public void SwitchHotKey(int key) {
    UIItem selected = uIItemManager.SelectedUIItem;

    UIItem removed = RemoveHotKey(key);
    if(removed != selected)SetHotKey(selected, key);
  }

  public void SetHotKey(UIItem uit, int hotKey){

    try{
      Equipable e = (Equipable)uit.LinkedItem;

      Keys[hotKey - 1].KeyedItem = uit;
      uit.SetHotKey(true, hotKey);
    }
    catch (InvalidCastException e)
    {
      Debug.LogError("The linked item " + uit.LinkedItem.name + " is not of the type " + typeof(Equipable) + " " + e.Message);
    }
  }

  public UIItem RemoveHotKey(int hotKey)
  {
    UIItem removed = Keys[hotKey - 1].KeyedItem;

    if (removed != null) removed.SetHotKey(false, -1);
    Keys[hotKey - 1].KeyedItem = null;

    return removed;
  }

  #endregion
}
