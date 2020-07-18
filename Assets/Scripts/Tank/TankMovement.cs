using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{


    [SerializeField] int _playerID = 1;
    [SerializeField] float _speed = 10f;
    [SerializeField] float _rotSpeed = 100f;
    [SerializeField] float _turn = 0;

    private Rigidbody _rb;
    private string _horizontalAxis;
    private string _verticalAxis;
    private float _horizontalInput;
    private float _verticalInput;
    //public int MyProperty { get; set; }

    public int PlayerId
    {
        get { return _playerID; }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // get input settings
        _horizontalAxis = "Horizontal" + _playerID;
        _verticalAxis = "Vertical" + _playerID;

    }

    // Update is called once per frame
    void Update()
    {
        // update inputs value based on axis
        _horizontalInput = _turn = Input.GetAxis(_horizontalAxis);
        _verticalInput = Input.GetAxis(_verticalAxis);
    }

    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }

    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * _verticalInput * _speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        _rb.MovePosition(_rb.position+movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
       float turn = _horizontalInput * _rotSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRot = Quaternion.Euler(0f, _turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        _rb.MoveRotation(_rb.rotation * turnRot);
    }

    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        _rb.isKinematic = false;

        // Also reset the input values.
        _horizontalInput = 0;
        _verticalInput = 0;

    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        _rb.isKinematic = true;

    }
}
