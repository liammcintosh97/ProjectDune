using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class WindowManager : Focuser
{
  private Canvas canvas;
  private UserInput userInput;

  protected override void Awake(){
    base.Awake();
    userInput = UserInput.Instance;
    canvas = (Canvas)Utility.ComponentCheck<Canvas>(gameObject, canvas);
  }


  // Update is called once per frame
  void Update()
  {
    if (userInput.MousePress(0)) FocusOnWindow();
  }

  #region Public Methods

  public Window InstantiateWindow(GameObject windowPrefab) {

    GameObject wg = Instantiate(windowPrefab, _rectTransform);
    Window w = wg.GetComponent<Window>();
    AddFocusable(w);

    return w;
  }

  public void CloseAllWindows() {
    foreach (DynamicUI dui in Focusables) {
      Window w = (Window)dui;

      if (w != null) w.CloseWindow();
    }
  }

  public bool HasWindowOpen() {
    foreach (DynamicUI dui in Focusables){
      Window w = (Window)dui;

      if (w != null) {
        if (w.State == WindowState.Open) return true;
      }
    }

    return false;
  }

  public bool IsWindowFocused(Window w) {
    if (Focused == null) return false;

    return ((Window)Focused).Equals(w);
  }

  #endregion

  #region Private Methods

  private void FocusOnWindow()
  {
    GraphicRaycaster m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
    EventSystem m_EventSystem = canvas.GetComponent<EventSystem>();
    PointerEventData m_PointerEventData;
    List<RaycastResult> results = new List<RaycastResult>();

    m_PointerEventData = new PointerEventData(m_EventSystem);
    m_PointerEventData.position = Input.mousePosition;

    m_Raycaster.Raycast(m_PointerEventData, results);

    if (results == null || results.Count == 0) CloseAllWindows();

    foreach (RaycastResult r in results) {
      Window w = r.gameObject.GetComponent<Window>();

      //Found the window so focus on it
      if (w != null){

        //Defocus focused window
        try{
          Window cf = (Window)Focused;

        } catch (Exception e) { Debug.Log(e + " is not a window"); }

        w.focusScreen.enabled = false;
        Focused = w;

        return;
      }
    }
  }

  #endregion

  #region virtual Methods

  protected override void Focus(DynamicUI dui, bool f){
    base.Focus(dui, f);

    if(f) ((Window)dui).focusScreen.enabled = false;
    else ((Window)dui).focusScreen.enabled = true;
  }

  #endregion
}
