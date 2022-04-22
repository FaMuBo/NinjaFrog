using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA, pointB;
    public ParticleSystem explosion;

    private Vector3 _currentTarget;
    private float speed = 1f;
    public bool directionToRight = true;

    private void Update()
    {
        if (transform.position == pointA.position)
        {
            if (!directionToRight)
            {
                EnemyFlip();
            }
            _currentTarget = pointB.position;
        } else if (transform.position == pointB.position)
        {
            EnemyFlip();
            _currentTarget = pointA.position;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }

    void EnemyFlip()
    {
        directionToRight = !directionToRight;
        Vector3 heroScale = gameObject.transform.localScale;
        heroScale.x *= -1;
        gameObject.transform.localScale = heroScale;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            explosion.startColor = Color.blue;
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

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
