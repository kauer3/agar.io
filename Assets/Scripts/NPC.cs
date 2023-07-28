using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public float mass = 1;
    public float speed = 0.2f;
    Vector3 velocity;
    public new Camera camera;
    public Camera[] cameras;
    float timer = 0;
    float waitTime = 0;


    // Make NPC follow other NPCs and players
    // Make NPC search for pickups

    void Update()
    {
        if (timer < waitTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Vector3 input = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Vector3 direction = input.normalized;
            velocity = direction * speed;
            timer = 0;
            waitTime = Random.Range(0, 10f);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= -50 && velocity.x < 0)
        {
            velocity = new Vector3(Random.Range(0, 1) * speed, velocity.y, 0);
        }
        else if (transform.position.x >= 50 && velocity.x > 0)
        {
            velocity = new Vector3(Random.Range(-1, 0) * speed, velocity.y, 0);
        }
        if (transform.position.y <= -50 && velocity.y < 0)
        {
            velocity = new Vector3(velocity.x, Random.Range(0, 1) * speed, 0);
        }
        else if (transform.position.y >= 50 && velocity.y > 0)
        {
            velocity = new Vector3(velocity.x, Random.Range(-1, 0) * speed, 0);
        }

        transform.Translate(velocity);
    }

    private void OnTriggerEnter(Collider triggerCollider)
    {
        float objectMass = 0;

        if (triggerCollider.tag == "Pickup")
        {
            objectMass = triggerCollider.GetComponentInParent<Pickup>().mass;
        }
        else if (triggerCollider.tag == "Player")
        {
            objectMass = triggerCollider.GetComponentInParent<PlayerController>().mass;
        }
        else if (triggerCollider.tag == "NPC")
        {
            objectMass = triggerCollider.GetComponentInParent<NPC>().mass;
        }

        if (objectMass > 0)
        {
            ManageCollision(triggerCollider.gameObject, objectMass, triggerCollider.tag);
        }
    }

    private void ManageCollision(GameObject collidedObject, float objectMass, string type)
    {
        if (mass > objectMass)
        {
            Destroy(collidedObject);
            mass += objectMass;
            camera.orthographicSize = mass * 2;
            transform.localScale = new Vector3(mass, mass, mass);

            if (type == "Player" || type == "NPC")
            {
                bool hasEnabledCamera = cameras.Any(cam =>
                {
                    if (cam != null && cam.enabled)
                    {
                        if (type == "Player")
                        {
                            cam.rect = new Rect(0, 0, 1, 1);
                        }
                        return true;
                    }
                    return false;
                });

                if (!hasEnabledCamera)
                {
                    camera.enabled = true;
                }
            }
        }
    }
}