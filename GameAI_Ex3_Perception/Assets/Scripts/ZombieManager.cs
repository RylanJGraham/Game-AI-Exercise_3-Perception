using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private List<ZombieController> zombies = new List<ZombieController>();
    public bool playerDetected = false;

    // Assign the player GameObject in the Inspector.
    public GameObject playerObject;

    void Start()
    {
        // Find and store all ZombieController scripts in the scene.
        zombies.AddRange(FindObjectsOfType<ZombieController>());
    }

    void Update()
    {
        if (!playerDetected)
        {
            // Check if any zombie has detected the player.
            foreach (var zombie in zombies)
            {
                if (zombie.CanSeePlayer())
                {
                    playerDetected = true;
                    PlayerDetected();
                    NotifyAllZombies();
                    break;
                }
            }
        }
    }

    // Notify all zombies to start chasing.
    private void NotifyAllZombies()
    {
        foreach (var zombie in zombies)
        {
            zombie.StartChasing(playerObject.transform);
        }
    }

    public void PlayerDetected()
    {

        ZombieController[] zombies = FindObjectsOfType<ZombieController>();
        foreach (ZombieController zombie in zombies)
        {
            zombie.SendMessage("OnPlayerDetected", SendMessageOptions.DontRequireReceiver);
        }
    }
}