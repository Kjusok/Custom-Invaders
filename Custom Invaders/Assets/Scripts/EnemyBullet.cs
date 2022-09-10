using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _ridgidBodyOfEnemyBullet;
    [SerializeField] private int _speedOfEnemyBullet;


    private void Start()
    {
        _ridgidBodyOfEnemyBullet.AddForce(new Vector2(0, -_speedOfEnemyBullet));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        var bulletPlayer = other.GetComponent<Bullet>();

        if (!enemy && !bulletPlayer)
        {
            Destroy(gameObject);
        }
    }
}
