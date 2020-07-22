using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(RectTransform))]

public class StaticUI : Selectable
{
  public Canvas Canvas { get { return _canvas; } }
  public RectTransform RectTransform { get { return _rectTransform; } }
  public List<Graphic> Graphics { get { return _graphics; } }

  protected List<Graphic> _graphics = new List<Graphic>();
  protected RectTransform _rectTransform;
  protected EventTrigger eventTrigger;

  protected UserInput userInput;
  protected GameManager gameManager;
  protected Canvas _canvas;

  protected override void Awake(){
    base.Awake();
    _rectTransform = GetComponent<RectTransform>();
    gameManager = GameManager.Instance;
    userInput = UserInput.Instance;
    transition = Transition.None;

    InitCanvas();
    InitTriggers();
  }

  protected override void Start()
  {
    base.Start();
  }

  #region Private Methods

  private void InitCanvas() {
    if (gameManager != null)
    {
      _canvas = gameManager.Canvas;
     
    }
  }

  protected virtual void InitTriggers() {

    eventTrigger = GetComponent<EventTrigger>();
    eventTrigger.triggers.Clear();

    //Create all the Trigger Entires
    EventTrigger.Entry onPointerEnterEntry = new EventTrigger.Entry();
    EventTrigger.Entry onPointerClickEntry = new EventTrigger.Entry();
    EventTrigger.Entry onPointerUpEntry = new EventTrigger.Entry();
    EventTrigger.Entry onPointerExitEntry = new EventTrigger.Entry();

    //Set the Entry IDs
    onPointerEnterEntry.eventID = EventTriggerType.PointerEnter;
    onPointerClickEntry.eventID = EventTriggerType.PointerClick;
    onPointerUpEntry.eventID = EventTriggerType.PointerUp;
    onPointerExitEntry.eventID = EventTriggerType.PointerExit;

    //Set the Entry's callback listeners
    onPointerEnterEntry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
    onPointerClickEntry.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
    onPointerUpEntry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
    onPointerExitEntry.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });

    //Add the entry into the Triggers
    eventTrigger.triggers.Add(onPointerEnterEntry);
    eventTrigger.triggers.Add(onPointerClickEntry);
    eventTrigger.triggers.Add(onPointerUpEntry);
    eventTrigger.triggers.Add(onPointerExitEntry);
  }

  #endregion

  #region Protected Methods

  protected void GetAllGraphics(){
    Graphic[] gs = _rectTransform.GetComponents<Graphic>();
    if (gs != null && gs.Length > 0) AddGraphics(gs);

    GetGraphicsInChildren(_rectTransform);
  }

  protected void GetGraphicsInChildren(Transform t) {

    foreach (Transform child in t) {
      if (child.GetComponent<StaticUI>() != null) continue;

      Graphic[] graphics = child.GetComponents<Graphic>();

      if (graphics != null && graphics.Length > 0) AddGraphics(graphics);

      GetGraphicsInChildren(child);
    }
  }

  #endregion

  #region Public Methods

  public override void OnPointerExit(PointerEventData data) { base.OnPointerExit(data);}

  public override void OnPointerEnter(PointerEventData data) { base.OnPointerEnter(data); }

  public virtual void OnPointerClick(PointerEventData data) { }

  public override void OnPointerUp(PointerEventData data) { base.OnPointerUp(data); }

  public void AddGraphic(Graphic g)
  {
    if (!_graphics.Contains(g)) _graphics.Add(g);
  }

  public void AddGraphics(List<Graphic> gs)
  {
    foreach (Graphic g in gs)
    {
      AddGraphic(g);
    }
  }

  public void AddGraphics(Graphic[] gs) {

    foreach (Graphic g in gs)
    {
      AddGraphic(g);
    }
  }

  public void RemoveGraphic(Graphic g)
  {
    if (_graphics.Contains(g)) _graphics.Remove(g);
  }

  public void RemoveGraphics(List<Graphic> gs)
  {
    foreach (Graphic g in gs)
    {
      RemoveGraphic(g);
    }
  }

  public virtual void SetUIgraphics(bool visible)
  {

    foreach (Graphic g in _graphics){
       g.enabled = visible;
    }
  }

  #endregion
}
