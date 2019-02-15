using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<Food>()||collision.transform.GetComponentInParent<Food>())
        {
            Destroy(collision.gameObject);
        }
    }
}
