using LOK1game.Tools.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkPlayer : MonoBehaviour
{

    private void FixedUpdate()
    {
        ClientSend.PlayerMovement(GetInput(), Client.Instance.Tick);
    }

    private bool[] GetInput()
    {
        bool[] inputs = new bool[4]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.D)
        };

        return inputs;
    }

    private Vector2 GetInputDirection(bool[] inputs)
    {
        var inputDir = Vector2.zero;

        if (inputs[0])
        {
            inputDir.y += 1;

        }
        if (inputs[1])
        {
            inputDir.x -= 1;
        }
        if (inputs[2])
        {
            inputDir.y -= 1;
        }
        if (inputs[3])
        {
            inputDir.x += 1;
        }

        return inputDir;
    }
}
