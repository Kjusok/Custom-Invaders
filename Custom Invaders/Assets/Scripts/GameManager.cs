using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
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
    [SerializeField] private GameObject _secondToStartPanel;
    [SerializeField] private GameObject[] _healthPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private RectTransform _boardSpawn;
    [SerializeField] private List<Enemy> _enemyList;
    [SerializeField] private Text _currentLevelText;
    [SerializeField] private Text _timerForNetLevelText;

    private float _padding = 0.5f;
    private float _posX;
    private float _posY;
    private float _speedForEnemySteps = 0.05f;
    private float _stepForTimerForEnemyWhoWilShoot;
    private int _counterForEnemy;
    private int _currentLevel = 1;
    private int _healthOfPlayer = 3;
    private int _initialSecondsForTimerForEnemyShoot = 3;

    public bool _enemySelectedToShoot;
    public bool _bulletOnBoard;
    public float _timerForChooseEnemyWhoWillShoot = 5;
    public float _timerForStarLevel;
    public float _delayForStepEnemy = 0.4f;
    public float _stepForEnemyHorizontal;
    public EnemyMovement EnemyMovement;


    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1f;
        _timerForStarLevel = 5;
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

    private void TimerOnScreenBeforeStartLevel()
    {
        var secMax = 3;
        var secMin = 1;

        if (_timerForStarLevel > secMax)
        {
            _nextLevelPanel.SetActive(true);
            _currentLevelText.text = _currentLevel.ToString();
        }

        if (_timerForStarLevel > secMin && _timerForStarLevel < secMax)
        {
            _nextLevelPanel.SetActive(false);
            _secondToStartPanel.SetActive(true);
            _timerForNetLevelText.text = _timerForStarLevel.ToString("0");
        }

        if (_timerForStarLevel < secMin)
        {
            _timerForNetLevelText.text = "GO!";
        }

        if (_timerForStarLevel <= 0)
        {
            _secondToStartPanel.SetActive(false);
        }
    }

    private void Update()
    {
        ClearListEnemy();

        if (_counterForEnemy == 0)
        {
            StartNextLevel();
        }

        if (_timerForStarLevel > 0)
        {
            _timerForStarLevel -= Time.deltaTime;

            TimerOnScreenBeforeStartLevel();
        }

        if (_timerForChooseEnemyWhoWillShoot > 0)
        {
            _timerForChooseEnemyWhoWillShoot -= Time.deltaTime;
        }
        if (_timerForChooseEnemyWhoWillShoot <= 0 && _enemySelectedToShoot == false)
        {
            SelectEnemyWhoFireNext();
        }
        if (_timerForChooseEnemyWhoWillShoot > _initialSecondsForTimerForEnemyShoot)
        {
            _timerForChooseEnemyWhoWillShoot = _initialSecondsForTimerForEnemyShoot;
        }
    }

    private void StartNextLevel()
    {
        SpawnEnemy();
        SelectEnemyWhoFireNext();

        _timerForChooseEnemyWhoWillShoot = 5;
        _delayForStepEnemy -= _speedForEnemySteps;
        _currentLevel++;
        _stepForTimerForEnemyWhoWilShoot += 0.3f;
        _timerForStarLevel += 5;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void ClearListEnemy()
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
    }
    public void SelectEnemyWhoFireNext()
    {
        List<Enemy> enemyWhoFireNextList = new List<Enemy>();

        foreach (Enemy enemy in _enemyList)
        {
            enemyWhoFireNextList.Add(enemy);
        }

        int indexOfEnemyWhoFireNex = Random.Range(0, enemyWhoFireNextList.Count);

        var enemyWhoFire = enemyWhoFireNextList[indexOfEnemyWhoFireNex];

        foreach (Enemy enemy in _enemyList)
        {
            if (enemy == enemyWhoFire)
            {
                enemy.EnemyReadyToFire();
            }
        }

        _enemySelectedToShoot = true;
        _timerForChooseEnemyWhoWillShoot += _initialSecondsForTimerForEnemyShoot - _stepForTimerForEnemyWhoWilShoot;
    }

    public void KillEnemysLowerLine()
    {
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
            ItemManager.Instance.SpawnItem();
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
