using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private bool _isTripelShot;
    [SerializeField] private bool _isSlowDownForPlayer;
    [SerializeField] private bool _isDestroyLineEnemys;

    private float _positionForLoseItem = -5.5f;
    private float _timeForItem = 10f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            if (_isSlowDownForPlayer)
            {
                ItemManager.Instance._timerForSlowDownPlayer += _timeForItem;
            }
            if (_isTripelShot)
            {
                ItemManager.Instance._timerForTripelshot += _timeForItem;
            }
            if (_isDestroyLineEnemys)
            {
                GameManager.Instance.KillEnemysLowerLine();
            }
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(transform.position.y < _positionForLoseItem || GameManager.Instance._timerForStarLevel > 0)
        {
            Destroy(gameObject);
        }
    }
}
