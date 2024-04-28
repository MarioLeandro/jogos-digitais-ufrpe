using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public int _damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(enemyHealth.clone){
            EnemyHealth cloneEnemyHealth = enemyHealth.clone.GetComponent<EnemyHealth>();
            enemyHealth = cloneEnemyHealth;
        }
        if(other.CompareTag("Enemy")) {
            // Debug.Log("entrou aki");
            enemyHealth.TakeDamage(_damage);
        }
    }
}
