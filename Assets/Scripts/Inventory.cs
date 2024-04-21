using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> characterOwnedRocks;
    int points = 0;

    public Dictionary<string, Sprite> potentialRocks;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void SaveInventory()
    {
        string json = JsonUtility.ToJson(characterOwnedRocks);
        PlayerPrefs.SetString("OwnedRocks", json);
        PlayerPrefs.Save();
    }

    void LoadInventory()
    {
        if (PlayerPrefs.HasKey("OwnedRocks"))
        {
            string json = PlayerPrefs.GetString("OwnedRocks");
            characterOwnedRocks = JsonUtility.FromJson<List<string>>(json);
        }
        else
        {
            Debug.Log("No saved inventory found.");
        }
    }
}
