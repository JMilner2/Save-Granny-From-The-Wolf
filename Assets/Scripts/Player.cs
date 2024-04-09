using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    int keys = 0;
    int locksUnlocked = 0;
    public bool gameEnd = false;
    [SerializeField] TextMeshProUGUI popUpBox;
    [SerializeField] int maxLocks = 2;
    [SerializeField] Transform startPos;

    private void Start()
    {
        popUpBox.text = "";
    }

    private void Awake()
    {
        gameEnd = false;
        transform.position = startPos.position;
        keys = 0;
        locksUnlocked = 0;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Key")) //pickup key. checks if player already has a key, if not they pick up
        {
            if (keys == 1)
            {
                popUpBox.text = "Only carry one key at a time";
                StartCoroutine(SimpleWaitCoroutine());

            }
            else
            {
                other.gameObject.SetActive(false);
                keys++;
            }
           
        }
        if (other.gameObject.CompareTag("Finish")) //Unlock a lock. When player gets to the door if they have a key unlock a lock.
        {
            if (keys == 1)
            {
                keys = 0;
                locksUnlocked++;
                popUpBox.text = "Locks unlocked = " + locksUnlocked;
                StartCoroutine(SimpleWaitCoroutine());

            }
            else
            {
                popUpBox.text = "Find a Key to Unlock";
                StartCoroutine(SimpleWaitCoroutine());
            }
        }
      
    }


    private void Update()
    {
        if(locksUnlocked == maxLocks)
        {
            gameEnd = true; //All locks unlocked so game win
        }
        
    }


    IEnumerator SimpleWaitCoroutine() //used for popup box so its only active for 2secs
    {
        yield return new WaitForSeconds(2f);
        popUpBox.text = "";
    }

}
