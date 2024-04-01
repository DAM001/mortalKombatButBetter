using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private float _attackForce = 10000f;
    [SerializeField] private float _verticalAttackForce = 10000f;

    [Header("Movement:")]
    [SerializeField] private Transform _target;
    public float _rotationSpeed = 5f;
    public float _movementSpeed = 5f;

    private GameObject _weapon;

    private void Start()
    {
        _weapon = transform.GetChild(0).gameObject;
        _weapon.GetComponent<Weapon>().SetParent(transform);
        _weapon.transform.parent = null;
    }

    public void IdlePos()
    {

    }

    public void AttackPos()
    {
        if (_movement.AttackState == 1)
        {
            _weapon.GetComponent<Rigidbody>().AddForce(transform.parent.right * _attackForce);
        }
        else if (_movement.AttackState == 2)
        {
            _weapon.GetComponent<Rigidbody>().AddForce(transform.parent.right * _attackForce / 2 + Vector3.up * _verticalAttackForce);
            _weapon.GetComponent<Rigidbody>().AddTorque(transform.parent.right * _verticalAttackForce, ForceMode.Force);

            transform.Rotate(0f, 0f, 20f);
            transform.position += Vector3.up * .5f;
        }
        else if (_movement.AttackState == 0)
        {
            _weapon.GetComponent<Rigidbody>().AddForce(transform.parent.right * _attackForce / 2 - Vector3.up * _verticalAttackForce);
            _weapon.GetComponent<Rigidbody>().AddTorque(transform.parent.right * -_verticalAttackForce, ForceMode.Force);

            transform.Rotate(0f, 0f, -20f);
            transform.position += Vector3.up * -.5f;
        }
    }

    private void Update()
    {
        // Smooth Rotation
        Vector3 directionToTarget = _target.position - transform.position;
        if (directionToTarget != Vector3.zero) // Ensure the direction is not zero
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        // Smooth Movement
        float step = _movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
    }
}
