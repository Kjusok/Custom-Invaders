using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridForEnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private RectTransform _boardSpawn;

    private float _padding = 0.5f;
    private float _posX;
    private float _posY;
    private void Start()
    {
        for (int row = 0; row < _boardSpawn.rect.height; row++)
        {
            _posY = -row - _padding;

            for (int line = 0; line < _boardSpawn.rect.width; line++)
            {
                _posX = line + _padding;

                if (Random.Range(0, 101) > 50)
                {
                    var enemy = Instantiate(_enemy, new Vector2(_posX, _posY), Quaternion.identity);
                    enemy.transform.SetParent(_boardSpawn.transform, false);
                }
            }
        }
    }
}
