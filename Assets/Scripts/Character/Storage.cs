using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
  public List<StorageItem> StorageItems { get { return _storageItems; } }

  private List<StorageItem> _storageItems = new List<StorageItem>();

  #region Public Methods

  public bool Store(Item item){

    if (_storageItems == null || _storageItems.Count == 0) {
      Debug.Log("There is no where to store the item");
      return false;
    }

    foreach (StorageItem si in _storageItems) {

      Vector2Int storageSize = si.StorageWindow.Size;
      StorageWindow sw = si.StorageWindow;
      StorageSlot[,] slots = sw.storageSlotMatrix;

      for (int x = 0; x < storageSize.x; x++)
      {
        for (int y = 0; y < storageSize.y; y++)
        {
          if (sw.Slot(slots[x, y], item.UIItem, false)) return true;
        }
      }
    }
    return false;
  }

  public void AddStorageItem(StorageItem si)
  {
    if (!_storageItems.Contains(si)) _storageItems.Add(si);
  }

  public void RemoveStorageItem(StorageItem si)
  {
    if (_storageItems.Contains(si)) _storageItems.Remove(si);
  }

  #endregion

}
