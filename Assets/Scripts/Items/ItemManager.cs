using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
  public static string PickedUpName = "PickedUp";
  public static string GroundName = "Ground";

  public Transform PickedUpTransform { get { return _pickedUpTransform; } }
  public Transform GroundTransform { get { return _groundTransform; } }

  private Transform _pickedUpTransform;
  private Transform _groundTransform;

  // Start is called before the first frame update
  private void Start()
  {
    _pickedUpTransform = GameObject.Find(PickedUpName).GetComponent<Transform>();
    if (_pickedUpTransform == null) throw new System.Exception("Can't Find PickedUp Transform");

    _groundTransform = GameObject.Find(GroundName).GetComponent<Transform>();
    if (_pickedUpTransform == null) throw new System.Exception("Can't Find Ground Transform");
  }

}
