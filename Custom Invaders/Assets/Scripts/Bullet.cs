using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _ridgidBodyOfBullet;
    [SerializeField] private int _speedOfBullet;


    private void Start()
    {
        _ridgidBodyOfBullet.AddForce(new Vector2(0, _speedOfBullet));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.Kill();
        }

        Destroy(gameObject);
    }
}
