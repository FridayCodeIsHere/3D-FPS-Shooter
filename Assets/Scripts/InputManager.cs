using UnityEngine;

public class InputManager : MonoBehaviour
{
    private UserInput _userInput;

    public UserInput.PlayerActions PlayerAction => _userInput.Player;
    private void OnEnable()
    {
        _userInput.Enable();
    }

    private void OnDisable()
    {
        _userInput.Disable();
    }
    
    private void Awake()
    {
        _userInput = new UserInput();
    }
    
}
