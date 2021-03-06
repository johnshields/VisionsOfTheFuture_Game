using UnityEngine;

/*
 * WhatMatAmIOn
 * Script to tell what Material the player is walking on - (used by Footsteps). 
 */
public class WhatMatAmIOn : MonoBehaviour
{
    public GameObject playerSteps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
            playerSteps.GetComponent<Footsteps>().mat = 0;
        else if (other.CompareTag("Concrete"))
            playerSteps.GetComponent<Footsteps>().mat = 1;
        else if (other.CompareTag("Metal"))
            playerSteps.GetComponent<Footsteps>().mat = 2;
    }
}
