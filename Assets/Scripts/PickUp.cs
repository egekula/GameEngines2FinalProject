using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool _wasCollected = false;
    

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_wasCollected)
        {
            _wasCollected = true;
            FindObjectOfType<PlayerStateManager>().TakeCoin(10);
            
            Destroy(gameObject,0.16f);
            

        }
    }
}
