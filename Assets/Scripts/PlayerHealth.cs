using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int maxHealth = 100;
    public Image red;
    public Image green;
    public PlayerController playerController;


    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount) {
        health -= amount;
        Debug.Log(health);
        playerController._playerAnimator.SetTrigger("isHurt");
        if(health <= 0) {
            Debug.Log("morreu");
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        Vector3 greenScale = green.rectTransform.localScale;
        greenScale.x = (float)health / maxHealth;
        green.rectTransform.localScale = greenScale;
        StartCoroutine(ReduceRedBar(greenScale));
    }

    IEnumerator ReduceRedBar(Vector3 newScale) 
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 redScale = red.transform.localScale;

        while(red.transform.localScale.x > newScale.x) {
            redScale.x -= Time.deltaTime * 0.25f;
            red.transform.localScale = redScale;

            yield return null;
        }

        red.transform.localScale = newScale;

    }

}
