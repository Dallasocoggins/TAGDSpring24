using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGameManager : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        while (!gameManager)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    public void CallOnGameManager(string methodName) {
        gameManager.Invoke(methodName, 0);
    }
}
