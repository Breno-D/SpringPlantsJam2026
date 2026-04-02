using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject bulletSplashPrefab;
    GameObject target;
    Rigidbody2D bulletRb;
    public float vel;
    int bulletDamage;
    bool isAreaAttack;
    
    void Awake()
    {
        Invoke("DestroyBullet", 2f);
        bulletRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Vector2 direction = target.transform.position - transform.position;
            bulletRb.velocity = direction.normalized * vel;
        }
    }

    public void SetBulletProprierties(GameObject targetToSet, int damageToDeal, bool _isAreaAttack)
    {
        target = targetToSet;
        bulletDamage = damageToDeal;
        isAreaAttack = _isAreaAttack;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == target)
        {
            other.GetComponent<EnemyHealth>()?.TakeDamage(bulletDamage);
            if(isAreaAttack)
            {
                Instantiate(bulletSplashPrefab, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
            return;
        }
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
        return;
    }
}
