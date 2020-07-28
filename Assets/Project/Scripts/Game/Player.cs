using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("View")]
    public Camera playerCamera;

    [Header("Gameplay")]
    public uint initAmmunation = 15U;
    public int initHealth = 50;
    public float recoilDuration = 0.5f;
    public float recoilForce = 120f;

    private int health;
    private uint ammunation;
    private bool isHurt;

    public int Health
    {
        get { return health; }
    }

    public uint Ammunation {
        get { return ammunation; }
    }

    private bool isKilled;
    public bool IsKilled {
        get { return isKilled; }
    }

    void Start()
    {
        ammunation = initAmmunation;
        health = initHealth;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (ammunation > 0 && !IsKilled)
            {
                ammunation--;
                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet(true); // Instantiate(bulletPfab);
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.GetComponent<AmmunationBox>() != null)
        {
            AmmunationBox ammunationBox = otherCollider.GetComponent<AmmunationBox>();
            ammunation += ammunationBox.ammunation;
            Destroy(ammunationBox.gameObject);
        }
        else if (otherCollider.GetComponent<HealthBox>() != null)
        {
            HealthBox healthBox = otherCollider.GetComponent<HealthBox>();
            health += healthBox.health;
            Destroy(healthBox.gameObject);
        }
        else
        {
            /* Do nothing */
        }

        if (!isHurt)
        {
            GameObject gameObject = null; //hazard

            if(otherCollider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = otherCollider.GetComponent<Enemy>();
                if(!enemy.IsKilled)
                {
                    gameObject = enemy.gameObject;
                    health -= enemy.damage;
                }
            }
            else if(otherCollider.GetComponent<Bullet>() != null)
            {
                Bullet bullet = otherCollider.GetComponent<Bullet>();
                if(!bullet.IsShotByPlayer)
                {
                    gameObject = bullet.gameObject;
                    health -= bullet.bulletDamage;
                    bullet.gameObject.SetActive(false);
                }
            }
            else
            {
                /* Do nothing */
            }

            if (gameObject != null)
            {
                isHurt = true;

                Vector3 hurtDirection = (transform.position - gameObject.transform.position).normalized;
                Vector3 recoilDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiver>().AddForce(recoilDirection, recoilForce);

                StartCoroutine(HurtRoutine());
            }

            if (health <= 0)
            {
                if (!isKilled)
                {
                    isKilled = true;
                    OnKill();
                }
            }
        }
    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(recoilDuration);
        isHurt = false;
    }

    public void OnKill()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
    }
}
