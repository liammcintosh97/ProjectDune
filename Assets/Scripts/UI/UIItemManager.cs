using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]

public class UIItemManager : Focuser
{
  public UIItem FocusedUIItem { get { return _focusedUIItem; } set { _focusedUIItem = value; } }
  public UIItem SelectedUIItem { get { return _selectedUIItem; } set { ChangeSelected(value); } }
  public Canvas Canvas { get { return _canvas; } }
  public GraphicRaycaster GraphicRaycaster { get { return _graphicRaycaster; } }
  public EventSystem EventSystem { get { return _eventSystem; } }

  public List<UIItem> UIItems = new List<UIItem>();

  public static string prefabPathName = "Prefabs/UI/UIItem";

  private static string namePostFix = " - UIItem";
  private UIItem _focusedUIItem;
  private UIItem _selectedUIItem;
  private GraphicRaycaster _graphicRaycaster;
  private EventSystem _eventSystem;
  private Canvas _canvas;
  private GameManager gameManager;
  private UserInput userInput;

  protected override void Awake()
  {
    base.Awake();

    gameManager = GameManager.Instance;

    if (gameManager != null) {
      _canvas = gameManager.Canvas;

      _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();
      _eventSystem = _canvas.GetComponent<EventSystem>();
      userInput = UserInput.Instance;
    }
  }

  private void Update()
  {
    //UIItem selection
    if (userInput.MousePress(0)) {
      UIItem uit = CastToUIItem(Input.mousePosition);
      SelectedUIItem = uit;
    }
  }

  #region Private Methods

  private void ChangeSelected(UIItem uit) {

    //Check if the current selected UIItem is not null and deselect it
    if (_selectedUIItem != null){
      _selectedUIItem.SetSelected(false);
      _selectedUIItem = null;
    }

    _selectedUIItem = uit;
    if (uit != null && uit.CanSelect) uit.SetSelected(true);
  }

  #endregion

  #region Public Methods

  public UIItem InstantiateUIItem(Item li, Vector2Int UISize, string _name){

    GameObject UIItemPrefab = Resources.Load<GameObject>(prefabPathName);
    UIItem UIItem = Instantiate(UIItemPrefab,RectTransform).GetComponent<UIItem>();

    UIItem.Init(li, UISize, _name + namePostFix);

    return UIItem;
  }

  public void FocusOnUIItem(UIItem uit) {
    _focusedUIItem = uit;
    uit.RectTransform.position = Input.mousePosition;
    uit.SetUIgraphics(true);
  }

  public void DeFocusOnUIItem(UIItem uit){
    _focusedUIItem = null;
    uit.SetUIgraphics(false);
    uit.itemDropDown.SetUIgraphics(false);
  }

  public ItemSlot CastToItemSlot(Vector3 pos)
  {
    PointerEventData m_PointerEventData;
    List<RaycastResult> results = new List<RaycastResult>();

    m_PointerEventData = new PointerEventData(_eventSystem);
    m_PointerEventData.position = pos;

    _graphicRaycaster.Raycast(m_PointerEventData, results);

    foreach (RaycastResult r in results)
    {
      ItemSlot s = r.gameObject.GetComponent<ItemSlot>();

      if (s != null)
      {
        return s;
      }
    }

    return null;
  }

  public UIItem CastToUIItem(Vector3 pos)
  {
    PointerEventData m_PointerEventData;
    List<RaycastResult> results = new List<RaycastResult>();

    m_PointerEventData = new PointerEventData(_eventSystem);
    m_PointerEventData.position = pos;

    _graphicRaycaster.Raycast(m_PointerEventData, results);

    foreach (RaycastResult r in results)
    {
      UIItem uit = r.gameObject.GetComponent<UIItem>();

      if (uit != null)
      {
        return uit;
      }
    }

    return null;
  }

  public UIItem CastToUIItem(Vector3 pos,UIItem ignore)
  {
    PointerEventData m_PointerEventData;
    List<RaycastResult> results = new List<RaycastResult>();

    m_PointerEventData = new PointerEventData(_eventSystem);
    m_PointerEventData.position = pos;

    _graphicRaycaster.Raycast(m_PointerEventData, results);

    foreach (RaycastResult r in results)
    {
      UIItem uit = r.gameObject.GetComponent<UIItem>();

      if (uit != null && uit != ignore)
      {
        return uit;
      }
    }

    return null;
  }

  #endregion
}
