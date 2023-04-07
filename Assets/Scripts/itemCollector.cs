using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{

    [SerializeField] private Text CherriesCount;

    [SerializeField] private AudioSource collectionSoundEffect;

    private int cherries = 0;

   private void OnTriggerEnter2D(Collider2D collision)
   {
        if(collision.gameObject.CompareTag("Cherry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            CherriesCount.text = "Cherries: " + cherries;
        }
   }
}
