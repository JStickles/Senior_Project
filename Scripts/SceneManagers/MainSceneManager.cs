using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public GameObject enemy;
    public int bossesKilled;
    public int EnemiesKilled;
    //int closestEnemy;

    public GameObject[] levelOneWaypoints;
    public GameObject[] enemies;
    public GameObject[] levelTwoWaypoints;
    public GameObject[] enemiesTwo;
    public GameObject[] levelThreeWaypoints;
    public GameObject[] enemiesThree;
    public GameObject[] levelFourWaypoints;
    public GameObject[] enemiesFour;
    public float[] distance;

    public GameObject bossOneEntry;
    public GameObject bossOneExit;
    public GameObject bossTwoEntry;
    public GameObject bossTwoExit;
    public GameObject bossThreeEntry;
    public GameObject bossThreeExit;
    public GameObject bossFourEntry;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_DEATH, GameOver);
        Messenger.AddListener(GameEvent.BOSS_KILL, BossKill);
        Messenger.AddListener(GameEvent.ENEMY_KILL, EnemyKill);
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new GameObject[levelOneWaypoints.Length / 2];
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < levelOneWaypoints.Length / 2; i++)
        {

            GameObject newEnemy = GameObject.Instantiate(enemy, levelOneWaypoints[2 * i].transform.position, new Quaternion(0, 0, 0, 0));
            newEnemy.GetComponent<AITypes>().waypoints = new GameObject[2];
            newEnemy.GetComponent<AITypes>().waypoints[0] = levelOneWaypoints[2 * i];
            newEnemy.GetComponent<AITypes>().waypoints[1] = levelOneWaypoints[2 * i + 1];
            newEnemy.GetComponent<StateManager>().lockOnTarget = playerCharacter.GetComponent<EnemyTarget>();
            enemies[i] = newEnemy;
        }
        enemiesTwo = new GameObject[levelTwoWaypoints.Length / 2];
        for (int i = 0; i < levelTwoWaypoints.Length / 2; i++)
        {
            GameObject newEnemy = GameObject.Instantiate(enemy, levelTwoWaypoints[2 * i].transform.position, new Quaternion(0, 0, 0, 0));
            newEnemy.GetComponent<AITypes>().waypoints = new GameObject[2];
            newEnemy.GetComponent<AITypes>().waypoints[0] = levelTwoWaypoints[2 * i];
            newEnemy.GetComponent<AITypes>().waypoints[1] = levelTwoWaypoints[2 * i + 1];
            newEnemy.GetComponent<StateManager>().lockOnTarget = playerCharacter.GetComponent<EnemyTarget>();
            enemiesTwo[i] = newEnemy;
        }
        enemiesThree = new GameObject[levelThreeWaypoints.Length / 2];
        for (int i = 0; i < levelThreeWaypoints.Length / 2; i++)
        {
            GameObject newEnemy = GameObject.Instantiate(enemy, levelThreeWaypoints[2 * i].transform.position, new Quaternion(0, 0, 0, 0));
            newEnemy.GetComponent<AITypes>().waypoints = new GameObject[2];
            newEnemy.GetComponent<AITypes>().waypoints[0] = levelThreeWaypoints[2 * i];
            newEnemy.GetComponent<AITypes>().waypoints[1] = levelThreeWaypoints[2 * i + 1];
            newEnemy.GetComponent<StateManager>().lockOnTarget = playerCharacter.GetComponent<EnemyTarget>();
            enemiesThree[i] = newEnemy;
        }
        enemiesFour = new GameObject[levelFourWaypoints.Length / 2];
        for (int i = 0; i < levelFourWaypoints.Length / 2; i++)
        {
            GameObject newEnemy = GameObject.Instantiate(enemy, levelFourWaypoints[2 * i].transform.position, new Quaternion(0, 0, 0, 0));
            newEnemy.GetComponent<AITypes>().waypoints = new GameObject[2];
            newEnemy.GetComponent<AITypes>().waypoints[0] = levelFourWaypoints[2 * i];
            newEnemy.GetComponent<AITypes>().waypoints[1] = levelFourWaypoints[2 * i + 1];
            newEnemy.GetComponent<StateManager>().lockOnTarget = playerCharacter.GetComponent<EnemyTarget>();
            enemiesFour[i] = newEnemy;
        }
        bossesKilled = PlayerPrefs.GetInt("bosses");
        EnemiesKilled = PlayerPrefs.GetInt("enemy");
        //closestEnemy = 0;

        playerCharacter.transform.position = new Vector3(PlayerPrefs.GetFloat("xCoord"), 0, PlayerPrefs.GetFloat("zCoord"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetFloat("xCoord", playerCharacter.transform.position.x);
            PlayerPrefs.SetFloat("zCoord", playerCharacter.transform.position.z);
            PlayerPrefs.SetInt("bosses", bossesKilled);
            PlayerPrefs.SetInt("enemy", EnemiesKilled);
        }
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_DEATH, GameOver);
        Messenger.RemoveListener(GameEvent.BOSS_KILL, BossKill);
	Messenger.RemoveListener(GameEvent.ENEMY_KILL, EnemyKill);
    }

    void GameOver()
    {
        SceneManager.LoadScene(3);
    }

    void BossKill()
    {
        bossesKilled++;
        if (bossesKilled == 1)
        {
            GameObject.Destroy(bossOneExit);
        }
        else if (bossesKilled == 2)
        {
            GameObject.Destroy(bossTwoExit);
        }
        else if (bossesKilled == 3)
        {
            GameObject.Destroy(bossThreeExit);
        }
        else if (bossesKilled == 4)
        {
            StartCoroutine(Victory());
        }
    }

    void EnemyKill()
    {
        EnemiesKilled++;
        if (EnemiesKilled >= 32)
        {
            GameObject.Destroy(bossFourEntry);
        }
        else if (EnemiesKilled >= 24)
        {
            GameObject.Destroy(bossThreeEntry);
        }
        else if (EnemiesKilled >= 16)
        {
            GameObject.Destroy(bossTwoEntry);
        }
        else if (EnemiesKilled >= 8)
        {
            GameObject.Destroy(bossOneEntry);
        }
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(6);
    }
}
