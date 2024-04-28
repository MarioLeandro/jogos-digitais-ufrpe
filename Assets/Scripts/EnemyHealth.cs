using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class EnemyHealth : MonoBehaviour
{

    public int health;
    public int maxHealth = 100;
    public Image red;
    public Image green;
    public TextMeshProUGUI text;
    public EnemyController enemyController;
    public EnemyHealth enemyHealth;
    public Transform[] spawnPoints;
    public GameObject enemy;
    public GameObject clone;
    private int level = 1;

    void Start()
    {
        maxHealth += (level * 10);
        health = maxHealth;
        text.text = $"Nivel {level}";
    }

    public void TakeDamage(int amount) {
        health -= amount;
        if(health <= 0) {
            Vector3 greenScale = green.rectTransform.localScale;
            greenScale.x = (float) 0 / maxHealth;
            green.rectTransform.localScale = greenScale;
            StartCoroutine(ReduceRedBar(greenScale));
            enemyController._enemyAnimator.SetTrigger("isDeath");
            enemyController._enemyRigidbody2D.isKinematic = true;
            StartCoroutine(DestroyEnemy());
        } else {
            Vector3 greenScale = green.rectTransform.localScale;
            greenScale.x = (float)health / maxHealth;
            green.rectTransform.localScale = greenScale;
            StartCoroutine(ReduceRedBar(greenScale));
            enemyController._enemyAnimator.SetTrigger("isHurt");
        }
    }

    public void EnemyDeath() {
        DestroyEnemy();
    }

    IEnumerator DestroyEnemy() 
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
        level++;
        enemyController._enemyRigidbody2D.isKinematic = false;
        text.text = $"Nivel {level}";
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);
        clone = Instantiate(enemy, spawnPoints[randSpawnPoint].position, Quaternion.identity);
        clone.SetActive(true);
        EnemyHealth cloneEnemyHealth = clone.GetComponent<EnemyHealth>();
        enemyHealth = cloneEnemyHealth;
        // EnemyController cloneEnemyController = clone.GetComponent<EnemyController>();
        // enemyController = cloneEnemyController;
        // enemyController.enemyLevel = enemyController.enemyLevel
        enemyHealth.enemy = enemy;
        enemyHealth.level = level;
        Debug.Log($"Nivel {level}");
    }

    IEnumerator ReduceRedBar(Vector3 newScale) 
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 redScale = red.transform.localScale;

        while(red.transform.localScale.x > newScale.x) {
            redScale.x -= Time.deltaTime * 1f;
            red.transform.localScale = redScale;

            yield return null;
        }

        red.transform.localScale = newScale;

    }

}
