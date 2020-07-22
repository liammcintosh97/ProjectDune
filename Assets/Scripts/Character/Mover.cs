using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Mover : MonoBehaviour
{
  private UserInput userInput;
  public Character character;
  public Rigidbody2D rigidbody2D;

  private GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {
    userInput = UserInput.Instance;

    //Check Required Components
    character = (Character)Utility.ComponentCheck<Character>(gameObject, character);
    rigidbody2D = GetComponent<Rigidbody2D>();

    gameManager = GameManager.Instance;
  }

  // Update is called once per frame
  void Update()
  {
    SetCharacterSpeed();
    MoveCharacter(UserInput.Instance.GetInputDir());
  }

  private void MoveCharacter(Vector2 input)
  {
    input *= character.speed.Current;

    Vector3 newVelocity = (transform.up * input);
    rigidbody2D.velocity = new Vector2(input.x, input.y);
  }

  private void SetCharacterSpeed()
  {
    Debug.Log(character.endurance.Current);

    if(!userInput.KeyHold(KeyCode.LeftShift)) character.endurance.Heal(25);

    if (userInput.KeyPress(KeyCode.LeftShift)) character.speed.Set(SpeedType.Run);

    if (userInput.KeyHold(KeyCode.LeftShift) && character.endurance.Current > 0)
    {
      character.endurance.Drain(35);
    }
    else if (userInput.KeyHold(KeyCode.LeftShift)) character.speed.Set(SpeedType.Walk);
  }
}
