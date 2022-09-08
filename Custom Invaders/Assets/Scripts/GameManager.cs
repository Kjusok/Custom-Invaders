using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private RectTransform _boardSpawn;

    private float _padding = 0.5f;
    private float _posX;
    private float _posY;
    private int _counterForEnemy;


    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1f;
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

                    _counterForEnemy++;
                }
            }
        }
    }

    private void Update()
    {
        if (_counterForEnemy == 0)
        {
            GameOver();
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void KillEnemy()
    {
        _counterForEnemy--;
    }

    public void PressButtonStartAgain()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log(_counterForEnemy);

    }
   
    public void GameOver()
    {
        _menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}

