using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { FireArms, Melee, Hauling, Repair}

[System.Serializable]
public class Skill
{
  public float value;
  public SkillType type;

  #region Consturctors
  public Skill(SkillType t)
  {
    type = t;
  }
  #endregion
}

public class Skills: Stats {

  public Skill fireArms = new Skill(SkillType.FireArms);
  public Skill melee = new Skill(SkillType.Melee);
  public Skill hauling = new Skill(SkillType.Hauling);
  public Skill repair = new Skill(SkillType.Repair);

  private Dictionary<SkillType, Skill> _dictionary = new Dictionary<SkillType, Skill>();

  #region Init

  protected override void InitStats(){
    _dictionary.Add(SkillType.FireArms, fireArms);
    _dictionary.Add(SkillType.Hauling, hauling);
    _dictionary.Add(SkillType.Melee, melee);
    _dictionary.Add(SkillType.Repair, repair);
  }

  #endregion

  #region Public Methods

  public bool Check(SkillType type, int toCheck)
  {
    return _dictionary[type].value >= toCheck;
  }

  public override void Randomize()
  {
    foreach (Skill s in _dictionary.Values)
    {
      s.value = Random.Range(minStat, maxStat);
    }
  }

  #endregion
 
}
