using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private WeaponHandler _weaponHandler;

    private void OnMove(InputValue inputValue)
    {
        _movement.Move(inputValue.Get<Vector2>());
    }

    private void OnJump()
    {
        _movement.Jump();
    }

    private void OnAttackDown()
    {
        _weaponHandler.AttackPos();
    }

    private void OnAttackUp()
    {
        _weaponHandler.IdlePos();
    }
}
