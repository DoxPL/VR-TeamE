using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5;
    public int damage = 5;

    private bool isKilled = false;
    public bool IsKilled {
        get { return isKilled; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet.IsShotByPlayer)
            {
                health -= bullet.bulletDamage;

                bullet.gameObject.SetActive(false);

                if (health <= 0)
                {
                    if(!isKilled)
                    {
                        isKilled = true;
                        OnKill();
                    }
                }
            }
        }
    }

    protected virtual void OnKill()
    {

    }
}
