using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
  public int padding = 10;
  public float radius = 3.5f;
  public float cameraMoveSpeed = 3;
  public bool debug =  false;

  public Vector3 ScreenMouse { get { return _screenMouse; } }
  public Vector3 ScreenMouseRaw { get { return _screenMouseRaw; } }

  private new Camera camera;
  private Actor actor;
  private GameManager gameManager;
  private Vector3 _screenMouse;
  private Vector3 _screenMouseRaw;
  private float minRaidus;
  private float maxRaidus;
  private Vector3 actorPos;
  private Bounds actorBounds;
  private Vector3 cameraPos;
  private Vector2 mousePos;
  private Vector3 mouseDiff;
  private float actorDist;

  private Vector3 minScreenPos;
  private Vector3 maxScreenPos;
  private Vector3 clampedPos;

  private float minX ;
  private float maxX ;
  private float minY ;
  private float maxY ;

  private void Awake(){
    gameManager = GameManager.Instance;

    if (gameManager) {
      actor = gameManager.Actor;
      camera = GetComponent<Camera>();

      minRaidus = radius;
}
  }

  // Start is called before the first frame update
  void Start()
  {
    debug = true;
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void FixedUpdate(){

    CameraUpdate();

    Vector3 newPos = cameraPos;

    newPos = new Vector3(mouseDiff.x + actorPos.x, mouseDiff.y + actorPos.y, cameraPos.z);
    actorDist = Vector2.Distance(newPos, actorPos);

    if (actorDist > radius){
      Vector3 norm = mouseDiff.normalized;
      newPos = new Vector3((norm.x * radius) + actorPos.x, (norm.y * radius) + actorPos.y, cameraPos.z);
    }

    transform.position = newPos;
    ClampCamera();

  }

  #region Public Methods

  public void SetZoom(float multiplier) {

    radius = minRaidus * multiplier;
  }

  #endregion

  #region Private Methods

  private void CameraUpdate() {

   //Get Required Postions
    actorPos = actor.transform.position;
    actorBounds = actor.GetComponent<BoxCollider2D>().bounds;
    cameraPos = transform.position;
    mousePos = Input.mousePosition;

    //Get Screen Mouse Position
    _screenMouse = camera.ScreenToWorldPoint(new Vector3(Mathf.Clamp(mousePos.x,0,Screen.width), Mathf.Clamp(mousePos.y,0,Screen.height), actorPos.z - cameraPos.z));
    _screenMouseRaw = camera.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y, actorPos.z - cameraPos.z));
    mouseDiff = (_screenMouse - actorPos) * (Time.deltaTime * cameraMoveSpeed);

    CalculateBounds();
  }

  private void CalculateBounds() {
    minX = 0 + padding;
    maxX = Screen.width - padding;
    minY = 0 + padding;
    maxY = Screen.height - padding;

    minScreenPos = camera.ScreenToWorldPoint(new Vector3(minX, minY, actorPos.z - cameraPos.z));
    maxScreenPos = camera.ScreenToWorldPoint(new Vector3(maxX, maxY, actorPos.z - cameraPos.z));
  }
  
  private void ClampCamera(){

    clampedPos = transform.position;

    Vector3 bounds = actorBounds.size;

    //Minimum X
    if (actorPos.x - bounds.x < minScreenPos.x){
      clampedPos.x += (actorPos.x - minScreenPos.x) - bounds.x;
    }

    //Maximum X
    if (actorPos.x + bounds.x > maxScreenPos.x){
      clampedPos.x += (actorPos.x - maxScreenPos.x) + bounds.x;
    }

    //Minimum y
    if (actorPos.y - bounds.y < minScreenPos.y) {
      clampedPos.y += (actorPos.y - minScreenPos.y) - bounds.y;    
    }

    //Maximum Y
    if (actorPos.y + bounds.y > maxScreenPos.y){
      clampedPos.y += (actorPos.y - maxScreenPos.y) + bounds.y;
    }

    transform.position = clampedPos;
  }


  #endregion

  #region Gizmos

  void OnDrawGizmosSelected()
  {
    if (debug) {
      Gizmos.color = Color.blue;
      Handles.color = Color.blue;

      Gizmos.DrawWireSphere(minScreenPos, 0.1f);
      Gizmos.DrawWireSphere(maxScreenPos, 0.1f);
      Gizmos.DrawWireCube(actorBounds.center, actorBounds.size);

      Handles.Label(minScreenPos, "Min Screen Pos : " + minScreenPos + "\n Min Screen Pixels : (" + minX + ", " + minY + ")");
      Handles.Label(maxScreenPos, "Max Screen Pos : " + maxScreenPos + "\n Max Screen Pixels : (" + maxX + ", " + maxY + ")");
      Handles.Label(cameraPos, "Camera Pos : " + cameraPos);
      Handles.Label(actorPos, "Actor Pos : " + actorPos);
      Handles.Label(camera.ScreenToWorldPoint(mousePos), "Mouse Pos: " + mousePos);
 
      //Draw Clamp 
      Gizmos.DrawLine(minScreenPos, new Vector3(maxScreenPos.x, minScreenPos.y, minScreenPos.z));
      Gizmos.DrawLine(minScreenPos, new Vector3(minScreenPos.x, maxScreenPos.y, minScreenPos.z));
      Gizmos.DrawLine(maxScreenPos, new Vector3(minScreenPos.x, maxScreenPos.y, maxScreenPos.z));
      Gizmos.DrawLine(maxScreenPos, new Vector3(maxScreenPos.x, minScreenPos.y, maxScreenPos.z));
    } 
  }

  #endregion
}
