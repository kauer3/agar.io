using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mass = 1;
    public float speed = 0.2f;
    Vector3 velocity;
    public bool player2;
    public new Camera camera;

    void Update()
    {
        transform.localScale = new Vector3(mass, mass, mass);
        string inputPrefix = player2 ? "P2" : "";
        Vector3 input = new Vector3(Input.GetAxis(inputPrefix + "Horizontal"), Input.GetAxis(inputPrefix + "Vertical"), 0);
        Vector3 direction = input.normalized;
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        transform.Translate(velocity);
    }

    private void OnTriggerEnter(Collider triggerCollider)
    {
        // print collider component
        // Debug.Log(triggerCollider);
        // Debug.Log(Equals(triggerCollider, GetComponent<Collider>()));
        float objectMass = 0;
        bool isPlayer = false;
        if (triggerCollider.tag == "Pickup")
        {
            objectMass = triggerCollider.GetComponentInParent<Pickup>().mass;
        }
        else if (triggerCollider.tag == "Player")
        {
            objectMass = triggerCollider.GetComponentInParent<PlayerController>().mass;
            isPlayer = true;
        }
        else if (triggerCollider.tag == "NPC")
        {
            objectMass = triggerCollider.GetComponentInParent<NPC>().mass;
        }

        if (objectMass > 0)
        {
            ManageCollision(triggerCollider.gameObject, objectMass, isPlayer);
        }
    }

    private void ManageCollision(GameObject collidedObject, float objectMass, bool isPlayer = false)
    {
        if (mass > objectMass)
        {
            mass += objectMass;
            if (collidedObject.tag == "NPCCollider")
            {
                Destroy(collidedObject.GetComponentInParent<NPC>().gameObject);
            } else
            {
                Destroy(collidedObject);
            }
            camera.orthographicSize = mass * 3;
            if (isPlayer)
            {
                camera.rect = new Rect(0, 0, 1, 1);
            }
        }
    }
}
