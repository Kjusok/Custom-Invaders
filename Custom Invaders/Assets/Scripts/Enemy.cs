using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private RectTransform _enemyRect;
    [SerializeField] private float _delayForStep;
    [SerializeField] private float _step;

    private bool _enemyReachedTarget;
    private void Start()
    {
        StartCoroutine(MovingEnemiesDown());
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
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
    private void Update()
    {
        CheckEnemyPosition();
    }
    private void CheckEnemyPosition()
    {
        if (transform.position.y < -4.5f)
        {
            _enemyReachedTarget = false;
           GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            canvas.gameObject.GetComponent<UI>().CheckEnemyPosition(_enemyReachedTarget);
        }
    }
}
