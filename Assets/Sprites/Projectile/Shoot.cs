using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;

    public float projectilespeed;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();


        if (projectilespeed <= 0)
            projectilespeed = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log("please set default values on: " + gameObject.name);
    }

    public void Fire()
    {
        if (! sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, 
                spawnPointRight.rotation);
            curProjectile.speed = projectilespeed;
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position,
                spawnPointLeft.rotation);
            curProjectile.speed = projectilespeed;
        }
    }
  
}
