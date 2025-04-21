using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    public bool playerStarted = false, constantUpdate = false;

    public float height = 0;
    
    void Start()
    {
        player = Character.Instance.transform;
    }
    void Update()
    {
        if (!constantUpdate)
        {
            if (!playerStarted)
            {
                if (player != null)
                {
                    playerStarted = true;
                    transform.SetParent(player.transform);
                    transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                }
            }
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + height, player.transform.position.z);
        }
    }
}
