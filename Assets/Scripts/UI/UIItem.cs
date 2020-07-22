using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]


public class UIItem : DynamicUI
{
  public static string SelectionName = "Selection";
  public static string HotKeyName = "HotKey";

  public ItemDropDown itemDropDown;

  public Item LinkedItem { get { return _linkedItem; } set { _linkedItem = value; } }
  public Image Image { get { return _image; } }
  public bool CanUseLinkednItem { get { return _canUseLinkednItem; } }
  public bool CanHotKey { get { return _canHotKey; } }
  public bool CanSelect { get { return _canSelect; } }
  public bool IsSelected { get { return _isSelected; } }
  public int HotKey { get { return _hotKey; } }

  public ItemSlot Slot { get { return _slot; } set { _slot = value; } }
  public StorageWindow StorageWindow { get { return _storageWindow; } set { _storageWindow = value; } }
  public Window Window { get { return _window; } }
  public ItemSlot PSlot { get { return _pSlot; } set { _pSlot = value; } }
  public Window PWindow { get { return PWindow; } set { _pWindow = value; } }

  public RectTransform Selection;

  private Item _linkedItem;
  private ItemSlot _slot;
  private StorageWindow _storageWindow;
  private Window _window;
  private ItemSlot _pSlot;
  private Window _pWindow;

  private WindowManager windowManager;
  private UIItemManager uIItemManager;
  private Equipment equipment;
  private RectTransform HotKeyRectTrans;
  private Text hotKeyText;
  private Image _image;
  private bool _canUseLinkednItem = false;
  private bool _canHotKey = false;
  private bool _canSelect = false;
  private bool _isSelected = false;
  private int _hotKey = -1;

  protected override void Awake()
  {
    base.Awake();

    if (gameManager != null) {
      windowManager = gameManager.WindowManager;
      uIItemManager = gameManager.UIItemManager;
      equipment = gameManager.Equipment;
    }

    if (itemDropDown == null) {
      Debug.LogError("Item Drop Down is null");
      Debug.Break();
    }

    _image = GetComponent<Image>();
    Selection = (RectTransform) Utility.FindComponentInChild<RectTransform>(RectTransform, Selection, SelectionName);
    HotKeyRectTrans = (RectTransform)Utility.FindComponentInChild<RectTransform>(RectTransform, HotKeyRectTrans, HotKeyName);
    hotKeyText = GetComponentInChildren<Text>();
  }
  protected override void Start() {

    GetAllGraphics();
    SetUIgraphics(false);
  }

  void Update() {

    if(gameManager != null){

      if (uIItemManager.FocusedUIItem == this && gameManager.Equipment.State == WindowState.Open)
      {
        UIItem ouit = uIItemManager.CastToUIItem(RectTransform.position, this);
        if (ouit != null)
        {
          ouit.OpenStorageItem();
        }
      }
    }
 
  }

  #region Init Methods

  public void Init(Item li, Vector2Int _UISize, string _name) {
    _rectTransform = GetComponent<RectTransform>();
    _rectTransform.anchoredPosition = Vector2.zero;
    _linkedItem = li;
    itemDropDown.Item = _linkedItem;
    UISize = _UISize;
    SizeUI(new Vector2(ItemSlot.size * _UISize.x, ItemSlot.size * _UISize.y));

    SetSelected(false);
    SetHotKey(false, -1);
    
    gameObject.name = _name; 
  }

  #endregion

  #region Public Methods

  public override void OnPointerClick(PointerEventData data)
  {
    base.OnPointerClick(data);

    //Right Click
    if (data.button == PointerEventData.InputButton.Right) {
      itemDropDown.SetUIgraphics(true);
    }
  }

  public override void OnPointerEnter(PointerEventData data) {
    base.OnPointerEnter(data);
    /*
    if (uIItemManager.FocusedUIItem != null && CanUseLinkednItem) {
      OpenStorageItem();
    }*/
  }

  public override void OnBeginDrag(PointerEventData data)
  {
    base.OnBeginDrag(data);

    if (data.button == PointerEventData.InputButton.Left) {

      if (_slot != null){

        if (_slot.GetType() == typeof(EquipmentSlot)){
          EquipmentSlot es = (EquipmentSlot)_slot;
          es.DeSlot(this);
          _slot = null;
        }
        else if (_slot.GetType() == typeof(StorageSlot)){

          StorageSlot ss = (StorageSlot)_slot;
          StorageWindow sw = ss.StorageWindow;
          if (sw != null){
            sw.DeSlot(ss, this);
            _storageWindow = null;
            _slot = null;
          }
        }
      }
    }

  }

  public override void OnPointerUp(PointerEventData data)
  {
    base.OnPointerUp(data);

    if (data.button == PointerEventData.InputButton.Left) {

      if (_canUseLinkednItem){
        UseLinkedItem();
        return;
      }

      ItemSlot slotHit = uIItemManager.CastToItemSlot(RectTransform.position);

      if (slotHit != null){

        if (slotHit.GetType() == typeof(EquipmentSlot)){
          EquipmentSlot es = (EquipmentSlot)slotHit;
          es.Slot(this);
          _slot = es;
        }

        else if (slotHit.GetType() == typeof(StorageSlot)){
          StorageSlot ss = (StorageSlot)slotHit;
          StorageWindow sw = ss.StorageWindow;
          if (sw != null){
            sw.Slot(ss, this,true);
            _storageWindow = sw;
            _slot = ss;
          }
        }
      }
      else {
        Debug.Log("No Slot");
        LinkedItem.Drop(new object[] { gameManager.Actor.Character.transform.position });
      }
    }
  }

  public void OpenStorageItem()
  {
    if (LinkedItem == null)
    {
      Debug.Log("There is no linked item");
      return;
    }

    try
    {
      StorageItem si = (StorageItem)LinkedItem;
      if (si != null) si.Use(null);
    }
    catch (Exception e) { Debug.Log("The linked item is not a storage item"); }

  }

  public void SetSelected(bool visible){
    Selection.gameObject.SetActive(visible);
    _isSelected = visible;
  }

  public void SetHotKey(bool visible,int key) {
    HotKeyRectTrans.gameObject.SetActive(visible);
    hotKeyText.text = key.ToString();

    _hotKey = key;
  }

  public void SetState(ItemSlot newSlot, bool useItem,bool hotKey,bool select,bool focus) {

    ChangeSlot(newSlot);

    _canUseLinkednItem = useItem;
    _canHotKey = hotKey;
    _canSelect = select;
    if (focus) uIItemManager.FocusedUIItem = this;
    else uIItemManager.FocusedUIItem = null;
  }

  public void RemoveFromWindow() {
    if (_window) _window.RemoveGraphics(Graphics);
  }


  #endregion

  #region Private Methods

  private void UseLinkedItem() {
    Equipable e = (Equipable)LinkedItem;

    if (e != null) e.Use(null);
  }

  private void ChangeSlot(ItemSlot newSlot) {

    if (_slot != null) {
      //Set the previous window and slot 
      _pSlot = _slot;
      _pWindow = _slot.Window;
    }
 
    //set the new slot and window
    _slot = newSlot;
    if (newSlot != null) _window = newSlot.Window;
    else _window = null;
  }
  #endregion 
}
