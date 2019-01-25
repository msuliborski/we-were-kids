using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonControll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

[SerializeField] internal Sprite _hover;
[SerializeField] internal Sprite _noHover;
	private Image image;

	// Use this for initialization
	void Start ()
	{
		image = gameObject.GetComponent<Image>();
	}

	public void OnPointerEnter(PointerEventData data)
	{
		image.sprite = _hover;
	}

	public void OnPointerExit(PointerEventData data)
	{
		image.sprite = _noHover;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
