using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAgent : MonoBehaviour
{
    public float maxSpeed = 2;
    public float maxForce = 2;
    public Perception perception;
    public Behavior behavior;

    public Vector3 Velocity { get; set; }
    public Vector3 Acceleration { get; set; }
    public Vector3 Direction { get { return Velocity.normalized; } }

    //public float speed = 1.0f;
    public float perceptionDistance = 4.0f;
    public float perceptionAngle = 30.0f;
    //public bool flee = false;

    void Update()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearestGameObject = GetNearestGameObject(gameObjects, out float nearestDistance);

        Acceleration = Vector3.zero;

        if (nearestDistance < perceptionDistance)
        {
            Debug.DrawLine(transform.position, nearestGameObject.transform.position);
            Debug.DrawRay(transform.position, transform.forward, Color.red);

            Vector3 direction = (nearestGameObject.transform.position - transform.position).normalized;

            //compute angle
            float dot = Dot(transform.forward, direction);
            dot = Mathf.Clamp(dot, -0.99f, 1);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if(angle <= perceptionAngle)
            {
                
                Vector3 force = behavior.Execute(gameObjects);
                Acceleration += force;

                //direction = (flee) ? -direction : direction;
                //if (flee)
                //{
                //    perceptionAngle = 360.0f;
                //}
                //Vector3 velocity = direction * speed;
                //transform.position += velocity * Time.deltaTime;
            }
        }
        Velocity += Acceleration * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, maxSpeed);
        transform.position += Velocity * Time.deltaTime;


        if (Direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(Direction);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3( -10, -10, -10), new Vector3(10, 10, 10));
    }

    float Dot(Vector3 v1, Vector3 v2)
    {
        return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
    }

    GameObject GetNearestGameObject(GameObject[] gameObjects, out float nearestDistance)
    {
        nearestDistance = float.MaxValue;

        GameObject nearestGameObject = null;
        foreach(GameObject gameObject in gameObjects)
        {
            if (gameObject == this.gameObject) continue;

            float distance = Vector3.Distance(transform.position, gameObject.transform.position);
            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestGameObject = gameObject;
            }
        }

        return nearestGameObject;
    }
}
