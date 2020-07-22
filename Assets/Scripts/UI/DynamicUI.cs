using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DynamicUI : StaticUI
{
  public Vector2Int UISize { get { return _UISize; } set { _UISize =  value; } }

  private RectTransform canvasRectTrans;
  private Vector2 canvasDimensions;
  private Vector3 pDragPos;
  private Vector2 UIDimensions;
  private Vector2 diferenceDimension;
  private Vector2 bounds;
  private Vector2Int _UISize;

  protected override void Awake()
  {
    base.Awake();
    InitTriggers();
  }

  protected override void Start()
  {
    base.Start();
    InitBounds();
  }

  #region Init

  private void InitBounds(){

    if (_canvas != null) canvasRectTrans = _canvas.GetComponent<RectTransform>();

    if (canvasRectTrans != null) {
      canvasDimensions = new Vector2(canvasRectTrans.rect.width, canvasRectTrans.rect.height);
      UIDimensions = _rectTransform.sizeDelta;
      diferenceDimension = new Vector2(canvasDimensions.x - UIDimensions.x, canvasDimensions.y - UIDimensions.y);

      bounds = diferenceDimension / 2;
    }

  }

  protected override void InitTriggers(){

    base.InitTriggers();

    //Create all the Trigger Entires
    EventTrigger.Entry onDragEntry = new EventTrigger.Entry();
    EventTrigger.Entry onBeginDragEntry = new EventTrigger.Entry();

    //Set the Entry IDs
    onDragEntry.eventID = EventTriggerType.Drag;
    onBeginDragEntry.eventID = EventTriggerType.BeginDrag;

    //Set the Entry's callback listeners
    onBeginDragEntry.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data); });
    onDragEntry.callback.AddListener((data) => { MoveUI((PointerEventData)data); });

    //Add the entry into the Triggers
    eventTrigger.triggers.Add(onBeginDragEntry);
    eventTrigger.triggers.Add(onDragEntry);

  }


  #endregion

  #region Public Methods

  virtual public void OnBeginDrag(PointerEventData data){
    if (data.button == PointerEventData.InputButton.Left){
      pDragPos = data.currentInputModule.input.mousePosition;
    }
  }

  virtual public void MoveUI(PointerEventData data){

    if (data.button == PointerEventData.InputButton.Left){

      Vector3 mousePos = data.currentInputModule.input.mousePosition;

      _rectTransform.localPosition += mousePos - pDragPos;
      ClampUI();

      pDragPos = data.currentInputModule.input.mousePosition;
    }
  }

  virtual public void MoveUI(Vector2 v)
  {
    _rectTransform.localPosition = v;
    ClampUI();
  }

  virtual public void SizeUI(Vector2 v){
    _rectTransform.sizeDelta = v;
    InitBounds();
  }

  #endregion

  #region Protected Methods

  protected void ClampUI(){

    Vector2 clampedPOS = _rectTransform.localPosition;
    Vector2 pos = _rectTransform.localPosition;

    clampedPOS.x = Mathf.Clamp(pos.x, -bounds.x, bounds.x);
    clampedPOS.y = Mathf.Clamp(pos.y, -bounds.y, bounds.y);

    _rectTransform.localPosition = clampedPOS;
  }

  #endregion
}
