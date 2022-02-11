using com.LOK1game.recode;
using LOK1game.Tools;
using LOK1game.Tools.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetworkPlayer : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    [SerializeField] private PlayerMoveData _movementData;

    private NetworkPlayerState[] _playerStateBuffer = new NetworkPlayerState[1024];

    private NetworkPlayerManager _networkPlayerData;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _networkPlayerData = GetComponent<NetworkPlayerManager>();

        for (int i = 0; i < _playerStateBuffer.Length; i++)
        {
            _playerStateBuffer[i] = new NetworkPlayerState();
        }
    }

    private void FixedUpdate()
    {
        var inputs = GetInput();

        ClientSend.PlayerMovement(inputs, Client.Instance.Tick);

        var bufferSlot = Client.Instance.Tick % 1024;

        _playerStateBuffer[bufferSlot].Inputs = inputs;
        _playerStateBuffer[bufferSlot].Position = transform.position;
        _playerStateBuffer[bufferSlot].Velocity = Rigidbody.velocity;

        var direction = GetDirection(GetInputDirection(inputs));
        var velocity = MoveGround(new CharacterMath.MoveParams(direction, Rigidbody.velocity));

        Rigidbody.velocity = velocity;

        Physics.Simulate(Time.fixedDeltaTime);

        HandleRewind();
    }

    private void HandleRewind()
    {
        var bufferSlot = _networkPlayerData.LastServerTick % 1024;
        var positionError = _networkPlayerData.ServerPosition - _playerStateBuffer[bufferSlot].Position;

        if(positionError.sqrMagnitude > 0.0000001f)
        {
            Rigidbody.position = _networkPlayerData.ServerPosition;
            Rigidbody.velocity = _networkPlayerData.ServerVelocity;

            var rewindTickNumber = _networkPlayerData.LastServerTick;

            while(rewindTickNumber < Client.Instance.Tick)
            {
                bufferSlot = rewindTickNumber % 1024;

                _playerStateBuffer[bufferSlot].Inputs = GetInput();
                _playerStateBuffer[bufferSlot].Position = Rigidbody.position;

                var direction = GetDirection(GetInputDirection(GetInput()));
                var velocity = MoveGround(new CharacterMath.MoveParams(direction, Rigidbody.velocity));

                Rigidbody.velocity = velocity;

                Physics.Simulate(Time.fixedDeltaTime);

                ++rewindTickNumber;
            }
        }
    }

    private bool[] GetInput()
    {
        bool[] inputs = new bool[5]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space),
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

    #region movement

    public Vector3 MoveGround(CharacterMath.MoveParams moveParams)
    {
        float t_speed = moveParams.previousVelocity.magnitude;

        if (t_speed != 0)
        {
            float drop = t_speed * _movementData.friction * Time.fixedDeltaTime;
            moveParams.previousVelocity *= Mathf.Max(t_speed - drop, 0) / t_speed;
        }

        return AccelerateVelocity(_movementData.walkGroundAccelerate, _movementData.walkGroundMaxVelocity, moveParams);
    }

    public Vector3 MoveAir(CharacterMath.MoveParams moveParams)
    {
        return AccelerateVelocity(_movementData.airAccelerate, _movementData.airMaxVelocity, moveParams);
    }

    private Vector3 AccelerateVelocity(float min, float max, CharacterMath.MoveParams moveParams)
    {
        return CharacterMath.Accelerate(moveParams, min, max, Time.fixedDeltaTime);
    }

    public Vector3 GetDirection(Vector2 input)
    {
        var direction = new Vector3(input.x, 0f, input.y);

        direction = transform.TransformDirection(direction);

        return direction.normalized;
    }

    #endregion
}
