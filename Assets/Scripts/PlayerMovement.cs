using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed = 100f;

    public void Move(Vector2 direction)
    {
        _rigidbody.AddForce(transform.right * direction.x);
    }
}
