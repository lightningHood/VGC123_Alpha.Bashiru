using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    float maxDistance = 10;
    public float projectileFireRate;
    float timeSinceLastFire;
    Shoot shootScript;
    public AudioClip DeathSound;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        shootScript = GetComponent<Shoot>();
        shootScript.OnProjectileSpawned.AddListener(UpdateTimeSinceLastFire);

    }

    private void OnDisable()
    {
        shootScript.OnProjectileSpawned.RemoveListener(UpdateTimeSinceLastFire);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerInstance.transform.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        AnimatorClipInfo[] currentClips = anim.GetCurrentAnimatorClipInfo(0);

        if (currentClips[0].clip.name != "Fire" && Vector2.Distance(gameObject.transform.position, GameManager.instance.playerInstance.transform.position) <= maxDistance)
        {
            if (Time.time >= timeSinceLastFire + projectileFireRate)
            {
                anim.SetTrigger("Fire");
            }
        }
    }

    public override void Death()
    {
        AudioSourceManager.instance.PlayOneShot(DeathSound, false);
        Destroy(gameObject);
    }

    void UpdateTimeSinceLastFire()
    {
        timeSinceLastFire = Time.time;
    }
}
