using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public Player player;

    public GameObject Enemy;
    public GameObject EnemyFast;
    public GameObject EnemyTank;

    //Character base stats
    private int maxhealth = 5;
    private int damage = 5;
    private int speed = 5;

    private bool isPlayerInRoom = false;
    private bool enemiesSpawned = false;
    private bool nextFloorSpawned = false;

    public PointOfStart startpoint;
    public NextFloor nextpoint;

    //interface
    public Text healthStatus;
    public Slider healthBar;
    public Text damageStatus;
    public Text speedStatus;
    public Text waveCount;

    private int waveCounter = -1;

    private bool isPaused = false;

    public Canvas escMenu;

    private GameObject BGMusic;
    private Animator BGMusicAnimator;
    private AudioSource BGMusicSource;
    private static GManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlayerInRoom = false;
        enemiesSpawned = false;
        nextFloorSpawned = false;
        player = FindObjectOfType<Player>();
        BGMusic = GameObject.FindGameObjectWithTag("BgMusic");
        BGMusicSource = BGMusic.GetComponent<AudioSource>();
        BGMusicAnimator = BGMusic.GetComponent<Animator>();
        escMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRoom)
        {
            GameObject enemies = GameObject.FindGameObjectWithTag("Enemy");
            if (enemies == null)
            {
                if (!enemiesSpawned)
                {
                    enemiesSpawned = true;
                    switch (waveCounter)
                    {
                        case 0:
                            Instantiate(Enemy, spawn1.transform.position, Quaternion.identity);
                            Instantiate(Enemy, spawn2.transform.position, Quaternion.identity);
                            Instantiate(Enemy, spawn3.transform.position, Quaternion.identity);
                            Instantiate(Enemy, spawn4.transform.position, Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(Enemy, spawn1.transform.position, Quaternion.identity);
                            Instantiate(EnemyFast, spawn2.transform.position, Quaternion.identity);
                            Instantiate(Enemy, spawn3.transform.position, Quaternion.identity);
                            Instantiate(EnemyFast, spawn4.transform.position, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(EnemyTank, spawn1.transform.position, Quaternion.identity);
                            Instantiate(Enemy, spawn2.transform.position, Quaternion.identity);
                            Instantiate(EnemyTank, spawn3.transform.position, Quaternion.identity);
                            Instantiate(Enemy, spawn4.transform.position, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(EnemyTank, spawn1.transform.position, Quaternion.identity);
                            Instantiate(EnemyFast, spawn2.transform.position, Quaternion.identity);
                            Instantiate(EnemyTank, spawn3.transform.position, Quaternion.identity);
                            Instantiate(EnemyFast, spawn4.transform.position, Quaternion.identity);
                            break;
                        case 4:
                            Instantiate(EnemyTank, spawn1.transform.position, Quaternion.identity);
                            Instantiate(EnemyTank, spawn2.transform.position, Quaternion.identity);
                            Instantiate(EnemyTank, spawn3.transform.position, Quaternion.identity);
                            Instantiate(EnemyTank, spawn4.transform.position, Quaternion.identity);
                            break;
                        default:
                            Instantiate(Enemy, spawn1.transform.position, Quaternion.identity);
                            break;
                    }
                }
                else
                {
                    if (!nextFloorSpawned)
                    {
                        nextFloorSpawned = true;
                        BGMusicAnimator.SetTrigger("FadeOut");
                        Instantiate(nextpoint, new Vector3(0f, 0.2f, 0f), Quaternion.identity);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                escMenu.enabled = false;
                Time.timeScale = 1;
            }
            else
            {
                isPaused = true;
                escMenu.enabled = true;
                Time.timeScale = 0;
            }
        }
    }

    public void StartFloor()
    {
        isPlayerInRoom = true;
        player.transform.position = new Vector3(0f, 0.4f, 0f);
        BGMusicSource.Play();
    }

    public void EndFloor()
    {
        if (waveCounter == 4)
        {
            Debug.Log("GameOver");
            waveCounter++;
            waveCount.text = "Floors cleared: " + waveCounter.ToString();
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void increaseHealth()
    {
        maxhealth+=2;
        player.RefreshStats();
        refreshInterface();
        Instantiate(startpoint, new Vector3(-88f, 1.25f, -10f), Quaternion.identity);
    }
    public void increaseDamage()
    {
        damage+=2;
        player.RefreshStats();
        refreshInterface();
        Instantiate(startpoint, new Vector3(-88f, 1.25f, -10f), Quaternion.identity);
    }
    public void increaseSpeed()
    {
        speed+=2;
        player.RefreshStats();
        refreshInterface();
        Instantiate(startpoint, new Vector3(-88f, 1.25f, -10f),Quaternion.identity);
    }

    public int getHealth()
    {
        return maxhealth;
    }
    public int getDamage()
    {
        return damage;
    }
    public int getSpeed()
    {
        return speed;
    }

    public void RestartScena()
    {
        if (waveCounter < 5)
        {
            isPlayerInRoom = false;
            enemiesSpawned = false;
            nextFloorSpawned = false;
            player = FindObjectOfType<Player>();
            BGMusic = GameObject.FindGameObjectWithTag("BgMusic");
            BGMusicSource = BGMusic.GetComponent<AudioSource>();
            BGMusicAnimator = BGMusic.GetComponent<Animator>();
            waveCounter++;
            player.setHealth(maxhealth);
            refreshInterface();
        }
    }

    void refreshInterface()
    {
        float curHealth = player.getHealth();
        float curMaxHealth = maxhealth;
        healthBar.value = curHealth / curMaxHealth;
        healthStatus.text = curHealth.ToString() + "/" + maxhealth.ToString();
        speedStatus.text = "Speed: " + speed.ToString();
        damageStatus.text = "Damage: " + damage.ToString();
        waveCount.text = "Floors cleared: " + waveCounter.ToString();
    }

    public void getDamageInterface()
    {
        float curHealth = player.getHealth();
        float curMaxHealth = maxhealth;
        healthBar.value = curHealth / curMaxHealth;
        healthStatus.text = curHealth.ToString() + "/" + maxhealth.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        maxhealth = 5;
        damage = 5;
        speed = 5;
        isPlayerInRoom = false;
        enemiesSpawned = false;
        nextFloorSpawned = false;
        waveCounter = -1;
        isPaused = false;
        escMenu.enabled = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayerDeath()
    {
        Time.timeScale = 0;
        isPaused = true;
        escMenu.enabled = true;
    }
}
