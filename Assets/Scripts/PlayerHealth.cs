using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{

    public int health;
    public int maxHealth = 100;
    public Image red;
    public Image green;
    public PlayerController playerController;
    public GameOverScreen gameOverScreen;


    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount) {
        health -= amount;
        playerController._playerAnimator.SetTrigger("isHurt");
        playerController.collider.enabled = false;
        playerController.colliderJab.enabled = false;
        playerController.colliderPunch.enabled = false;
        if(health <= 0) {
            gameOverScreen.Setup();
        } else {
            Vector3 greenScale = green.rectTransform.localScale;
            greenScale.x = (float)health / maxHealth;
            green.rectTransform.localScale = greenScale;
            StartCoroutine(ReduceRedBar(greenScale));
        }
        
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
