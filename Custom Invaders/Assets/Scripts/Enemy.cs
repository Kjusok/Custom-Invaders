using System.Collections;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _delayForStep;
    [SerializeField] private float _step;

    private float _positionEnemyForLooseY = -4.5f;


    private void Start()
    {
        StartCoroutine(MovingEnemiesDown());
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
            transform.position = new Vector3(transform.position.x, transform.position.y - _step, 0);
            yield return new WaitForSeconds(_delayForStep);
        }

        GameManager.Instance.GameOver();
    }
}

