using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    public static GameManager instance
    {
        get => _instance;
        set { _instance = value; }
    }
    public int maxLives = 5;
    private int _lives = 3;

    public PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance = null;
    [HideInInspector] public Level currentLevel = null;
    [HideInInspector] public Transform currentSpawnPoint;

    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value)
                Respawn();

            if (value < 0)
                GameOver();

            _lives = value;

            if (_lives > maxLives)
                _lives = maxLives;

            if (_lives < 0)
                GameOver();

            Debug.Log("Lives have been set to: " + _lives.ToString());
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Level"|| SceneManager.GetActiveScene().name =="GameOver")
            {
                SceneManager.LoadScene(0);
                playerInstance = null;
            }
            else
                SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.K))
            lives--;

        

    }

    public void SpawnPlayer(Transform spawnPoint)
    {
        playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        currentSpawnPoint = spawnPoint;
    }

    void Respawn()
    {
        if (playerInstance)
            playerInstance.transform.position = currentSpawnPoint.position;
    }

    void GameOver()
    {
        
            SceneManager.LoadScene(2);
    }
}