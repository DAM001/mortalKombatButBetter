using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsRightFacing { get; set; }

    private void Start()
    {
        IsRightFacing = true;
    }
}
