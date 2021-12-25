using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDirectionController : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnDirectionChanged)) ]
    int playerDirection;

    [SerializeField] private GameObject playerBody;

    public override void OnStartAuthority()
    {
        enabled = true;
    }

    [ClientCallback]
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (mousePos.x - playerBody.transform.position.x >= 0)
            CmdChangeDirection(1);
        else
            CmdChangeDirection(-1);
    }

    [Command]
    private void CmdChangeDirection(int dir)
    {
        playerDirection = dir;
    }

    private void OnDirectionChanged(int oldV, int newV)
    {
        playerBody.transform.localScale = new Vector3(newV, 1, 1);
    } 
}
