using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDropable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image sugarObj;
    [SerializeField] private Transform place;
    private Image obj;

    public void OnBeginDrag(PointerEventData eventData)
    {
        obj = Instantiate(sugarObj, place);
        obj.transform.SetParent(place, false);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        place as RectTransform,         // �������������� � ������������ place
        eventData.position,              // ������� ����
        eventData.pressEventCamera,      // ������, ���� Canvas � ������ Screen Space - Camera
        out Vector2 localPoint           // �������� ��������� �������
        );

        obj.rectTransform.anchoredPosition = localPoint;
        Debug.Log("q");
    }

    public void OnDrag(PointerEventData eventData)
    {
        obj.rectTransform.anchoredPosition += eventData.delta;
        Debug.Log("qq");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("qqq");
    }
}
