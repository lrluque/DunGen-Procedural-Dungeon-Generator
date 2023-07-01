using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _fastSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveTime;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private Vector3 _newPosition;
    [SerializeField] private Vector3 _dragOrigin;
    [SerializeField] private Vector3 _dragCurrent;

    void Start()
    {
        _newPosition = transform.position;
    }

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {   
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveSpeed = _fastSpeed;
        }
        else
        {
            _moveSpeed = _normalSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _newPosition += (transform.forward * _moveSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _newPosition += (transform.forward * -_moveSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _newPosition += (transform.right * -_moveSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _newPosition += (transform.right * _moveSpeed);
        }

        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _moveTime);
    }


}
