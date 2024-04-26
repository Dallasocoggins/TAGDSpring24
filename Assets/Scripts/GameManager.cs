using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int points = 0;
    float lastPlayerMaxHeight = 0;
    public GameObject pauseMenu;

    public int rockIndex = 0;
    public int backgroundIndex = 0;

    public static GameManager instance { get; private set; }

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadPoints(); // Load saved points when the game starts
        pauseMenu.SetActive(false);
    }

    void Update()
    {

    }

    public void UpdatePoints(player p)
    {
        if (lastPlayerMaxHeight < p.gameObject.transform.localPosition.y)
        {
            points += (int)p.gameObject.transform.localPosition.y;
            lastPlayerMaxHeight = p.gameObject.transform.localPosition.y;
            Debug.Log("Points: " + points);
            SavePoints(); // Save points after updating
        }
    }

    void SavePoints()
    {
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.Save();
    }

    void LoadPoints()
    {
        if (PlayerPrefs.HasKey("Points"))
        {
            points = PlayerPrefs.GetInt("Points");
            Debug.Log("Loaded Points: " + points);
        }
        else
        {
            Debug.Log("No saved points found.");
        }
    }

    public void PauseMenuToggle()
    {
        if (!pauseMenu)
        {
            Debug.Log("Error no pause menu linked");
            return;
        }

        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        } else if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void HowTo() {
        SceneManager.LoadScene("HowTo", LoadSceneMode.Additive);
    }

    public void Credits() {
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }

    public void Inventory()
    {
        SceneManager.LoadScene("Inventory", LoadSceneMode.Additive);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
