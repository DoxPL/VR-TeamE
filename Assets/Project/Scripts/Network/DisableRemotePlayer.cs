using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class DisableRemotePlayer : NetworkBehaviour
{
    private void Start()
    {
        string playerID = string.Format("{0}", this.netId);
        Player player = this.GetComponent<Player>();
        //PlayerMovementCoop playerCoop = this.GetComponent<PlayerMovementCoop>();

        if (isLocalPlayer)
        {
            player.enabled = true;
            //playerCoop.enabled = true;
        }
        else
        {
            player.enabled = false;
            //var camera = transform.Find("Main Camera");
            //camera.GetComponent<Camera>().enabled = false;

            //playerCoop.enabled = false;
        }

        player.SetPlayerName(playerID);
    }
}
