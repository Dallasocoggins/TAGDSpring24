using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float floatPoints = 0f;
    public int points => (int)floatPoints;
    public float unitsPerPoint = 1f;
    float lastPlayerMaxHeight = 0;
    float maxHeight = 0;
    public GameObject pauseMenu;

    public int personIndex = 0;
    public int rockIndex = 0;
    public int backgroundIndex = 0;

    public bool[] unlockedPeople = new bool[32];
    public bool[] unlockedRocks = new bool[32];
    public bool[] unlockedBackgrounds = new bool[32];

    public static GameManager instance { get; private set; } = null;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadPoints(); // Load saved points when the game starts
        pauseMenu.SetActive(false);
    }

    public void UpdatePoints(player p)
    {
        if (lastPlayerMaxHeight < p.gameObject.transform.localPosition.y)
        {
            var heightDelta = p.gameObject.transform.localPosition.y - lastPlayerMaxHeight;
            floatPoints += heightDelta / unitsPerPoint;
            lastPlayerMaxHeight = p.gameObject.transform.localPosition.y;
            if(lastPlayerMaxHeight > maxHeight)
            {
                maxHeight = lastPlayerMaxHeight;
            }
            p.UpdateHeightText(maxHeight);
            //p.UpdatePointsText(points);
            Debug.Log("Points: " + points);
            SavePoints(); // Save points after updating
        }
    }

    public void ClearPoints()
    {
        floatPoints = 0;

        unlockedPeople[0] = true;
        unlockedRocks[0] = true;
        unlockedBackgrounds[0] = true;

        for (int i = 1; i < 32; i++) {
            unlockedPeople[i] = false;
            unlockedRocks[i] = false;
            unlockedBackgrounds[i] = false;
        }

        SavePoints();
    }

    public void SubtractPoints(int amount) {
        floatPoints -= amount;
        SavePoints();
    }

    public void SavePoints()
    {
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.SetFloat("MaxHeight", maxHeight);

        PlayerPrefs.SetInt("UnlockedPeople", ConvertToInt(unlockedPeople));
        PlayerPrefs.SetInt("UnlockedRocks", ConvertToInt(unlockedRocks));
        PlayerPrefs.SetInt("UnlockedBackgrounds", ConvertToInt(unlockedBackgrounds));

        PlayerPrefs.Save();
    }

    void LoadPoints()
    {
        if (PlayerPrefs.HasKey("Points"))
        {
            floatPoints = PlayerPrefs.GetInt("Points");
            Debug.Log("Loaded Points: " + points);
        }
        else
        {
            Debug.Log("No saved points found.");
        }


        if (PlayerPrefs.HasKey("MaxHeight"))
        {
            maxHeight = PlayerPrefs.GetInt("MaxHeight");
            Debug.Log("Loaded Height: " + maxHeight);
        }
        else
        {
            Debug.Log("No saved points found.");
        }

        if (PlayerPrefs.HasKey("UnlockedPeople")) {
            int unlockedPeopleInt = PlayerPrefs.GetInt("UnlockedPeople");
            for (int i = 0; i < 32; i++) {
                unlockedPeople[i] = ((unlockedPeopleInt >> i) & 1) != 0;
            }
        }
        else {
            Debug.Log("No saved unlocked rocks found.");
            unlockedPeople[0] = true;
        }

        if (PlayerPrefs.HasKey("UnlockedRocks")) {
            int unlockedRocksInt = PlayerPrefs.GetInt("UnlockedRocks");
            for (int i = 0; i < 32; i++) {
                unlockedRocks[i] = ((unlockedRocksInt >> i) & 1) != 0;
            }
        }
        else {
            Debug.Log("No saved unlocked rocks found.");
            unlockedRocks[0] = true;
        }

        if (PlayerPrefs.HasKey("UnlockedBackgrounds")) {
            int unlockedBackgroundsInt = PlayerPrefs.GetInt("UnlockedBackgrounds");
            for (int i = 0; i < 32; i++) {
                unlockedBackgrounds[i] = ((unlockedBackgroundsInt >> i) & 1) != 0;
            }
        }
        else {
            Debug.Log("No saved unlocked rocks found.");
            unlockedBackgrounds[0] = true;
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
        lastPlayerMaxHeight = 0;
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

    int ConvertToInt(bool[] array) {
        int output = 0;
        for (int i = 0; i < 32; i++) {
            output |= array[i] ? 1 << i : 0;
        }
        return output;
    }
}
