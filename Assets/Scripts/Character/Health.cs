using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{

  public float max;

  public float Value { get { return _value; } }
  private float _value;

  public Health() {
    _value = max;
  }

  #region Public Methodsa

  public void Subtract(float _amount) {
    _value -= _amount;
  }

  public void SetValue(float _newValue){
    _value -= _newValue;
  }

  public void Heal(float _healAmount) {
    _value += _healAmount;
  }

  #endregion

}
