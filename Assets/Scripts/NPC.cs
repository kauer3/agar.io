using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float mass = 1;
    public float speed = 0.2f;
    Vector3 velocity;

    void Update()
    {
        transform.localScale = new Vector3(mass, mass, mass);
        // Vector3 input = player1 ? new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) : new Vector3(Input.GetAxis("P2Horizontal"), Input.GetAxis("P2Vertical"), 0);
        Vector3 direction = new Vector3(Random.Range(0, 1), Random.Range(0, 1), 0);
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
        else if (triggerCollider.tag == "Player")
        {
            PlayerController player = triggerCollider.GetComponent<PlayerController>();
            if (mass > player.mass)
            {
                mass += player.mass;
                DestroyGameObject(triggerCollider.gameObject);
            }
        }
        else if (triggerCollider.tag == "NPC")
        {
            NPC npc = triggerCollider.GetComponent<NPC>();
            if (mass > npc.mass)
            {
                mass += npc.mass;
                DestroyGameObject(triggerCollider.gameObject);
            }
        }
    }

    private void DestroyGameObject(GameObject objectToBedestroyed)
    {
        Destroy(objectToBedestroyed);
    }
}