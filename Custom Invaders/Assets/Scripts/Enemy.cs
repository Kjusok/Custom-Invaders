using System.Collections;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private RectTransform _enemyRect;
    [SerializeField] private float _delayForStep;
    [SerializeField] private float _step;

    private void Start()
    {
        StartCoroutine(MovingEnemiesDown());
    }
    public void Kill()
    {
        --GameManager.Instance._counterForEnemy;
        Destroy(gameObject);
    }
    private IEnumerator MovingEnemiesDown()
    {
        while (transform.position.y > -4.5f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - _step);
            yield return new WaitForSeconds(_delayForStep);
        }

        GameManager.Instance.StopGame();
    }
}

