using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField] private Interactable _keypad;
    private Animator _animator;
    private static int OpenAnim = Animator.StringToHash("Open");
    private static int CloseAnim = Animator.StringToHash("Close");
    private bool _isOpen = false;
    private bool _isProccessing = false;

    private void OnEnable()
    {
        _keypad.OnClick += DoDoor;
    }

    private void OnDisable()
    {
        _keypad.OnClick -= DoDoor;
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void DoDoor()
    {
        if (!_isProccessing)
        {
            if (_isOpen)
                CloseDoor();
            else
                OpenDoor();
            _isProccessing = true;
        }
    }

    public void OpenDoor()
    {
        _animator.SetTrigger(OpenAnim);
    }

    public void CloseDoor()
    {
        _animator.SetTrigger(CloseAnim);
    }

    public void EndDoorAnimation()
    {
        _isProccessing = false;
        _isOpen = !_isOpen;
        Debug.Log("EndAnimation");
    }
}
