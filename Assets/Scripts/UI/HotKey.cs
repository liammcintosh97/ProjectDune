using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotKey : StaticUI
{
  public static string keyTextName = "Text";

  public UIItem KeyedItem { get { return _keyedItem; } set { SetKeyedItem(value); } }
  public int KeyNumber { get{return _keyNumber;}set { SetKeyNumber(value); } }
  public bool IsSet { get { return _isSet; } }

  public Text keyText;
  public Image itemImage;
  public Text itemName;

  private UIItem _keyedItem;
  private int _keyNumber;
  private bool _isSet;

  protected override void Awake(){
    base.Awake();
  }

  #region Init

  public void Init(int keyNumber) {
    SetKeyNumber(keyNumber);
    itemName.text = "";
  }

  #endregion

  #region Public Methods

  public int SetKeyNumber(int i) {
    _keyNumber = i;
    keyText.text = i.ToString();
    return _keyNumber;
  }


  public UIItem SetKeyedItem(UIItem item) {
    _keyedItem = item;

    if (item == null){
      _isSet = false;
      itemName.text = "";
    }
    else {
      _isSet = true;
      itemName.text = item.LinkedItem.name;
    }

    return _keyedItem;
  }

  #endregion

}
