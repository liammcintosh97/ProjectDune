using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : Equipable
{
  public bool Holstered {
    get { return _holstered; }
    set { SetHolstered(value); }
  }

  private bool _holstered;

  private void SetHolstered(bool h) {  _holstered = h; }

  public bool SwitchHolstered() {
    if (_holstered) SetHolstered(false);
    else SetHolstered(true);

    return _holstered;
  }
}
