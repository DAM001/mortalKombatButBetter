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
    private bool _isRightFacing = true;
    [HideInInspector] public bool IsRightFacing
    {
        get 
        {
            return _isRightFacing;
        } 
        set 
        {
            if (_isRightFacing == value) return;

            _isRightFacing = !_isRightFacing;
            transform.Rotate(0, 180, 0);
        } 
    }


    public void Move(Vector2 direction)
    {
        if (direction.x > .5)
        {
            IsRightFacing = true;
        }
        else if (direction.x < -.5)
        {
            IsRightFacing = false;
        }


        moveDirection = direction * (IsRightFacing ? 1 : -1);
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
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, .1f, _rigidbody.velocity.z);
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
