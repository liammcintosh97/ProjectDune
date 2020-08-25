using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoReserve : MonoBehaviour
{
  public List<Magazine> Reserve { get { return _reserve; } }

  public List<Magazine> _reserve = new List<Magazine>();

  #region Public Methods

  public Magazine GetMagazine(ProjectileType pType){
    foreach (Magazine m in _reserve)
    {
      if (m.projectileType == pType)
      {
        Magazine toReturn = m;
        _reserve.Remove(m);
        return toReturn;
      }
    }

    return null;
  }

  public void Add(Magazine m)
  {
    if (!_reserve.Contains(m)) _reserve.Add(m);
  }

  private void Remove(Magazine m){
    if (_reserve.Contains(m)) _reserve.Remove(m);
  }

  #endregion

}
