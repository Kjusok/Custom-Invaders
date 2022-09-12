using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;
    [SerializeField] private RectTransform _fieldForSpawnItem;

    private Vector2 _positionOfSpawnedItem;
    private float _padding = 0.5f;
    

 
    public void SpawnItem()
    {
        var item = Instantiate(_items[Random.Range(0, 3)],
             new Vector2(Random.Range(_padding, _fieldForSpawnItem.rect.width - _padding), _positionOfSpawnedItem.y),
              Quaternion.identity);
        item.transform.SetParent(_fieldForSpawnItem.transform, false);
    }
}
