using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorageWindow : Window
{
  public static string matrixName = "StorageMatix";
  public static string storedItemsName = "StoredItems";

  [SerializeField]
  public GameObject storageSlotPrefab;
  public StorageSlot[,] storageSlotMatrix;
  public RectTransform storageMatrixRectTrans;
  public RectTransform storedItemsRectTrans;

  public Vector2Int Size { get { return _size; } }

  [Space(5)]
  [Header("Padding")]
  public float pLeft = 5;
  public float pTop = 25;
  public float pRight = 5;
  public float pBottom = 5;

  private Vector2Int _size;
  private int rowLength;
  private int colsLength;
  private StorageItem _storageItem;

  #region Init Methods

  public void Init(Vector2Int size,StorageItem storageItem,string name) {

    windowName = name;
    _size = size;
    _storageItem = storageItem;

    InitStorageSlotMatrix();
    InitTransforms();
    InitSlots();
  }

  private void InitStorageSlotMatrix()
  {

    storageSlotMatrix = new StorageSlot[_size.x, _size.y];
    rowLength = storageSlotMatrix.GetLength(0);
    colsLength = storageSlotMatrix.GetLength(1);
  }

  private void InitSlots()
  {
    for (int x = 0; x < _size.x; x++)
    {
      for (int y = 0; y < _size.y; y++)
      {
        Vector2Int matrixPOS = new Vector2Int(x, y);
        Vector2 pos = new Vector2(y * ItemSlot.size, -(x * ItemSlot.size));

        storageSlotMatrix[x, y] = InitStorageSlot(matrixPOS, pos);
      }
    }
  }

  private void InitTransforms()
  {
    //Matrix RectTransform
    Vector2 matrixSizeDelta = new Vector2(colsLength * ItemSlot.size, rowLength * ItemSlot.size);
    content.sizeDelta = matrixSizeDelta;

    storageMatrixRectTrans = (RectTransform)Utility.FindComponentInChild<RectTransform>(storageMatrixRectTrans, storageMatrixRectTrans, matrixName);
    storedItemsRectTrans = (RectTransform)Utility.FindComponentInChild<RectTransform>(storedItemsRectTrans, storedItemsRectTrans, storedItemsName);

    //Content RectTransform
    Vector2 cs = content.sizeDelta = matrixSizeDelta;
    content.anchoredPosition = new Vector2(pLeft, -pTop);

    //This RectTransform
    _rectTransform.sizeDelta = new Vector2(cs.x + (pLeft + pRight), cs.y + (pTop + pBottom));
  }

  private StorageSlot InitStorageSlot(Vector2Int _matrixPOS, Vector2 pos)
  {
    StorageSlot slot = Instantiate(storageSlotPrefab, storageMatrixRectTrans).GetComponent<StorageSlot>();
    slot.StorageWindow = this;
    slot.MatrixPOS = _matrixPOS;

    Vector3 newPos = new Vector3(pos.x, pos.y, 0);
    slot.GetComponent<RectTransform>().anchoredPosition = newPos;


    return slot;
  }

  #endregion

  #region Public Methods

  public bool Slot(StorageSlot startSlot, UIItem uIItem,bool focus)
  {
    uIItem.RemoveFromWindow();
    AddGraphics(uIItem.Graphics);
   if(focus) Manager.Focused = this;

    if (AvailiableSpace(startSlot.MatrixPOS, uIItem))
    {
      Item linkedItem = uIItem.LinkedItem;

      uIItem.SetState(startSlot, false, true, true, false);
      uIItem.RectTransform.position = startSlot.RectTransform.position;
      uIItem.RectTransform.parent = storedItemsRectTrans;

      FillSlots(startSlot.MatrixPOS, uIItem);

      linkedItem.transform.SetParent(_storageItem.transform);

      if (linkedItem.GetType() == typeof(Magazine)) {
        gameManager.Actor.EquipmentManager.AddMagazineToReserve((Magazine)linkedItem);
      }

      Debug.Log("item was sloted in storage");
      return true;
    }

    return false;
  }

  public void DeSlot(StorageSlot startSlot, UIItem uIItem)
  {
    Debug.Log("The item " + uIItem.LinkedItem.name + " was de-sloted from storage");

    uIItem.SetState(null, false, false, false, false);
    uIItem.RectTransform.parent = GameManager.Instance.UIItemManager.RectTransform;

    FreeSlots(startSlot.MatrixPOS, uIItem);
    RemoveGraphics(uIItem.Graphics);
  }

  #endregion

  #region Private Methods

  private bool AvailiableSpace(Vector2Int matrixPos, UIItem uIItem)
  {

    int rows = uIItem.UISize.x + matrixPos.x;
    int collums = uIItem.UISize.y + matrixPos.y;

    for (int x = matrixPos.x; x < rows; x++)
    {
      for (int y = matrixPos.y; y < collums; y++)
      {
        try { if (!storageSlotMatrix[x, y].IsFree()) return false; }
        catch (Exception e) { return false; }
      }
    }

    return true;
  }

  private void FillSlots(Vector2Int matrixPos, UIItem uIItem)
  {

    int rows = uIItem.UISize.x + matrixPos.x;
    int collums = uIItem.UISize.y + matrixPos.y;

    for (int x = matrixPos.x; x < rows; x++)
    {
      for (int y = matrixPos.y; y < collums; y++)
      {
        storageSlotMatrix[x, y].SlotedItem = uIItem;
      }
    }

  }

  private void FreeSlots(Vector2Int matrixPos, UIItem uIItem)
  {

    int rows = uIItem.UISize.x + matrixPos.x;
    int collums = uIItem.UISize.y + matrixPos.y;

    for (int x = matrixPos.x; x < rows; x++)
    {
      for (int y = matrixPos.y; y < collums; y++)
      {
        try { storageSlotMatrix[x, y].SlotedItem = null; }
        catch (Exception e) { return; }
      }
    }
  }

  #endregion
}
