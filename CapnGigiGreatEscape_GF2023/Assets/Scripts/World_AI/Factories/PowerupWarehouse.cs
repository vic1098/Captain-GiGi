using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Factory))]
public class PowerupWarehouse : Factory
{
    private Vector3 spawnLocation;
    private Transform powerupParent; 

    private void Awake()
    {
        // Picks its place in the hierarchy 
        powerupParent = GameObject.Find("Powerups_Active").transform;

        // Gets the objects spawn location
        spawnLocation = transform.position;
    }
    private void Start()
    {
        GenerateRandomPowerup(powerups, spawnLocation, powerupParent);
    }

    private void GenerateRandomPowerup(List<GameObject> powerupList, Vector3 spawnLocation, Transform powerupParent)
    {
        if (powerupList != null)
        {
            int randomPowerup = UnityEngine.Random.Range(0, powerupList.Count);


            int randomRoll = Random.Range(0, 100);


            if (randomRoll <= 75)
            {
                Debug.Log("Better luck next time! Your Roll: " + randomRoll);
                return;
            }
            else
            {
                Instantiate(powerupList[randomPowerup], spawnLocation, Quaternion.identity, powerupParent);
            }
        } 
        else
        {
            Debug.Log("Stash is dry, Cap'n!");
            return; 
        }

    }
}
