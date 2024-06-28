using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credits: https://www.youtube.com/watch?v=tMXgLBwtsvI&t=0s

public class ParallaxEffect : MonoBehaviour {

    [Header("Camera")]
    public Camera cam;
    public Transform followTarget;

    // starting pos of the G.O
    Vector2 startingPosition;

    // distance that the camera has moved from the startPos of the parallaxEffect
    Vector2 cam_move_since_start => (Vector2) cam.transform.position - startingPosition; // updating per frame

    // starting z value of the G.O
    float startingZValue;
    float z_DistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (z_DistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(z_DistanceFromTarget) / clippingPlane; // the further a obj goes from the player, the faster the parallaxEffect will move

    void Start() {
        startingPosition = transform.position;
        startingZValue = transform.position.z;

    } // Start

    void Update() {
        Vector2 newPosition = startingPosition + cam_move_since_start * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZValue);

    } // Update

} // ParallaxEffect
