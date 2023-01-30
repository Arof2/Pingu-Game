using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class collect : MonoBehaviour
{
    public static int coins = 0;
    [SerializeField] private AudioSource collectionSound;

    [SerializeField] TextMeshProUGUI coinsText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            coins++;
            coinsText.text = "Fish: " + coins + " / 5";
            collectionSound.Play();
        }
    }

}
