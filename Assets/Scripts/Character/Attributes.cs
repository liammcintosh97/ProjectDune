using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStatTypes {Strength,Endurance,Dexterity,Intelligence,Charisma,Will}

[System.Serializable]
public class Artibute
{
  public int value;
  public CharacterStatTypes type;

  #region Consturctors
  public Artibute(CharacterStatTypes t)
  {
    type = t;
  }
  #endregion
}

[System.Serializable]
public class Atributes: Stats{

  public Artibute strength = new Artibute(CharacterStatTypes.Strength);
  public Artibute endurance = new Artibute(CharacterStatTypes.Endurance);
  public Artibute dexterity = new Artibute(CharacterStatTypes.Dexterity);
  public Artibute intelligence = new Artibute(CharacterStatTypes.Intelligence);
  public Artibute Charisma = new Artibute(CharacterStatTypes.Charisma);
  public Artibute Will = new Artibute(CharacterStatTypes.Will);

  private Dictionary<CharacterStatTypes, Artibute> _dictionary = new Dictionary<CharacterStatTypes, Artibute>();

  #region Constructors
  public Atributes() {
    InitStats();
  }
  #endregion

  #region Init

  protected override void InitStats() {
    _dictionary.Add(CharacterStatTypes.Strength, strength);
    _dictionary.Add(CharacterStatTypes.Endurance, endurance);
    _dictionary.Add(CharacterStatTypes.Dexterity, dexterity);
    _dictionary.Add(CharacterStatTypes.Intelligence, intelligence);
    _dictionary.Add(CharacterStatTypes.Charisma, Charisma);
    _dictionary.Add(CharacterStatTypes.Will, Will);
  }

  #endregion

  #region Public Methods

  public bool Check(CharacterStatTypes type, int toCheck) {
    return _dictionary[type].value >= toCheck;
  }

  public override void Randomize(){
    foreach (Artibute s in _dictionary.Values) {
      s.value = Random.Range((int)minStat,(int)maxStat);
    }
  }
   
  #endregion
}
