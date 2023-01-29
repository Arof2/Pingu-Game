using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(SphereCollider))]
public class SingularityCore : MonoBehaviour
{
    //This script is responsible for what happens when the pullable objects reach the core
    //by default, the game objects are simply turned off
    //as this is much more performant than destroying the objects
    void OnTriggerStay (Collider other) {
        if(other.GetComponent<SingularityPullable>()){
            other.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Awake(){
        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
}
