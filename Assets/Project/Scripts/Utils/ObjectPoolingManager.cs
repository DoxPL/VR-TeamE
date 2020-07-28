using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance {
        get {
            return instance;
        }
    }
    public GameObject bulletPfab;
    public int amount = 25;
    private List<GameObject> bulletList;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        bulletList = new List<GameObject>(amount);

        for(int i=0; i < amount; i++)
        {
            GameObject pfabInstance = Instantiate(bulletPfab);
            pfabInstance.transform.SetParent(transform);
            pfabInstance.SetActive(false);
            bulletList.Add(pfabInstance);
        }
    }

    public GameObject GetBullet(bool isShotByPlayer)
    {
        foreach(GameObject bullet in bulletList)
        {
            if(!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().IsShotByPlayer = isShotByPlayer;
                return bullet;
            }
        }

        GameObject pfabInstance = Instantiate(bulletPfab);
        pfabInstance.transform.SetParent(transform);
        pfabInstance.GetComponent<Bullet>().IsShotByPlayer = isShotByPlayer;
        bulletList.Add(pfabInstance);
        return pfabInstance;
    }
}
