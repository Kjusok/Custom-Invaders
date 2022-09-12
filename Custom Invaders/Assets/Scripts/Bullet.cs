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
        var enemyBullet = other.GetComponent<EnemyBullet>();
        var player = other.GetComponent<Player>();
        var item = other.GetComponent<Item>();

        if (enemy)
        {
            enemy.Kill();
        }

        if (!enemyBullet && !player && !item)
        {
            Destroy(gameObject);
            GameManager.Instance._bulletOnBoard = false;
        }
    }
}
