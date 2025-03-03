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
        yield return new WaitForSeconds(timeWait);

        if (animal == null)
        {
            SpawnNewAnimal(food);
            yield break;
        }

        float xFood = food.rectTransform.position.x;
        float yFood = food.rectTransform.position.y;
        float acceleration = 0.5f;
        float currentSpeed = 1f;

        if (xFood < animal.transform.position.x)
        {
            animal.GetComponent<SpriteRenderer>().flipX = true;
        }

        while (animal != null && Vector2.Distance(animal.transform.position, new Vector2(xFood, yFood)) > 0.5f)
        {
            if (animal == null)
            {
                SpawnNewAnimal(food);
                yield break;
            }

            currentSpeed = Mathf.Lerp(currentSpeed, speed, acceleration * Time.deltaTime);
            animal.transform.position = Vector2.MoveTowards(
                animal.transform.position,
                new Vector2(xFood, yFood),
                currentSpeed * Time.deltaTime
            );

            yield return null;
        }

        if (animal == null)
        {
            SpawnNewAnimal(food);
            yield break;
        }

        yield return new WaitForSeconds(Random.Range(timeMin, timeMax));

        if (animal)
        {
            Destroy(food.gameObject);


            speed = Random.Range(speedMin, speedMax);

            xFood = Random.Range(-xAnimal, xAnimal);
            if (xFood > 9f || xFood < -9f)
            {
                yFood = Random.Range(0, yAnimal);
            }
            else yFood = yAnimal;


            animal.GetComponent<SpriteRenderer>().flipX = (xFood < animal.transform.position.x) ? true : false;


            while (animal != null && Vector2.Distance(animal.transform.position, new Vector2(xFood, yFood)) > 0.5f)
            {
                if (animal == null)
                {
                    SpawnNewAnimal(food);
                    yield break;
                }

                currentSpeed = Mathf.Lerp(currentSpeed, speed, acceleration * Time.deltaTime);
                animal.transform.position = Vector2.MoveTowards(
                    animal.transform.position,
                    new Vector2(xFood, yFood),
                    currentSpeed * Time.deltaTime
                );

                yield return null;
            }
            Destroy(animal.gameObject);
        }
        else
        {
            xPoint = Random.Range(-xAnimal, xAnimal);
            if (xPoint > 9f || xPoint < -9f)
            {
                yPoint = Random.Range(0, yAnimal);
            }
            else yPoint = yAnimal;
            animal = Instantiate(animals[Random.Range(0, animals.Count)], new Vector2(xPoint, yPoint), Quaternion.identity);
            animal.transform.SetParent(AnimalFolder, false);

            time = Random.Range(timeMin, timeMax);
            speed = Random.Range(speedMin, speedMax);

            StartCoroutine(animalToFood(time, speed, xPoint, yPoint, animal, food));
        }
    }

    // Функция для создания нового животного
    private void SpawnNewAnimal(Image food)
    {
        float xPoint = Random.Range(-xAnimal, xAnimal);
        float yPoint = (xPoint > 9f || xPoint < -9f) ? Random.Range(0, yAnimal) : yAnimal;

        GameObject newAnimal = Instantiate(animals[Random.Range(0, animals.Count)], new Vector2(xPoint, yPoint), Quaternion.identity);
        newAnimal.transform.SetParent(AnimalFolder, false);

        float time = Random.Range(timeMin, timeMax);
        float speed = Random.Range(speedMin, speedMax);

        StartCoroutine(animalToFood(time, speed, xPoint, yPoint, newAnimal, food));
    }
}
