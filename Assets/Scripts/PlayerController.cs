using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mass = 1;
    public float speed = 0.2f;
    Vector3 velocity;
    public bool player1;
    public new Camera camera;

    void Update()
    {
        transform.localScale = new Vector3(mass, mass, mass);
        Vector3 input = player1 ? new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) : new Vector3(Input.GetAxis("P2Horizontal"), Input.GetAxis("P2Vertical"), 0);
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
            DestroyGameObject(triggerCollider.gameObject);
        }
        else
        {
            PlayerController player = triggerCollider.GetComponent<PlayerController>();
            if (mass > player.mass)
            {
                mass += player.mass;
                DestroyGameObject(triggerCollider.gameObject);
                camera.rect = new Rect(0, 0, 1, 1);
            }
        }
    }

    private void DestroyGameObject(GameObject objectToBedestroyed)
    {
            Destroy(objectToBedestroyed);
            camera.orthographicSize = mass * 2;
    }
}
