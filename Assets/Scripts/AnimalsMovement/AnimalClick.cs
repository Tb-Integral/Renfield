using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalClick : MonoBehaviour
{
    private Resourses _resourses;
    private void Start()
    {
        _resourses = GameObject.Find("ResourseManager").GetComponent<Resourses>();
    }
    private void OnMouseDown()
    {
        _resourses.fliesCount++;
        Destroy(gameObject);
    }
}
