using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]

public class DebugElement : StaticUI
{
  public Text log;

  private DebugWindow _dWindow;
  private int _position;

  protected override void Awake()
  {
    base.Awake();
    log = GetComponent<Text>();

    GetAllGraphics();
    SetUIgraphics(false);
  }

  #region Init

  public void Init(DebugWindow dw,int position) {

    _dWindow = dw;
    _position = position;

    InitReactTrans(position);
  }

  private void InitReactTrans(int position) {

    float padding = _dWindow.elementPadding;
    float width = _dWindow.RectTransform.sizeDelta.x;
    float height = _rectTransform.sizeDelta.y;

    _rectTransform.SetParent(_dWindow.RectTransform);
    _rectTransform.anchoredPosition = new Vector2(padding, (-height * position) - padding );
    _rectTransform.sizeDelta = new Vector2(width - (padding * 2), height);
  }

  #endregion

  #region Public Methods

  public void UpdateText(string name, string value) {

    if (_dWindow.IsShowing){
      string newText = (_position + 1) + " : " + name + " = " + value;
      log.text = newText;
    }
  }

  #endregion
}
