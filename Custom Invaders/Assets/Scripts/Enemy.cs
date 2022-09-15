using System.Collections;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBullet _enemyBullet;

    private int _layerMaskOnlyPlaer = 1 << 8;
    private float _paddingForSpawnBullet = 0.65f;
    private float _positionEnemyForLooseY = -4.5f;
    private float _stepForVertical = -0.5f;
    private Vector2 _positionOfSpawnedEnemyBullet;
    private Vector3 _directionForX = Vector2.right;
    private Vector3 _directionForY = Vector2.down;
    private Vector3 _position;
    private float _xMin;
    private float _xMax;
    private float _padding = 0.7f;
    private bool _enemyOnTheMove;


    private void Start()
    {
        _xMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x + _padding;
        _xMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x - _padding;
    }

    public void Kill()
    {
        GameManager.Instance.KillEnemy();
        
        Destroy(gameObject);
    }

    private IEnumerator MovingEnemiesDown()
    {
        while (transform.position.y > _positionEnemyForLooseY)
        {
            if (GameManager.Instance.EnemyMovement == EnemyMovement.Horizontal)
            {
                transform.position += _directionForX * GameManager.Instance._stepForEnemyHorizontal;
                _position = transform.position;
            }
            if (GameManager.Instance.EnemyMovement == EnemyMovement.Down)
            {
                transform.position -= _directionForY * _stepForVertical;
            }
            yield return new WaitForSeconds(GameManager.Instance._delayForStepEnemy);
        }

        GameManager.Instance.GameOver();
    }

    private void CheckPosition()
    {
        if ((transform.position.x >= _xMax && GameManager.Instance._stepForEnemyHorizontal > 0) ||
            (transform.position.x <= _xMin && GameManager.Instance._stepForEnemyHorizontal < 0))
        {
            GameManager.Instance.ChangeMovementEnemy();
        }
        else if (transform.position.y < _position.y)
        {
            GameManager.Instance.EnemyMovement = EnemyMovement.Horizontal;
        }
    }

    private void Update()
    {
        CheckPosition();

        if (GameManager.Instance._timerForStarLevel <= 0 && _enemyOnTheMove)
        {
            StartCoroutine(MovingEnemiesDown());
            _enemyOnTheMove = false;
        }
        if(GameManager.Instance._timerForStarLevel > 0)
        {
            _enemyOnTheMove = true;
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, _layerMaskOnlyPlaer))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (UnityEngine.Random.Range(0, 1001) < 1 && GameManager.Instance._timerForStarLevel <= 0)
        {
            _positionOfSpawnedEnemyBullet = gameObject.transform.position;

            Instantiate(_enemyBullet,
                new Vector2(_positionOfSpawnedEnemyBullet.x, _positionOfSpawnedEnemyBullet.y - _paddingForSpawnBullet),
                Quaternion.Euler(0, 0, 90));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            GameManager.Instance.PlayerTakeDamage();
            Kill();
        }
    }
}

