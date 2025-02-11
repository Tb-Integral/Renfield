using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDropable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image sugarObj;
    [SerializeField] private Transform place;
    [SerializeField] private RectTransform dropPlace;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private int quantity;
    private Image obj;

    private void Awake()
    {
        quantityText.text = quantity.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            obj = Instantiate(sugarObj, place);
            obj.transform.SetParent(place, false);

            obj.rectTransform.anchoredPosition = Vector2.zero;
            obj.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            place as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
            );

            obj.rectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(dropPlace, eventData.position, eventData.pressEventCamera))
            {
                Destroy(obj.gameObject);
            }
            else
            {
                quantity--;
                quantityText.text = quantity.ToString();
            }
        }
    }
}
