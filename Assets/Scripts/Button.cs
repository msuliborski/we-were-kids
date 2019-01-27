using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Sprite hover;
    [SerializeField] private Sprite nonhover;
    private Image button;

    void Start()
    {
        button = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.sprite = hover;
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.sprite = nonhover;
        Debug.Log("Mouse Exit");
    }
}
