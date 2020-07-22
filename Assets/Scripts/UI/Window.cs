using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum WindowState {Null, Open, Closed}

[RequireComponent(typeof(Image))]

public class Window : DynamicUI
{
  public string windowName;
  public static string ExitButtonName = "ExitButton";
  public static string FocusScreenName = "FocusScreen";
  public static string TitleName = "Title";
  public static string ContentName = "Content";

  [Header("Required Components")]
  public Button exitButton;
  public Image focusScreen;
  public TextMeshProUGUI title;
  public RectTransform content;

  public WindowState State { get { return _state; } }
  public WindowManager Manager { get { return _manager; } set { _manager = value; } }

  //Focusing
  private static Color focusedColor =  new Color(0.73333f, 0.72549f, 0.63137f, 255);
  private static Color UnfocusedColour = new Color(0,0,0,0.5f);

  private WindowManager _manager;
  private WindowState _state = WindowState.Closed;
  private UserInput userInput;

  protected override void Awake()
  {
    base.Awake();
    userInput = UserInput.Instance;
    if (gameManager != null) _manager = gameManager.WindowManager;

    transition = Transition.None;
  }

  // Start is called before the first frame update
  protected override void Start()
  {
    base.Start();

    InitFocusing();
    InitWindowName();
    InitExitButton();
    InitContent();

    GetAllGraphics();
    CloseWindow();
  }

  #region Init Methods

  private void InitWindowName() {
    title = (TextMeshProUGUI)Utility.FindComponentInChild<TextMeshProUGUI>(gameObject.transform, title, TitleName);
    title.text = windowName;
  }

  private void InitFocusing() {
    focusScreen = (Image)Utility.FindComponentInChild<Image>(gameObject.transform, focusScreen, FocusScreenName);
    focusScreen.color = UnfocusedColour;
    image.color = focusedColor;
  }

  private void InitExitButton() {
    exitButton = (Button)Utility.FindComponentInChild<Button>(_rectTransform, exitButton, ExitButtonName);
    exitButton.onClick.AddListener(() => CloseWindow());
  }

  private void InitContent() {
    exitButton = (Button)Utility.FindComponentInChild<Button>(_rectTransform, exitButton, ContentName);
  }

  #endregion

  #region Public Methods

  public void OpenWindow() {
    _manager.Focused = this;
    _state = WindowState.Open;
    SetUIgraphics(true);
  }

  public void OpenWindow(Vector2 position)
  {
    RectTransform.position = position;
    ClampUI();
    _manager.Focused = this;
    _state = WindowState.Open;
    SetUIgraphics(true);
  }

  public void CloseWindow(){
    if (_manager != null)_manager.Focused = null;
    _state = WindowState.Closed;
    SetUIgraphics(false);
  }

  override public void MoveUI(PointerEventData data) {

    base.MoveUI(data);

    if (data.button == PointerEventData.InputButton.Left){
      if (!_manager.IsWindowFocused(this)) return;
    }
  }

  public override void SetUIgraphics(bool visible)
  {
    foreach (Graphic g in _graphics)
    {

      if (g.gameObject.name.Equals(FocusScreenName))
      {
        if (_manager != null && _manager.IsWindowFocused(this)) continue;
      }
      g.enabled = visible;
    }
  }

  #endregion


}
