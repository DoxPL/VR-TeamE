using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CoopSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void ServerListen()
    {
        NetworkServer.RegisterHandler<ReadyMessage>(OnClientReady);

        // start listening, and allow up to 4 connections
        NetworkServer.Listen(4);
    }

    void OnClientReady(NetworkConnection conn, ReadyMessage msg)
    {
        NetworkServer.SetClientReady(conn);
        SpawnTrees();
    }

    void SpawnTrees()
    {
        int x = 0;
        for (int i = 0; i < 5; ++i)
        {
            GameObject treeGo = Instantiate(enemyPrefab, new Vector3(x++, 1.06f, -34.35f), Quaternion.identity);
            NetworkServer.Spawn(treeGo);
        }
    }
}
