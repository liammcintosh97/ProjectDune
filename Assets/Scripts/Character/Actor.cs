using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actor : MonoBehaviour
{
  public static int pixelSize = 32;
  public float mouseLookSpeed = 5;
  public float mouseAimSpeed = 5;

  public bool debug = false;

  public Equipable inHand;
  public Character Character { get { return _character; } }
  public EquipmentManager EquipmentManager { get { return _equipmentManager; } }

  private UserInput input;
  private HotKeys hotKeys;
  private Equipment equipment;
  private WindowManager windowManager;
  private Character _character;
  private EquipmentManager _equipmentManager;
  private PlayerCamera playerCamera;

  private Vector3 followLookPoint;
  private Vector3 followToDir;
  private Quaternion followToRotation;

  private Vector3 aimLookPoint;
  private Vector3 aimToDir;
  private Quaternion aimToRotation;

  private void Awake()
  {
    hotKeys = GameManager.Instance.HotKeys;
    windowManager = GameManager.Instance.WindowManager;
    equipment = GameManager.Instance.Equipment;
    input = UserInput.Instance;
    _character = GetComponent<Character>();
    _equipmentManager = transform.GetChild(0).GetComponent<EquipmentManager>();
    playerCamera = GameManager.Instance.PlayerCamera;
  }

  private void Start()
  {
    debug = true;
  }

  // Update is called once per frame
  void Update(){
    if (input.MousePress(0)) UseInHand();
    if (input.KeyPress(KeyCode.I)) ToggleEquipment();
    if (HolsterCheck(KeyCode.R)) HolsterWeapon();
    if (input.KeyPress(KeyCode.R)) ReloadFireArm();
    if (input.MouseHold(1)) Aim(true);
    if (input.MouseRelease(1)) Aim(false);
  }

  private void FixedUpdate()
  {
    LookAtMouse();

    AimAtMouse();
  }

  #region Public Methods
  public void UseInHand() {
    if(inHand)inHand.Use(this);
  }

  public void SetInhand(Equipable e) { inHand = e; }

  public void ReloadFireArm()
  {
    try
    {

      FireArm fa = (FireArm)inHand;

      Magazine mag = _equipmentManager.ammoReserve.GetMagazine(fa.projectileType);

      if (mag){
        fa.Reload(new object[] { mag, transform.position });
      }
      else {
        Debug.Log("There are no magazines of the type " + fa.projectileType + " in the reserves");
      }
     
      Debug.Log("Reload");
    }
    catch (InvalidCastException) {
      Debug.Log("The item in hand is not a FireArm");
    }
  }

  public void HolsterWeapon()
  {
    try
    {
      Weapon w = (Weapon)inHand;
      w.SwitchHolstered();
      Debug.Log("Holstered = " + w.Holstered);
    }
    catch (InvalidCastException)
    {
      Debug.Log("The item in hand is not a FireArm");
    }
  }

  public void ToggleEquipment() {
    if (windowManager.HasWindowOpen()){
      windowManager.CloseAllWindows();
    }
    else {
      equipment.OpenWindow(Input.mousePosition);
    }
  }

  public void PickUpItem(Item item) {

    EquipmentSlot potentailSlot = equipment.CheckEquipmentSlots(item.UIItem);

    if (potentailSlot){
      item.PickUp(false);
      potentailSlot.Slot(item.UIItem);
    }
    else{
    
      bool stored = _equipmentManager.storage.Store(item);
      if (!stored){
        item.PickUp(true);
        equipment.OpenWindow();
      }
      else item.PickUp(false);
    }
  }

  public void Aim(bool aiming) {

    if (aiming){
      if (inHand && inHand.GetType() == typeof(FireArm)){
        FireArm fireArm = (FireArm)inHand;
        playerCamera.SetZoom(fireArm.zoomMultiplyer);
      }
    }
    else playerCamera.SetZoom(0);
  }
  #endregion

  #region Private Method
  private bool HolsterCheck(KeyCode k) {

    if (inHand && inHand.GetType() == typeof(Weapon)) {

      Weapon weapon = (Weapon)inHand;
      float holsterTime = weapon.holsterTime;

      return input.KeyHold(k) && (input.HeldKey == k) && (input.KeyHoldTime == holsterTime);

    } else return false;


    
  }

  private void LookAtMouse() {

    float playerLookSpeed = mouseLookSpeed * Time.deltaTime;

    //followLookPoint = new Vector3(playerCamera.ScreenMouseRaw.x - transform.position.x, playerCamera.ScreenMouseRaw.y - transform.position.y, transform.position.z);
    followLookPoint = new Vector3(playerCamera.ScreenMouseRaw.x, playerCamera.ScreenMouseRaw.y, transform.position.z);

    followToDir = followLookPoint - transform.position;

    Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 1) * followToDir;
    followToRotation = Quaternion.LookRotation(forward: transform.forward, upwards: followToDir);

    transform.rotation = Quaternion.RotateTowards(transform.rotation, followToRotation, playerLookSpeed);
  }

  
  private void AimAtMouse(){

    Transform rightHand = _equipmentManager.rightHand;
    float playerAimSpeed = mouseAimSpeed * Time.deltaTime;

    //aimLookPoint = new Vector3(playerCamera.ScreenMouseRaw.x - rightHand.position.x, playerCamera.ScreenMouseRaw.y - rightHand.position.y, rightHand.position.z);
    aimLookPoint = new Vector3(playerCamera.ScreenMouseRaw.x, playerCamera.ScreenMouseRaw.y, rightHand.position.z);

    aimToDir = aimLookPoint - rightHand.position;

    Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * aimToDir;
    aimToRotation = Quaternion.LookRotation(forward: rightHand.forward, upwards: aimToDir);

    rightHand.rotation = Quaternion.RotateTowards(rightHand.rotation, aimToRotation, playerAimSpeed);
  }
    #endregion

  #region Trigger Methods

  void OnTriggerStay2D(Collider2D other)
  {
    if (input.KeyPress(KeyCode.E)) {
      Item item = other.gameObject.GetComponent<Item>();

      if (item != null) PickUpItem(item);
    }
  }

  #endregion

  #region Debug

  private void OnDrawGizmos()
  {
    if (debug) {
      Gizmos.color = Color.red;

      Gizmos.DrawSphere(followLookPoint, 0.05f);
      Gizmos.DrawLine(transform.position, transform.position + followToDir);

      Gizmos.color = Color.blue;
      Gizmos.DrawLine(transform.position, transform.position + transform.up);

      Transform rightHand = _equipmentManager.rightHand;

      Gizmos.color = Color.green;

      Gizmos.DrawSphere(aimLookPoint, 0.05f);
      Gizmos.DrawLine(rightHand.position, rightHand.position + aimToDir);

      Gizmos.color = Color.cyan;
      Gizmos.DrawLine(rightHand.position, rightHand.position + rightHand.up);
    }  
  }

  #endregion
}
