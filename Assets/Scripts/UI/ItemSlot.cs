using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]


public class ItemSlot : Selectable
{
  public static float size = 50;
  public static string gridName = "Grid";

  public Image grid;
  public UIItem SlotedItem { get { return _slotedItem; } set { _slotedItem = value; } }
  public RectTransform RectTransform { get { return _rectTransform; } }
  public Window Window { get { return _window; } }

  protected RectTransform _rectTransform;
  protected UIItem _slotedItem;
  protected UIItemManager uIItemManager;
  protected Window _window;

  protected override void Awake() {
    _rectTransform = GetComponent<RectTransform>();
    if (GameManager.Instance != null){
      uIItemManager = GameManager.Instance.UIItemManager;
    }
    grid = (Image)Utility.FindComponentInChild<Image>(_rectTransform, grid, gridName);
    _window = GetComponentInParent<Window>();
  }

  #region Init Methods

  private void InitSize() {
    _rectTransform.sizeDelta = new Vector2(size, size);
  }

  #endregion


  #region Public Methods

  public virtual void Slot(UIItem uit){
    _slotedItem = uit;
  }

  public virtual void DeSlot(UIItem uit){
    _slotedItem = null;
  }

  public bool IsFree() {
    return _slotedItem == null;
  }

  #endregion
}
