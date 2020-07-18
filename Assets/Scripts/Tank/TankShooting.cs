using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{

    [SerializeField] private ShellExplosion _shellPrefab;
    [SerializeField] private float _minFireForce = 10;
    [SerializeField] private float _maxFireForce = 35;
    [SerializeField] private float _chargeTime = 0.65f;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Slider _aimSlider;

    private bool _fire = false;
    private float _currentFireForce;
    private float _chargeSpeed;
    private string _fireButton;

    void Start()
    {

        // get input Fire Button
        _fireButton = "Fire" + GetComponent<TankMovement>().PlayerId;
        print("fire butt " + _fireButton);

        // calculate charge value 
        _chargeSpeed = (_maxFireForce - _minFireForce) / _chargeTime;

        _aimSlider.minValue = _minFireForce;
        _aimSlider.maxValue = _maxFireForce;
    }

    // Update is called once per frame
    void Update()
    {
        // The slider should have a default value of the minimum launch force.
        _aimSlider.value = _minFireForce;

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if(_currentFireForce >= _maxFireForce && !_fire)
        {
            _currentFireForce = _maxFireForce;
            Fire();
        }
        // Otherwise, if the fire button has just started being pressed...
        else if(Input.GetButtonDown(_fireButton))
        {
            // ... reset the fired flag and reset the launch force.
            _fire = false;
            _currentFireForce = _minFireForce;
            // Change the clip to the charging clip and start it playing.
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if(Input.GetButton(_fireButton)&&!_fire)
        {
            // Increment the launch force and update the slider.
            _currentFireForce += _chargeSpeed * Time.deltaTime;
            _aimSlider.value = _currentFireForce;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        else if(Input.GetButtonUp(_fireButton)&&!_fire)
        {
            Fire();
        }

    }

    private void Fire()
    {
        _fire = true;
        // Set the fired flag so only Fire is only called once.

        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance = Instantiate(_shellPrefab.GetComponent<Rigidbody>(), _firePoint.position, _firePoint.rotation) as Rigidbody;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = _currentFireForce * _firePoint.forward;

        // Change the clip to the firing clip and play it.

        // Reset the launch force.  This is a precaution in case of missing button events.
        _currentFireForce = _minFireForce;
    }
}
