using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    [SerializeField]
    GameObject playerDataObject;
    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    PlayerOptions playerOptions;

    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }
    private void Start()
    {
        playerDataObject = GameObject.Find("PlayerDataObject");
        playerData = playerDataObject.GetComponent<PlayerData>();
        playerOptions = playerData.GetOptions();
    }
    private void Update()
    {
        GetMovementInput();
    }

    private void GetMovementInput()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;
        if (Input.anyKey)
        {
            if (Input.GetKey(playerOptions.MoveLeft))
            {
                horizontalInput -= 1f;
            }

            if (Input.GetKey(playerOptions.MoveRight))
            {
                horizontalInput += 1f;
            }

            if (Input.GetKey(playerOptions.MoveForward))
            {
                verticalInput += 1f;
            }

            if (Input.GetKey(playerOptions.MoveBack))
            {
                verticalInput -= 1f;
            }
        }
        OnMovementKeyPressed?.Invoke(new Vector2(horizontalInput, verticalInput));
    }
}
