using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float _positionForLoseItem = -5.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(transform.position.y < _positionForLoseItem)
        {
            Destroy(gameObject);
        }
    }
}
