using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Exit : MonoBehaviour
{
    private BoxCollider boxCollider;
    private FlightAttendent flightAttendent;

    private int count = 0;
    private void Start() {
        flightAttendent = GameObject.Find("Flight Attendent").GetComponent<FlightAttendent>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject != null)
        {
            Destroy(other.gameObject);
            count++;
        }
    }

    private void Update() {
        if (count == 5)
        {
            flightAttendent.EscapeAll();
            Destroy(gameObject);
        }
    }
}
