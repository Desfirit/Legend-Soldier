using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public event Action<GameObject, GameObject> OnProjectileCollided;

    private GameObject _shooter;

    private float _range;
    private float _speed;

    private Vector3 _direction;

    private float _traveledDistance;
    
    public void Fire(GameObject shooter, Vector3 target, float range, float speed)
    {
        _shooter = shooter;
        _range = range;
        _speed = speed;

        _direction = target - shooter.transform.position;
        _direction.y = 0f;
        _direction.Normalize();

        transform.LookAt(target, Vector3.up);

        _traveledDistance = 0f;

    }


    
    void Update()
    {
        float distanceToTravel = _speed * Time.deltaTime;

        transform.Translate(_direction * distanceToTravel, Space.World);

        _traveledDistance += distanceToTravel;
        if(_traveledDistance > _range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnProjectileCollided?.Invoke(_shooter, other.gameObject);
        Destroy(gameObject);
    }


}
