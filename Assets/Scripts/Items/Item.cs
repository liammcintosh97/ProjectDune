using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsages {
  void SetUsages();
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(RectTransform))]

public abstract class Item : MonoBehaviour, IUsages
{

  public Vector2Int UISize;
  public bool PhysicsEnabled {
    get { return m_physicsEnabled; }
    set{ EnablePhysics(value); }
  }
  public Transform Transform { get { return _transform; } }
  public UIItem UIItem { get { return _UIItem; } }

  public delegate void Usage(object[] o);
  public Dictionary<string, Usage> usages;
  public Usage UPickUp;
  public Usage UDrop;

  private bool m_physicsEnabled;
  private UIItem _UIItem;
  private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

  protected GameManager gameManager;
  protected UIItemManager uIItemManager;
  protected HotKeys hotKeys;
  protected int ID = 0;
  protected BoxCollider2D collider;
  protected Transform _transform;


  protected virtual void Awake(){
    ID = System.DateTime.Now.Millisecond;
    usages = new Dictionary<string, Usage>();
    gameManager = GameManager.Instance;
    if (gameManager != null) {
      uIItemManager = gameManager.UIItemManager;
      hotKeys = gameManager.HotKeys;
    }
    _transform = GetComponent<Transform>();
    collider = (BoxCollider2D)Utility.ComponentCheck<BoxCollider2D>(gameObject, collider);
    if (collider == null) Debug.LogError(gameObject.name + " does not have a collider");
    SetUsages();
  }

  // Start is called before the first frame update
  protected virtual void Start()
  {
    ID = System.DateTime.Now.Millisecond;
    GetAllRenderes();

    _UIItem = uIItemManager.InstantiateUIItem(this, UISize, gameObject.name);
    uIItemManager.AddFocusable(_UIItem);

    if(!_transform.parent)_transform.parent = GameManager.Instance.ItemManager.GroundTransform;
  }

  #region Public Methods

  public void PickUp(object o) {

    bool focusOnUI = (bool)o;

    EnablePhysics(false);
    if(focusOnUI) uIItemManager.FocusOnUIItem(_UIItem);
    SetRenderes(false);
  }

  public virtual void Drop(object[] o) {

    Vector3 pos =  Vector3.zero;

    if (o == null) { pos = gameManager.Actor.transform.position; }
    else if (o[0] != null){
      pos = (Vector3)o[0];
    }

    EnablePhysics(true);
    uIItemManager.DeFocusOnUIItem(_UIItem);
    SetRenderes(true);
    _transform.parent = GameManager.Instance.ItemManager.GroundTransform;
    _transform.position = pos;
    if (_UIItem.HotKey != -1) hotKeys.RemoveHotKey(_UIItem.HotKey);

    if (o != null && o[1] != null) {
      UIItem uIItem = (UIItem)o[1];
      ItemSlot slot = uIItem.Slot;
      StorageWindow storageWindow = uIItem.StorageWindow;

      if (storageWindow && slot){
        storageWindow.DeSlot((StorageSlot)slot, uIItem);
        return;
      }
      if (slot)slot.DeSlot(uIItem);
    
    }
  }

  public virtual void SetRenderes(bool visible)
  {
    foreach (SpriteRenderer sr in spriteRenderers)
    {
      sr.enabled = visible;
    }
  }

  public void SetUsages(){

    string pickUpString = "Pick Up";
    string dropString = "Drop";

    if (!usages.ContainsKey(pickUpString) && !usages.ContainsKey(dropString)) {

      UPickUp = PickUp;
      UDrop = Drop;

      usages.Add(pickUpString, UPickUp);
      usages.Add(dropString, UDrop);
    }
  }

  #endregion

  #region Private Methods

  private void EnablePhysics(bool enable) {
    if (enable)
    {
      collider.enabled = true;
      m_physicsEnabled = true;
    }
    else {
      collider.enabled = false;
      m_physicsEnabled = true;
    }
  }

  private void GetAllRenderes()
  {
    /*
    //Add Parent Graphics
    SpriteRenderer[] parentRenders = gameObject.GetComponentsInParent<SpriteRenderer>();

    foreach (SpriteRenderer sr in parentRenders)
    {
      spriteRenderers.Add(sr);
    }*/

    //Add Child Graphics
    SpriteRenderer[] childRenderes = gameObject.GetComponentsInChildren<SpriteRenderer>();

    foreach (SpriteRenderer sr in childRenderes)
    {
      spriteRenderers.Add(sr);
    }

  }


  #endregion

}
