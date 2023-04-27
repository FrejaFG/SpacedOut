using System;
using UnityEngine;

[Serializable]
public class PlayerOptions
{

    [SerializeField] public KeyCode MoveForward = KeyCode.W;
    [SerializeField] public KeyCode MoveBack = KeyCode.S;
    [SerializeField] public KeyCode MoveLeft = KeyCode.A;
    [SerializeField] public KeyCode MoveRight = KeyCode.D;

}
