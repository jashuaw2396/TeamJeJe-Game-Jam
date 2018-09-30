using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameObject.FindGameObjectWithTag("PlayerTracker").GetComponent<PlayerTracker>().PickupItem();
            Destroy(gameObject);
        }
    }
}
