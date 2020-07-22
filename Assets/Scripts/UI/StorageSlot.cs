using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSlot : ItemSlot
{
  public StorageWindow StorageWindow { get { return _storageWindow; } set { _storageWindow = value; } }
  public Vector2Int MatrixPOS { get { return _matrixPOS; } set { _matrixPOS = value; } }

  private StorageWindow _storageWindow;
  private Vector2Int _matrixPOS;
}
