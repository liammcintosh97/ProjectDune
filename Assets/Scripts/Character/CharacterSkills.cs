using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterSkillType { FireArms, Melee, Hauling, Repair}

[System.Serializable]
public class CharacterSkill
{
  public float value;
  public CharacterSkillType type;

  #region Consturctors
  public CharacterSkill(CharacterSkillType t)
  {
    type = t;
  }
  #endregion
}

public class CharacterSkills {

  public CharacterSkill fireArms;
  public CharacterSkill melee;
  public CharacterSkill hauling;
  public CharacterSkill repair;

  private static float minSkill = 0;
  private static float maxSkill = 100;
  private Dictionary<CharacterSkillType, CharacterSkill> _skills = new Dictionary<CharacterSkillType, CharacterSkill>();

  #region Constructors
  public CharacterSkills(){
    InitStats();
  }
  #endregion

  #region Init

  private void InitStats(){
    _skills.Add(CharacterSkillType.FireArms, fireArms);
    _skills.Add(CharacterSkillType.Hauling, hauling);
    _skills.Add(CharacterSkillType.Melee, melee);
    _skills.Add(CharacterSkillType.Repair, repair);
  
  }

  #endregion


  #region Public Methods

  public bool Check(CharacterSkillType type, int toCheck)
  {
    return _skills[type].value >= toCheck;
  }

  public void Randomize()
  {
    foreach (CharacterSkill s in _skills.Values)
    {
      s.value = Random.Range(minSkill, maxSkill);
    }
  }

  #endregion
 
}
