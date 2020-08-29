using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : Enemy
{
    public GameObject playerObject;
    private Player player;
    private float shotTimer;
    private float chasingTimer;
    private NavMeshAgent agent;

    public float shotInterval = 2.5f;
    public float chasingInterval = 2.5f;
    public float shotDistance = 7f;
    public float chasingDistance = 11f;

    void Start()
    {
        player = playerObject.GetComponent<Player>();
        shotTimer = Random.Range(0, shotInterval);
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
    }


    void Update()
    {
        if (player.IsKilled)
        {
            agent.enabled = false;
            this.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }

        shotTimer -= Time.deltaTime;
        if (shotTimer <= 0 && (Vector3.Distance(transform.position, player.transform.position) <= shotDistance))
        {
            shotTimer = shotInterval;
            GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false);
            bullet.transform.position = transform.position;
            bullet.transform.forward = (player.transform.position - transform.position).normalized;
            agent.SetDestination(player.transform.position);
        }

        chasingTimer -= Time.deltaTime;

        if(chasingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= chasingDistance)
        {
            chasingTimer = chasingInterval;
            agent.SetDestination(player.transform.position);
        }
    }

    protected override void OnKill()
    {
        base.OnKill();
        agent.enabled = false;
        this.enabled = false;

        transform.localEulerAngles = new Vector3(10, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
