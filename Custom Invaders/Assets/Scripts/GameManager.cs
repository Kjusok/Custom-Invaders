using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
public enum EnemyMovement
{
    Horizontal,
    Down
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
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

    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _nextLevelPanel;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private RectTransform _boardSpawn;
    [SerializeField] private GameObject[] _healthPrefab;
    [SerializeField] private ItemManager _itemManager;
    [SerializeField] private List<Enemy> _enemyList;

    private float _padding = 0.5f;
    private float _posX;
    private float _posY;
    private int _counterForEnemy;
    private int _healthOfPlayer = 3;
    private float _speedForEnemySteps = 0.05f;

    public float _delayForStepEnemy = 0.4f;
    public bool _bulletOnBoard;
    public float _stepForEnemyHorizontal;
    public EnemyMovement EnemyMovement;


    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1f;
        _stepForEnemyHorizontal = 0.3f;
        EnemyMovement = EnemyMovement.Horizontal;

        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        for (int i = 0; i < _boardSpawn.rect.height; i++)
        {
            _posY = -i - _padding;

            for (int j = 0; j < _boardSpawn.rect.width; j++)
            {
                _posX = j + _padding;

                if (Random.Range(0, 2) == 0)
                {
                    var enemy = Instantiate(_enemyPrefab, new Vector2(_posX, _posY), Quaternion.identity);
                    enemy.transform.SetParent(_boardSpawn.transform, false);

                    enemy.GetComponent<EnemyBullet>();

                    _counterForEnemy++;

                    _enemyList.Add(enemy);
                }
            }
        }
    }

    private void Update()
    {
        if (_counterForEnemy == 0)
        {
            StartNextLevel();
        }
    }

    private void StartNextLevel()
    {
        _nextLevelPanel.SetActive(true);
    }
    public void PressNextLevelButton()
    {
        _delayForStepEnemy -= _speedForEnemySteps;
        _nextLevelPanel.SetActive(false);

        SpawnEnemy();
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void KillEnemysLowerLine()
    {
        for (int i = 0; i < _enemyList.Count;)
        {
            if (_enemyList[i] == null)
            {
                _enemyList.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        List<float> positionY = new List<float>();

        foreach (Enemy enemy in _enemyList)
        {
            positionY.Add(enemy.transform.position.y);
        }

        float minY = positionY.Min();

        foreach (Enemy enemy in _enemyList)
        {
            if (minY == enemy.transform.position.y)
            {
                enemy.Kill();
            }
        }

    }

    public void PlayerTakeDamage()
    {
        _healthOfPlayer -= 1;

        if (_healthOfPlayer >= 0)
        {
            Destroy(_healthPrefab[_healthOfPlayer]);
        }
        if (_healthOfPlayer == 0)
        {
            GameOver();
        }
    }

    public void ChangeMovementEnemy()
    {
        _stepForEnemyHorizontal *= -1.0f;
        EnemyMovement = EnemyMovement.Down;
    }

    public void KillEnemy()
    {
        _counterForEnemy--;

        if (_counterForEnemy % 5 == 0)
        {
            _itemManager.SpawnItem();
        }
    }

    public void PressButtonStartAgain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GameOver()
    {
        _menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
