using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireDelay;

    private Vector2 _positionOfSpawnedBullet;
    private float _timerForNextShot;
    private float _adjustment = 0.45f;
    private float _paddingForSpawnBullet = 0.65f;
    private float _xMin;
    private float _xMax;
    private bool _needFire;


    private void Start()
    {
        _xMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x + _adjustment;
        _xMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x - _adjustment;
    }

    private void Fire()
    {
        if (!_needFire)
        {
            return;
        }
        _timerForNextShot += _fireDelay;

        _positionOfSpawnedBullet = gameObject.transform.position;
        Instantiate(_bullet,
            new Vector2(_positionOfSpawnedBullet.x, _positionOfSpawnedBullet.y + _paddingForSpawnBullet),
            Quaternion.Euler(0, 0, 90));
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _needFire = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _needFire = false;
        }

        if (_timerForNextShot > 0)
        {
            _timerForNextShot -= Time.deltaTime;
        }
        else
        {
            Fire();
        }
    }
}
