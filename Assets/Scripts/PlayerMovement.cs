using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed = 100f;
    [SerializeField] private float _jumpSpeed = 100f;
    [SerializeField] private float _downForce = 30f;
    private Vector2 moveDirection = Vector2.zero;

    public void Move(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (!IsGrounded())
        {
            _rigidbody.AddForce(Vector3.down * _downForce);
        }
        else
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, 0);
        }

        float moveSpeed = IsGrounded() ? _moveSpeed : _moveSpeed / 2f;
        _rigidbody.AddForce(transform.right * moveDirection.x * moveSpeed);
    }

    private bool IsGrounded()
    {
        float distance = .1f;
        return Physics.Raycast(transform.position + Vector3.up * distance / 2f, -Vector3.up, distance);
    }
}
