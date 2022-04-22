using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(Destruction), 2f);
    }
    void Destruction()
    {
        Destroy(this.gameObject);
    }
}
