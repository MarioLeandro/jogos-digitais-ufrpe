using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagePunch : MonoBehaviour
{
    // public EnemyHealth enemyHealth;
    public PlayerController playerController;

    [SerializeField] PlayerHealth playerHealth;

    public int _damage = 25;

    [SerializeField] GameObject aura;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.L) && playerController.enemyKilled == 5){
            playerController.enemyKilled = 0;
            playerController.special.text = $"Despertar {playerController.enemyKilled}/5";
            aura.SetActive(true);
            _damage *= 3;
            playerHealth.TakeDamage(playerHealth.health / 3);
            StartCoroutine(Awakening());
        }
        
    }

    IEnumerator Awakening() 
    {
        yield return new WaitForSeconds(10f);

        aura.SetActive(false);
        _damage /= 3;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // if(enemyHealth.clone){
        //     EnemyHealth cloneEnemyHealth = enemyHealth.clone.GetComponent<EnemyHealth>();
        //     enemyHealth = cloneEnemyHealth;
        // }
        if(other.CompareTag("Enemy")) {
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
            if(enemyHealth.health > 0) {
                enemyHealth.TakeDamage(_damage);
            }
        }
    }
}
