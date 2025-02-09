using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconSpawner : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject sugar;
    [SerializeField] private RectTransform canvasT;
    [SerializeField] private RectTransform dropZone;
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject newObj = Instantiate(sugar, canvasT);
        RectTransform newObjRect = newObj.GetComponent<RectTransform>();

        if (newObj.GetComponent<CanvasGroup>() == null)
        {
            newObj.AddComponent<CanvasGroup>();
        }

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasT, Input.mousePosition, eventData.pressEventCamera, out localPoint);

        // Устанавливаем правильную позицию
        newObjRect.anchoredPosition = localPoint;
        var draggable = newObj.GetComponent<DragNDrop>();
        draggable.dropZone = dropZone;

        StartCoroutine(ForceBeginDrag(draggable));
    }

    private IEnumerator ForceBeginDrag(DragNDrop draggable)
    {
        yield return null; // Ждем один кадр, чтобы объект появился
        PointerEventData fakeEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        ExecuteEvents.Execute(draggable.gameObject, fakeEventData, ExecuteEvents.beginDragHandler);
    }
}
