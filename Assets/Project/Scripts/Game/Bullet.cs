using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletLifeTimer;
    private bool isShotByPlayer;

    public float bulletSpeed = 17f;
    public float bulletLifeDuration = 2f;
    public int bulletDamage = 5;

    public bool IsShotByPlayer{
        get { return isShotByPlayer; }
        set { isShotByPlayer = value; }
    }


    // Start is called before the first frame update
    void OnEnable()
    {
        bulletLifeTimer = bulletLifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        bulletLifeTimer -= Time.deltaTime;

        if(bulletLifeTimer <= 0f)
        {
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}
