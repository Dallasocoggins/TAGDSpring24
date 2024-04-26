using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadScene : MonoBehaviour
{
    public void Unload() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
