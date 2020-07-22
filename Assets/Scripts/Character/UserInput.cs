using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserInput : Singleton<UserInput>
{
  public float KeyHoldTime { get{return _keyHoldTime;} }
  public KeyCode HeldKey { get { return _heldKey; } }

  public float MouseHoldTime { get { return _mouseHoldTime; } }
  public int HeldMouse { get { return _heldMouse; } }
  public PointerEventData eventData;

  private float _keyHoldTime;
  private KeyCode _heldKey;
  private float _mouseHoldTime;
  private int _heldMouse = -1;

  public Vector2 GetInputDir() {
    return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
  }

  public Vector2 GetMouseDir() {
    return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
  }

  #region Mouse Input

  public bool MousePress(int button) { return Input.GetMouseButtonDown(button); }

  public bool MouseHold(int button) {
    _heldMouse = button;
    _mouseHoldTime += Time.deltaTime;

    return Input.GetMouseButtonDown(button);
  }

  public bool MouseRelease(int button) {
    if (_heldMouse == button)
    {
      _mouseHoldTime = 0;
      _heldMouse = -1;
    }

    return Input.GetMouseButtonUp(button);
  }

  #endregion

  #region Key Input
  public bool KeyHold(KeyCode k) {
    _heldKey = k;
    _keyHoldTime += Time.deltaTime;

    return Input.GetKey(k);
  }

  public bool KeyRelease(KeyCode k) {

    if (_heldKey == k) {
      _keyHoldTime = 0;
      _heldKey = KeyCode.None;
    }

    return Input.GetKeyUp(k);
  }

  public bool KeyPress(KeyCode k) { return Input.GetKeyDown(k);}

  public int PressHotKey() {

    if (KeyPress(KeyCode.Alpha1))return 1;
    if (KeyPress(KeyCode.Alpha2)) return 2;
    if (KeyPress(KeyCode.Alpha3)) return 3;
    if (KeyPress(KeyCode.Alpha4)) return 4;
    if (KeyPress(KeyCode.Alpha5)) return 5;
    if (KeyPress(KeyCode.Alpha6)) return 6;
    if (KeyPress(KeyCode.Alpha7)) return 7;
    if (KeyPress(KeyCode.Alpha8)) return 8;
    if (KeyPress(KeyCode.Alpha9)) return 9;
    if (KeyPress(KeyCode.Alpha0)) return 10;

    return -1;
  }
  #endregion


}
