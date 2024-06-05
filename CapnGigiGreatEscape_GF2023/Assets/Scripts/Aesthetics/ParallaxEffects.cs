using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffects : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    // Starting position for the parallax game object 
    Vector2 startingPosition;
    // Start z value of the parallax game object 
    float startingZ;
    // Distance that the camera has moved from the starting position of the parallax objetc
    Vector2 canMoveSinceStart => (Vector2)cam.transform.position - startingPosition;
    // Camera z distance from the target
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    // If object  is in front of target, use near clip plane. If behind the target, use far clip plane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0? cam.farClipPlane :  cam.nearClipPlane));
    // The futherthe object from the player, the faster the parallax effect object will move. 
    // Drag it's z value closer to the target to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane; 

    // Start is called before the first frame update
    void Start(){
        // Get the player position 
        startingPosition = transform.position;
        // Get the z position
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update(){
        // When the target moves, move the parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + canMoveSinceStart * parallaxFactor;
        // The X/Y position changes based on target travel speed times the parallax factor, but Z stays consistent 
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
