using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] List<GameObject> animals = new List<GameObject>();

    private void Start()
    {
        Transform place = GetComponent<Transform>();
        Instantiate(animals[Random.Range(0, animals.Count)], Vector2.zero, Quaternion.identity).transform.SetParent(place, false); ;
    }
}
