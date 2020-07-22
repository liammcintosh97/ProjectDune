using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Utility
{
  public static Component ComponentCheck<T>(GameObject g, Component c) {

    if (c == null)
    {
      c = FindComponent<T>(g);
      if (c == null) return null;
      else return c;
    }
    return c;
  }

  public static Component FindComponentInChild<T>(Transform parent, Component c, string childName){

    if (c != null && c.GetType() == typeof(T)) return c;

    Transform t = SearchChildThroughHierachy(parent, childName);
    if (t == null){
      throw new Exception("Could not find the child object named " + childName);
    }

    Component nc = t.GetComponent(typeof(T));
    if (nc == null) {
      throw new Exception("Could not find component of type " + typeof(T));
    }

    return nc;
  }

  public static bool IsBetween(double testValue, double bound1, double bound2){
    return (testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2));
  }

  public static bool IsBetween(float value, float min, float max) {
    return value > min && value < max;
  }

  public static bool EpsilonDist(Vector3 v1, Vector3 v2) {
    float dist = Vector3.Distance(v1, v2);

    return dist <= Mathf.Epsilon;
  }

  public static bool EpsilonDist(float f1, float f2)
  {
    float dist = f1 - f2;

    return dist <= Mathf.Epsilon;
  }

  #region Private Methods
  private static Component FindComponent<T>(GameObject g) {
    //Goes through all methods of component retreaval until it finds the requested component or returns null
    Component c;

    c = g.GetComponent(typeof(T));
    if (c == null) c = g.GetComponentInChildren(typeof(T));
    if (c == null) c = g.GetComponentInParent(typeof(T));

    if (c == null) throw new Exception("Could not find Component of type " + typeof(T));

    return c;
  }

  private static Transform SearchChildThroughHierachy(Transform p, string childName) {

    Queue<Transform> queue = new Queue<Transform>();
    queue.Enqueue(p);

    while (queue.Count > 0)
    {
      var c = queue.Dequeue();
      if (c.name == childName)
        return c;
      foreach (Transform t in c)
        queue.Enqueue(t);
    }
    return null;
  }

  #endregion

}
