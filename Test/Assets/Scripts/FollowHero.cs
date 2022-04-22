using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHero : MonoBehaviour
{
    public GameObject player;
    public float smoothTime;
    private Vector3 currentVelocity;
    private Vector3 _newCameraPosition;

    //private void Update()
    //{
    //    _newCameraPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    //}

    private void LateUpdate()
    {
        _newCameraPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _newCameraPosition, ref currentVelocity, smoothTime);
    }
}
