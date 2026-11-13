
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementByCC : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        // WASD 이동
        Vector3 move = Vector3.zero;

        if (keyboard.wKey.isPressed)
        {
            move += transform.forward;
        }

        if (keyboard.sKey.isPressed)
        {
            move -= transform.forward;
        }

        if (keyboard.aKey.isPressed)
        {
            move -= transform.right;
        }

        if (keyboard.dKey.isPressed)
        {
            move += transform.right;
        }

        // 대각선 이동 속도 보정
        move = move.normalized * moveSpeed;




        //--------------------------------------------------
        // 트릭(실제 물리를 정밀하게 적용하는 대신 간소화 하여 게임다운 느낌을 내는 방법)
        //--------------------------------------------------
        // 바닥 판정
        bool isGrounded = controller.isGrounded;

        // 바닥에 붙어있게 하는 트릭
        if (isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        // 점프 초기 속도 계산 공식(원하는 높이까지 올라가게 하기 위함)
        if (isGrounded && keyboard.spaceKey.wasPressedThisFrame)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //--------------------------------------------------




        // 중력
        verticalVelocity += gravity * Time.deltaTime;

        // 수직 이동 적용
        move.y = verticalVelocity;

        // 최종 이동
        controller.Move(move * Time.deltaTime);
    }
}
