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

    [SerializeField] private List<GameObject> animals = new List<GameObject>();
    [SerializeField] private Transform AnimalFolder;
    [SerializeField] private float speed;
    [SerializeField] private float time;
    [SerializeField] private float xAnimal;
    [SerializeField] private float yAnimal;

    private Image obj;
    private float xPoint;
    private float yPoint;
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
                AnimalMovement();
                quantity--;
                quantityText.text = quantity.ToString();
            }
        }
    }

    private void AnimalMovement()
    {
        xPoint = Random.Range(-xAnimal, xAnimal);
        if (xPoint > 10f || xPoint < -10f)
        {
            yPoint = Random.Range(0, yAnimal);
        }
        else yPoint = yAnimal;
        Instantiate(animals[Random.Range(0, animals.Count)], new Vector2 (xPoint, yPoint), Quaternion.identity).transform.SetParent(AnimalFolder, false); ;
    }
}
