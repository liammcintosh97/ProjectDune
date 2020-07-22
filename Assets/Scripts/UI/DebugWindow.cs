using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Image))]

public class DebugWindow : StaticUI
{
  public GameObject debugElementPrefab;
  public float elementPadding = 10;

  public bool IsShowing { get{ return _isShowing; } }
  public List<DebugElement> Elements { get { return _elements; } }

  private bool _isShowing;
  private List<DebugElement> _elements = new List<DebugElement>();

  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();
    GetAllGraphics();
    SetUIgraphics(false);
    SetSize();
  }

  // Update is called once per frame
  void Update()
  {
    if (userInput && userInput.KeyPress(KeyCode.Tab)) ToggleWindow();
  }

  #region Public Methods

  public void Log(string name, string value, int position) {
    try
    {
      DebugElement de = _elements[position];
      de.UpdateText(name, value);
    }
    catch (Exception e) {

      DebugElement de = InitializeElement(position);
      de.UpdateText(name, value);
    }

  }

  public void ToggleWindow() {
    if (_isShowing) ShowWindow(false);
    else ShowWindow(true);
  }

  public void ShowWindow(bool show) {
    SetUIgraphics(show);
    _isShowing = show;
  }

  public void SetSize() {
    float elementHeight = debugElementPrefab.GetComponent<RectTransform>().sizeDelta.y;
    float elementSpace = elementHeight + (2 * elementPadding);
    Vector2 newSize;

    if (_elements.Count > 0) newSize = new Vector2(_rectTransform.sizeDelta.x, _elements.Count * elementSpace);
    else newSize = new Vector2(_rectTransform.sizeDelta.x,elementSpace);


    _rectTransform.sizeDelta = newSize;
  }

  #endregion

  #region Private Methods

  private DebugElement InitializeElement(int position) {
    GameObject element = Instantiate(debugElementPrefab);
    DebugElement debugElement = element.GetComponent<DebugElement>();

    debugElement.Init(this, position);
    _elements.Add(debugElement);
    AddGraphics(debugElement.Graphics);
    SetSize();

    if (_isShowing) debugElement.SetUIgraphics(true);

    return debugElement;
  }


  #endregion
}
