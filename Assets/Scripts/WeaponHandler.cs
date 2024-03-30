using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private float _attackForce = 10000f;

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
        _weapon.GetComponent<Rigidbody>().AddForce(transform.parent.right * _attackForce);
    }
}
