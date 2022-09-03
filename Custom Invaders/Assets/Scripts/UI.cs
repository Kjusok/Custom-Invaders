using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void PressButtonStartAgain()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void CheckEnemyPosition(bool _enemyReachedTarget)
    {
        if (_enemyReachedTarget == false)
        {
            StopGame();
        }
    }
    private void StopGame()
    {
        _menu.SetActive(true);
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
           StopGame();
        }
    }
}
