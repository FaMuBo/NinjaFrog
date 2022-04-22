using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public ParticleSystem explosion;
    [CanBeNull] public Transform pointA, pointB;
    private float speed = 3f;
    private Vector3 _currentTarget;
    private void Update()
    {
        if (pointA != null && pointB != null)
        {
            if (transform.position == pointA.position)
            {
                _currentTarget = pointB.position;
            } else if (transform.position == pointB.position)
            {
                _currentTarget = pointA.position;
            }
        
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            explosion.startColor = Color.green;
            Instantiate(explosion, col.gameObject.transform.position, col.gameObject.transform.rotation);
            GameManager.instance.GameOver();
            col.gameObject.SetActive(false);
            GameManager.instance.Restart();
        }
    }
}
