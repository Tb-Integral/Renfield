using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
