using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _delayForStep;
    [SerializeField] private RectTransform _enemyRect;

    private float _step = 0.1f;
    private void Start()
    {
        StartCoroutine(MovingEnemiesDown());
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("collision");
        if (collider.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator MovingEnemiesDown()
    {
        while (true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - _step);
            yield return new WaitForSeconds(_delayForStep);
        }
    }
}
