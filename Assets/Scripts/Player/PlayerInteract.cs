using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private LayerMask _maskInteractable;
        [SerializeField] private float _maxDistance = 1f;
        private InputManager _inputManager;
        private Camera _camera;
        private Interactable _currentInteractable;

        private void OnValidate()
        {
            if (_maxDistance < 0f) _maxDistance = 0f;
        }
        
        private void Awake()
        {
            _camera = Camera.main;
            _inputManager = GetComponent<InputManager>();
        }

        private void Update()
        {
            if (CheckInteractable())
            {
                _currentInteractable.OnEnterZone?.Invoke();
                if (_inputManager.PlayerAction.Interact.triggered)
                {
                    _currentInteractable.OnClick?.Invoke();
                }
            }
            else
            {
                if (_currentInteractable != null)
                {
                    _currentInteractable.OnOutZone?.Invoke();
                    _currentInteractable = null;
                }
            }
        }

        private bool CheckInteractable()
        {
            
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            DebugRay(ray);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, _maxDistance, _maskInteractable))
            {
                if (hitInfo.transform.TryGetComponent(out Interactable interact))
                {
                    _currentInteractable = interact;
                    return true;
                }
            }

            return false;
        }

        private void DebugRay(Ray ray)
        {
            Color color = Color.red;
            Debug.DrawRay(ray.origin, ray.direction * _maxDistance, color);
        }
    }
}