using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _rightWall;
    [SerializeField] private float _leftWall;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private RectTransform _gameBoard;

    private Vector2 _positionOfSpawnedBullet;
    private Vector2 _playerPosition;
    private void Start()
    {
        _playerPosition = transform.position;
    }
    private void FireToGuns()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _positionOfSpawnedBullet = gameObject.transform.position;
            Instantiate(_bullet, new Vector2(_positionOfSpawnedBullet.x, _positionOfSpawnedBullet.y + 0.65f), Quaternion.Euler(0, 0, 90), _gameBoard.transform);
        }
    }
    private void MoveOfPlayer()
    {
        _playerPosition.x += Input.GetAxis("Horizontal") * _playerSpeed;
        transform.position = _playerPosition;
        if (_playerPosition.x < _leftWall)
        {
            transform.position = new Vector2(_leftWall, _playerPosition.y);
            _playerPosition.x = _leftWall;
        }
        if (_playerPosition.x > _rightWall)
        {
            transform.position = new Vector3(_rightWall, _playerPosition.y);
            _playerPosition.x = _rightWall;
        }
    }
    void Update()
    {
        FireToGuns();
        MoveOfPlayer();
    }
}
