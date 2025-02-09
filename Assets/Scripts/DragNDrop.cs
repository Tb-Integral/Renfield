using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public RectTransform dropZone;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Проверяем, есть ли CanvasGroup, если нет — добавляем
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false; // Отключаем, чтобы не блокировал Drop
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dropZone, eventData.position, eventData.pressEventCamera, out localPoint);
        rectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Включаем обратно

        if (!RectTransformUtility.RectangleContainsScreenPoint(dropZone, eventData.position, eventData.pressEventCamera))
        {
            Destroy(gameObject); // Удаляем объект, если он не в зоне
        }
    }
}