using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

  public Actor Actor { get { return _actor; } }
  public Canvas Canvas { get { return _canvas; } }
  public UIItemManager UIItemManager { get { return _UIItemManager; } }
  public WindowManager WindowManager { get { return _windowManager; } }
  public ItemManager ItemManager { get { return _itemManager; } }
  public Equipment Equipment { get { return _equipment; } }
  public HotKeys HotKeys { get { return _hotKeys; } }
  public DebugWindow DebugWindow { get { return _debugWindow; } }
  public PlayerCamera PlayerCamera { get { return _playerCamera; } }


  private Canvas _canvas;
  private UIItemManager _UIItemManager;
  private WindowManager _windowManager;
  private ItemManager _itemManager;
  private Equipment _equipment;
  private HotKeys _hotKeys;
  private Actor _actor;
  private DebugWindow _debugWindow;
  private PlayerCamera _playerCamera;

  // Start is called before the first frame update
  void Awake()
  {
    _actor = FindObjectOfType<Actor>();
    _canvas = FindObjectOfType<Canvas>();
    _UIItemManager = FindObjectOfType<UIItemManager>();
    _windowManager = FindObjectOfType<WindowManager>();
    _equipment = FindObjectOfType<Equipment>();
    _itemManager = FindObjectOfType<ItemManager>();
    _hotKeys = FindObjectOfType<HotKeys>();
    _debugWindow = FindObjectOfType<DebugWindow>();
    _playerCamera = FindObjectOfType<PlayerCamera>();
  }


  // Update is called once per frame
  void Update()
  {

  }
}
