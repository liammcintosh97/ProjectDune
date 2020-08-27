using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStatTypes {Strength,Endurance,Dexterity,Intelligence,Charisma,Will}

[System.Serializable]
public class CharacterStat
{
  public int value;
  public CharacterStatTypes type;

  #region Consturctors
  public CharacterStat(CharacterStatTypes t)
  {
    type = t;
  }
  #endregion
}

[System.Serializable]
public class CharacterStats{

  public CharacterStat strength = new CharacterStat(CharacterStatTypes.Strength);
  public CharacterStat endurance = new CharacterStat(CharacterStatTypes.Endurance);
  public CharacterStat dexterity = new CharacterStat(CharacterStatTypes.Dexterity);
  public CharacterStat intelligence = new CharacterStat(CharacterStatTypes.Intelligence);
  public CharacterStat Charisma = new CharacterStat(CharacterStatTypes.Charisma);
  public CharacterStat Will = new CharacterStat(CharacterStatTypes.Will);

  private int minSkill = 0;
  private int maxSkill = 100;
  private Dictionary<CharacterStatTypes, CharacterStat> _stats = new Dictionary<CharacterStatTypes, CharacterStat>();

  #region Constructors
  public CharacterStats() {
    InitDictionary();
  }
  #endregion

  #region Init

  public void InitDictionary() {
    _stats.Add(CharacterStatTypes.Strength, strength);
    _stats.Add(CharacterStatTypes.Endurance, endurance);
    _stats.Add(CharacterStatTypes.Dexterity, dexterity);
    _stats.Add(CharacterStatTypes.Intelligence, intelligence);
    _stats.Add(CharacterStatTypes.Charisma, Charisma);
    _stats.Add(CharacterStatTypes.Will, Will);
  }

  #endregion

  #region Public Methods

  public bool Check(CharacterStatTypes type, int toCheck) {
    return _stats[type].value >= toCheck;
  }

  public void Randomize(){
    foreach (CharacterStat s in _stats.Values) {
      s.value = Random.Range(minSkill, maxSkill);
    }
  }
   
  #endregion
}
