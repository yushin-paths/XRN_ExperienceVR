using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
	[Header("Translate"), SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float rotateSpeed;
	float yRotation;
	float xRotation;

	[Header("Jump")]
	[SerializeField]
	private UnityEngine.UI.Button jump_button;
	[SerializeField]
	private Rigidbody body;
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private float jumpDelay;
	private bool isJump;
	private bool isGround;

	[Header("Rotate"), SerializeField]
	private Camera playerCam;
	private Vector3 camLocalPos;
	bool alt;

	private int rightFingerId;
	float halfScreenWidth;  //화면 절반만 터치하면 카메라 회전
	private Vector2 prevPoint;

	public Transform cameraTransform;
	public float cameraSensitivity;

	private Vector2 lookInput;
	private float cameraPitch; //pitch 시점

	private void Awake()
    {
		camLocalPos = playerCam.transform.localPosition;

		halfScreenWidth = Screen.width * 0.5f;

		jump_button.onClick.AddListener(delegate { Jump(); });
	}

    void Update()
    {
		Move(OutPutGetAxis());
		Rotate();

		if (Input.GetKey(KeyCode.Space))
		{
			Jump();
		}
	}

	private Vector2 OutPutGetAxis()
	{
		Vector2 result = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		return result;
	}

	public void Move(Vector2 inputDirection)
	{
		// 이동 방향키 이동 값 가져오기
		Vector2 moveInput = inputDirection;

		bool isMove = moveInput.magnitude != 0;

		if (isMove)
		{
			//Vector3 looFoward = new Vector3(playerCam.transform.forward.x, 0, playerCam.transform.forward.z).normalized;

			//Vector3 lookRight = new Vector3(playerCam.transform.right.x, 0, playerCam.transform.right.z).normalized;

			//Vector3 moveDir = looFoward * moveInput.y + lookRight * moveInput.x;

			//transform.forward = moveDir;

			//transform.position += moveDir * Time.deltaTime * moveSpeed;

			transform.Translate(inputDirection.x * Time.deltaTime * moveSpeed, 0, inputDirection.y * Time.deltaTime * moveSpeed);
			playerCam.transform.localPosition = camLocalPos;
		}
	}

	public void Jump()
	{
		if (!isJump && isGround)
			StartCoroutine(JumpDelayed());
	}

	private IEnumerator JumpDelayed()
	{
		isJump = true;
		body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		yield return new WaitForSeconds(jumpDelay);
		isJump = false;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
			isGround = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
			isGround = false;
	}

	public void Rotate()
	{
		// 모바일 터치 화면 회전
		GetTouchInput();

		//Vector2 rotateInput = inputDirection;

		float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

		yRotation += mouseX;
		xRotation -= mouseY;

		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		// PC 마우스 화면 회전
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			alt = !alt;
		}
		if (!alt)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			transform.rotation = Quaternion.Euler(0, yRotation, 0);
			playerCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		}
	}

	private void GetTouchInput()
	{
		//몇개의 터치가 입력되는가
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch t = Input.GetTouch(i);

			switch (t.phase)
			{
				case TouchPhase.Began:

					if (t.position.x > halfScreenWidth && rightFingerId == -1)
					{
						rightFingerId = t.fingerId;
						Debug.Log("오른쪽 손가락 입력");
					}
					break;

				case TouchPhase.Moved:

					//이것을 추가하면 시점 원상태 버튼을 누를 때 화면이 돌아가지 않는다
					if (!EventSystem.current.IsPointerOverGameObject(i))
					{
						if (t.fingerId == rightFingerId)
						{

							//수평
							prevPoint = t.position - t.deltaPosition;
							transform.RotateAround(transform.position, Vector3.up, -(t.position.x - prevPoint.x) * 0.2f);
							prevPoint = t.position;


							//수직
							lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
							cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
							cameraTransform.localRotation = Quaternion.Euler(-cameraPitch, 0, 0);
						}
					}
					break;

				case TouchPhase.Stationary:

					if (t.fingerId == rightFingerId)
					{
						lookInput = Vector2.zero;

					}
					break;

				case TouchPhase.Ended:

					if (t.fingerId == rightFingerId)
					{
						rightFingerId = -1;
						Debug.Log("오른쪽 손가락 끝");

					}
					break;

				case TouchPhase.Canceled:

					if (t.fingerId == rightFingerId)
					{
						rightFingerId = -1;
						Debug.Log("오른쪽 손가락 끝");

					}
					break;
			}
		}
	}
}
