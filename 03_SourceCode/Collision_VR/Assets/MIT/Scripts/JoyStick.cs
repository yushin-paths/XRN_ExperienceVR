using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	private RectTransform lever;
	private RectTransform rectTransform;

	[SerializeField, Range(10, 200)]
	private float leverRange;

	private Vector2 inputDirection;
	private bool isInput;

	[SerializeField]
	private PlayerController playerController;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();

		playerController = FindObjectOfType<PlayerController>();
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	gameObject.SetActive(true);
		//}
		//else
		//{
		//	gameObject.SetActive(false);
		//}
	}

	void Update()
	{
		if (isInput)
		{
			InputControlVector();
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		ControlJoyStickLever(eventData);
		isInput = true;
	}

	public void OnDrag(PointerEventData eventData)
	{
		ControlJoyStickLever(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		lever.anchoredPosition = Vector2.zero;
		isInput = false;
	}

	private void ControlJoyStickLever(PointerEventData eventData)
	{
		var inputPos = eventData.position - rectTransform.anchoredPosition;
		var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
		lever.anchoredPosition = inputVector;
		inputDirection = inputVector / leverRange;
	}

	private void InputControlVector()
	{
		playerController.Move(inputDirection);
	}
}
