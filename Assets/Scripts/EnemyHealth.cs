using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class EnemyHealth : MonoBehaviour
{

    public int health;
    public int maxHealth = 90;
    public Image red;
    public Image green;
    public TextMeshProUGUI text;
    public EnemyController enemyController;
    public PlayerController playerController;
    public EnemyHealth enemyHealth;
    public Transform[] spawnPoints;
    public GameObject enemy;
    public GameObject clone;
    private int level = 1;
    private int id;

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
            enemyController.enemyCount -= 1;
            if(playerController.enemyKilled < 5) {
                playerController.enemyKilled++;
            }
            playerController.special.text = $"Despertar {playerController.enemyKilled}/5";
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

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy").Where(obj => obj.activeInHierarchy).ToArray();
        
        Debug.Log($"enemies {objectsWithTag.Length} id {id} level {level}");

        if (enemyController.enemyCount == 0 && objectsWithTag.Length == 1)
        {
            Destroy(gameObject);
            level++;
            enemyController.enemyCount = level;
            text.text = $"Nivel {level}";
            SpawnEnemies();
        } else if (enemyController.enemyCount == 0 && objectsWithTag.Length <= 2 && (id+1) == level)
        {
            Destroy(gameObject);
            level++;
            enemyController.enemyCount = level;
            text.text = $"Nivel {level}";
            SpawnEnemies();
        } else {
            Destroy(gameObject);
        }
    }

    private void SpawnEnemies(){
    for (int i = 0; i < level; i++) 
    {
        Vector3 spawnPosition = spawnPoints[0].position + Random.insideUnitSphere * 2f;
        spawnPosition.z = 0f; 
        
        GameObject clone = Instantiate(enemy, spawnPosition, Quaternion.identity);
        clone.SetActive(true);
        
        EnemyHealth cloneEnemyHealth = clone.GetComponent<EnemyHealth>();
        
        cloneEnemyHealth.enemy = enemy;
        cloneEnemyHealth.level = level;
        cloneEnemyHealth.id = i;
    }
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
