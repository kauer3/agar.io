using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mass = 1;
    public float speed = 0.2f;
    Vector3 velocity;
    new Camera camera;

    void Update()
    {
        transform.localScale = new Vector3(mass, mass, mass);
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 direction = input.normalized;
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        transform.Translate(velocity);

    }

    private void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Pickup")
        {
            Pickup pickup = triggerCollider.GetComponentInParent<Pickup>();
            mass += pickup.mass;
            Destroy(triggerCollider.gameObject);
            // calculate camera size relative to player size
            Camera.main.orthographicSize = mass * 2;
        }
    }
}
