using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]

public abstract class Focuser : MonoBehaviour
{
  public DynamicUI Focused { get { return _focused; } set { ChangeFocused(value); } }
  public DynamicUI PreviousFocused { get { return _previousFocused; } set { _previousFocused = value; } }
  public RectTransform RectTransform { get { return _rectTransform; } set { _rectTransform = value; } }
  public List<DynamicUI> Focusables { get { return _focusables; } }

  private DynamicUI _focused;
  private DynamicUI _previousFocused;
  private List<DynamicUI> _focusables =  new List<DynamicUI>();
  protected RectTransform _rectTransform;

  protected virtual void Awake()
  {
    _rectTransform = GetComponent<RectTransform>();
  }

  protected void Start()
  {
    FindAllFocusables();
  }

  #region Public Methods

  public DynamicUI GetFocusable(int index) {
    if (_focusables == null || _focusables.Count == 0) return null;

    return _focusables[index];
  }

  public void AddFocusable(DynamicUI dui) {
    if (!_focusables.Contains(dui)) _focusables.Add(dui);
  }

  public void AddFocusables(DynamicUI[] duis) {
    foreach (DynamicUI dui in duis) AddFocusable(dui);
  }

  #endregion

  #region Private Methods

  private void FindAllFocusables() {
    DynamicUI[] duis = GetComponentsInChildren<DynamicUI>();
    if (duis != null && duis.Length > 0) AddFocusables(duis);
  }

  private void ChangeFocused(DynamicUI dui) {

    //Set the exsisting Focused Window to unfocused
    if (_focused != null){
      Focus(_focused, false);
    }

    if (dui != null) Focus(dui, true);
  }

  #endregion

  #region Vritual Methods

  protected virtual void Focus(DynamicUI dui, bool f)
  {

    //Focused
    if (f)
    {
      _focused = dui;
      if (_focused != null) _focused.RectTransform.SetAsLastSibling();
    }
    else
    {
      _previousFocused = _focused;
      _focused = null;
    }
  }

  #endregion
}

