using UnityEngine;

public enum BulletMovement
{
    StraightUp,
    RightBullet,
    LeftBullet
}
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _ridgidBodyOfBullet;
    [SerializeField] private int _speedOfBullet;

    public BulletMovement BulletMovement;

    public void Start()
    {
        if(BulletMovement == BulletMovement.StraightUp)
        {
            _ridgidBodyOfBullet.AddForce(new Vector2(0, _speedOfBullet));
        }
        if (BulletMovement == BulletMovement.RightBullet || BulletMovement == BulletMovement.LeftBullet)
        {
            var angel = _ridgidBodyOfBullet.rotation * Mathf.Deg2Rad;

            _ridgidBodyOfBullet.AddForce(new Vector2(_speedOfBullet * Mathf.Cos(angel), _speedOfBullet * Mathf.Sin(angel)));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        var enemyBullet = other.GetComponent<EnemyBullet>();
        var player = other.GetComponent<Player>();
        var item = other.GetComponent<Item>();
        var bullet = other.GetComponent<Bullet>();

        if (enemy)
        {
            enemy.Kill();
        }

        if (!enemyBullet && !player && !item && !bullet)
        {
            Destroy(gameObject);

            if (BulletMovement == BulletMovement.StraightUp)
            {
                GameManager.Instance._bulletOnBoard = false;
            }
        }
    }
}
