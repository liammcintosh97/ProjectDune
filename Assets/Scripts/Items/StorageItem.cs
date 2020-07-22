using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItem : Equipable
{
  public GameObject storageWindowPrefab;
  public Vector2Int storageSize;
  public StorageWindow StorageWindow { get { return _storageWindow; } }

  private StorageWindow _storageWindow;
  private WindowManager _windowManager;
  private Equipment _equipment;

  protected override void Start(){
    base.Start();

    if (gameManager != null)
    {
      _windowManager = GameManager.Instance.WindowManager;
      _equipment = GameManager.Instance.Equipment;
    }

    _storageWindow = (StorageWindow) _windowManager.InstantiateWindow(storageWindowPrefab);
    _storageWindow.Init(storageSize,this,gameObject.name);
  }

  #region Public Methods

  public override void Use(object o)
  {
   
      _equipment.OpenWindow();
      _storageWindow.OpenWindow();
      _windowManager.Focused = _storageWindow;

  }

  public override void Drop(object[] o) {
    base.Drop(null);

    if (o != null) {
      _storageWindow.CloseWindow();
      _windowManager.Focused = _windowManager.PreviousFocused;
    }

  }

  #endregion

}
