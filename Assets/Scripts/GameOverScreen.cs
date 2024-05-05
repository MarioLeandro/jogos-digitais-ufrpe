using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject pauseBg;
    // Start is called before the first frame update
    public void Setup(){
        gameObject.SetActive(true);
    }

    public void RestartButton(){
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void StartButton(){
        SceneManager.LoadScene("SampleScene");
    }

    public void Pause(){
        Time.timeScale = 0;
        pauseBg.SetActive(true);
    }

    public void ResumeButton(){
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
