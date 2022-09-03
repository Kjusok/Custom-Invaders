using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _ridgidBodyOfBullet;
    [SerializeField] private int _speedOfBullet;
    private void Start()
    {
        _ridgidBodyOfBullet.AddForce(new Vector2(0, _speedOfBullet));
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }
}
