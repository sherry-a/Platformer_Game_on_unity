using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    Player_Movement1 Player1;
    [SerializeField]
    Player_Movement1 Player2;

    private TextMeshProUGUI scorebar;

    //[HideInInspector]
    public bool Player1Reached = false;
    //[HideInInspector]
    public bool Player2Reached = false;

    static int score = 0;

    [SerializeField]
    GameObject WinMenu;

    [SerializeField]
    GameObject LoseMenu;

    private GameObject Win;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        Player_Movement1.WhenPlayerDead += ShowDeathMenu;
        SceneManager.sceneLoaded += OnLoadedScene;
    }

    void OnLoadedScene(Scene scene, LoadSceneMode mode)
    {
        Player1Reached = false;
        Player2Reached = false;
        Player1 = GameObject.Find("P1 Punk").GetComponent<Player_Movement1>();
        Player2 = GameObject.Find("P2 Cyborg").GetComponent<Player_Movement1>();
        scorebar = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        Player_Movement1.WhenPlayerDead -= ShowDeathMenu;
    }

    private void Update()
    {
        if (Player1Reached == true && Player2Reached == true)  
        {
            if (Win == null) 
            {
                ShowWinMenu();
            }
        }
        if (scorebar != null) 
        {
            scorebar.text = "Score = " + score;
        }
    }
    public void ShowWinMenu()
    {
        Win = Instantiate(WinMenu);
        Win.SetActive(true);
        Player1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Player2.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Player1.enabled = false;
        Player2.enabled = false;
    }
    void ShowDeathMenu()
    {
        GameObject Lose = Instantiate(LoseMenu);
        Lose.SetActive(true);
        Player1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Player2.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Player1.enabled = false;
        Player2.enabled = false;
    }
    public void RestartScene()
    {
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadMainMenu()
    {
        score = 0;
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseScore()
    {
        score++;
    }
}
