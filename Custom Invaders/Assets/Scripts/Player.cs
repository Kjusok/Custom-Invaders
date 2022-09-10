using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireDelay;

    private Vector2 _positionOfSpawnedBullet;
    private float _adjustment = 0.45f;
    private float _paddingForSpawnBullet = 0.65f;
    private float _xMin;
    private float _xMax;


    private void Start()
    {
        _xMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x + _adjustment;
        _xMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x - _adjustment;
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.Space) && GameManager.Instance._bulletOnBoard == false)
        {
            _positionOfSpawnedBullet = gameObject.transform.position;
            Instantiate(_bullet,
                new Vector2(_positionOfSpawnedBullet.x, _positionOfSpawnedBullet.y + _paddingForSpawnBullet),
                Quaternion.Euler(0, 0, 90));

            GameManager.Instance._bulletOnBoard = true;
        }
    }


    private void MoveOfPlayer()
    {
       transform.Translate(new Vector2((-Input.GetAxis("Horizontal")), 0.0f) * _playerSpeed * Time.fixedDeltaTime);

        var limitForMovementPosX = Mathf.Clamp(transform.position.x, _xMin, _xMax);
        transform.position = new Vector2(limitForMovementPosX, transform.position.y);
    }

    private void FixedUpdate()
    {
        MoveOfPlayer();
    }

    private void Update()
    {
        Fire();
    }
}
