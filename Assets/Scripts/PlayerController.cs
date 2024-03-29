using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;

    private void OnMove(InputValue inputValue)
    {
        _movement.Move(inputValue.Get<Vector2>());
    }

    private void OnJump()
    {
        Debug.Log("Jump");
    }
}
