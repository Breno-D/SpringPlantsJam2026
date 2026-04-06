using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCombat : MonoBehaviour
{
    GameObject target;
    public ScriptableTurret turret;
    public GameObject bulletGo;
    Animator anim;
    bool canShoot;
    float currShootTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("SearchForTarget", 0f, 0.5f);
        canShoot = true;
        currShootTimer = turret.timeToShoot;
    }

    void Update()
    {
        if(canShoot)
        {
            ShootTarget();
        }
        else
        {
            currShootTimer -= Time.deltaTime;
        }

        if(currShootTimer <= 0)
        {
            canShoot = true;
        }
    }

    void SearchForTarget()
    {
        float minDistance = Mathf.Infinity;
        if(target == null)
        {
            foreach(GameObject enemyGo in WaveManager.instance.enemiesInWave)
            {
                float currentDistance = Vector2.Distance(transform.position, enemyGo.transform.position);
                if(currentDistance < minDistance && currentDistance < turret.range)
                {
                    minDistance = currentDistance;
                    target = enemyGo;
                }
            }
        }
        else if(Vector2.Distance(transform.position, target.transform.position) > turret.range)
        {
            target = null;
        }
    }

    void ShootTarget()
    {
        if(target == null)
        {
            anim.SetBool("shooting", false);
            return;
        }
        else
        {
            canShoot = false;
            currShootTimer = turret.timeToShoot;
            anim.SetBool("shooting", true);
            GameObject newBullet = Instantiate(bulletGo, transform.position, Quaternion.identity);
            newBullet.GetComponent<BulletScript>().SetBulletProprierties(target, turret.damage, turret.isAreaAttack);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, turret.range);
    }
}
