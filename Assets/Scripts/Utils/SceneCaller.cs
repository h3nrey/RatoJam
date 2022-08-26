using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneCaller : MonoBehaviour
{
    public static SceneCaller _sceneCaller;
    public string currentScene;

    private void Awake() {
        _sceneCaller = this;
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void CallScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void CallNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
