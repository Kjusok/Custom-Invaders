using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireDelay;
    private Coroutine _fireFrequency;

    private Vector2 _positionOfSpawnedBullet;
    private float _timerForNextShot;
    private float _adjustment = 0.45f;
    private float _xMin;
    private float _xMax;
    private void Start()
    {
        MoveBorders();
    }
    private void MoveBorders()
    {
        Camera gameCamera = Camera.main;

        _xMin = gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).x + _adjustment;
        _xMax = gameCamera.ViewportToWorldPoint(new Vector2(1, 0)).x - _adjustment;
    }
    private IEnumerator FireDelay()
    {
        while (true)
        {
            _positionOfSpawnedBullet = gameObject.transform.position;
            Instantiate(_bullet, new Vector2(_positionOfSpawnedBullet.x, _positionOfSpawnedBullet.y + 0.65f), Quaternion.Euler(0, 0, 90));
            yield return new WaitForSeconds(_fireDelay);
        }
    }
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _timerForNextShot<=0)
        {
            _timerForNextShot += _fireDelay;
            _fireFrequency = StartCoroutine(FireDelay());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(_fireFrequency);
        }
    }
    private void MoveOfPlayer()
    { 
      var deltaX = Input.GetAxis("Horizontal") * _playerSpeed;
      var newPosX =  Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
        transform.position = new Vector2(newPosX, -4.4f);
    }
    void Update()
    {
        if (_timerForNextShot > 0)
        {
            _timerForNextShot -= Time.deltaTime;
        }
        Fire();
        MoveOfPlayer();
    }
}
