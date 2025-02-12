using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] private  List<GameObject> animals = new List<GameObject>();
    [SerializeField] private float speed;
    [SerializeField] private GameObject sugarFolder;

    private void Update()
    {
        if (sugarFolder.transform.childCount > 0)
        {
            Transform place = GetComponent<Transform>();
            Instantiate(animals[Random.Range(0, animals.Count)], Vector2.zero, Quaternion.identity).transform.SetParent(place, false); ;
        }
    }
}
