using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLIfe : MonoBehaviour
{
    bool dead = false;
    [SerializeField] AudioSource deathSound;

    void Update()
    {
        if (transform.position.y < -1f && !dead)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBody"))
        {
           // GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerBehavior>().enabled = false;
            Die();
            
        }
    }

    void Die()
    {
        
        dead = true;
        collect.coins = 0;
        Invoke(nameof(ReloadLevel), 1.3f);
        deathSound.Play();
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
