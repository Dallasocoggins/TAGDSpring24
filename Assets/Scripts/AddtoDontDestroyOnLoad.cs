using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtoDontDestroyOnLoad : MonoBehaviour
{
    static AddtoDontDestroyOnLoad instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }
}
