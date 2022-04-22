using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public GameObject particlePrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(particlePrefab, transform.position, transform.rotation);
            GameManager.instance.AddBanana();
            Destroy(gameObject);
        }
    }
}
