using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;

    public static ItemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Instance not specified");
            }
            return _instance;
        }
    }

    [SerializeField] private RectTransform _fieldForSpawnItem;

    private Vector2 _positionOfSpawnedItem;
    private float _padding = 0.5f;

    public Item[] _items;
    public float _timerForTripelshot;
    public float _timerForSlowDownPlayer;


    private void Awake()
    {
        _instance = this;
    }
    public void SpawnItem()
    {
        if(GameManager.Instance._timerForStarLevel <= 0)
        {
            var item = Instantiate(_items[Random.Range(0, 3)],
             new Vector2(Random.Range(_padding, _fieldForSpawnItem.rect.width - _padding), _positionOfSpawnedItem.y),
              Quaternion.identity);
            item.transform.SetParent(_fieldForSpawnItem.transform, false);
        }
    }

    private void Update()
    {
        if (_timerForTripelshot > 0)
        {
            _timerForTripelshot -= Time.deltaTime;
        }
        if (_timerForSlowDownPlayer > 0)
        {
            _timerForSlowDownPlayer -= Time.deltaTime;
        }
        if (GameManager.Instance._timerForStarLevel > 0)
        {
            _timerForSlowDownPlayer = 0;
            _timerForTripelshot = 0;
        }
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
