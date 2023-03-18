using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Game;
    public bool paused;
    private void Awake() {
        Game = this;
    }

    public void ChangePausedState() {
        paused = !paused;
    }
    public void setTimeScale(int scale) {
        Time.timeScale = scale;
    }
}
