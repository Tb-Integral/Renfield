using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class DragNDropable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image sugarObj;
    [SerializeField] private Transform place;
    [SerializeField] private RectTransform dropPlace;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private int quantity;

    [SerializeField] private List<GameObject> animals = new List<GameObject>();
    [SerializeField] private Transform AnimalFolder;
    [SerializeField] private float speedMax;
    [SerializeField] private float speedMin = 0f;
    [SerializeField] private float timeMax;
    [SerializeField] private float timeMin = 0f;
    [SerializeField] private float xAnimal;
    [SerializeField] private float yAnimal;

    private Image obj;
    private float xPoint;
    private float yPoint;
    private float time;
    private float speed;

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
        if (xPoint > 9f || xPoint < -9f)
        {
            yPoint = Random.Range(0, yAnimal);
        }
        else yPoint = yAnimal;
        GameObject animal = Instantiate(animals[Random.Range(0, animals.Count)], new Vector2 (xPoint, yPoint), Quaternion.identity);
        animal.transform.SetParent(AnimalFolder, false);

        time = Random.Range(timeMin, timeMax);
        speed = Random.Range(speedMin, speedMax);

        StartCoroutine( animalToFood(time, speed, xPoint, yPoint, animal, obj));
    }

    private IEnumerator animalToFood(float timeWait, float speed, float x, float y, GameObject animal, Image food)
    {
        float xFood = food.rectTransform.position.x;
        float yFood = food.rectTransform.position.y;
        float acceleration = 0.5f;
        float currentSpeed = 0f;

        float timer = 0f;
        while (timer < timeWait)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (xFood < animal.transform.position.x)
        {
            animal.GetComponent<SpriteRenderer>().flipX = true;
        }

        float step;
        while (Mathf.Abs(xFood - animal.transform.position.x) > 0.5f ||
           Mathf.Abs(yFood - animal.transform.position.y) > 0.5f)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speed, acceleration * Time.deltaTime);
                animal.transform.position = Vector2.Lerp(
                    animal.transform.position,
                    new Vector2(xFood, yFood),
                    currentSpeed * Time.deltaTime
            );
            yield return null;
        }

        Debug.Log("X food: " + xFood);
        Debug.Log("Y food: " + yFood);
        Debug.Log("X : " + x);
        Debug.Log("Y : " + y);

        Debug.Log("end of while");

        timeWait = Random.Range (timeMin, timeMax);
        timer = 0f;
        while (timer < timeWait)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(food.gameObject); // ׃האכÿול מבתוךע


        speed = Random.Range(speedMin, speedMax);

        xFood = Random.Range(-xAnimal, xAnimal);
        if (xFood > 9f || xFood < -9f)
        {
            yFood = Random.Range(0, yAnimal);
        }
        else yFood = yAnimal;


        animal.GetComponent<SpriteRenderer>().flipX = (xFood < animal.transform.position.x) ? true : false;


        while (Mathf.Abs(xFood - animal.transform.position.x) > 0.5f ||
           Mathf.Abs(yFood - animal.transform.position.y) > 0.5f)
        {
                currentSpeed = Mathf.Lerp(currentSpeed, speed, acceleration * Time.deltaTime);

                animal.transform.position = Vector2.Lerp(
                    animal.transform.position,
                    new Vector2(xFood, yFood),
                    currentSpeed * Time.deltaTime
                );
            yield return null;
        }
    }
}
