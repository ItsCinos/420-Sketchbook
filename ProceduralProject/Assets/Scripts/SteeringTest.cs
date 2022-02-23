using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringTest : MonoBehaviour
{

    static float MAX_FORCE = 10;
    static float MAX_SPEED = 10;
    static List<SteeringTest> agents = new List<SteeringTest>();

    Vector3 position;
    Vector3 force;
    Vector3 velocity;

    float mass;

    public SteeringTarget target;

    Vector3 targetPos;

    void Start()
    {
        targetPos = target.transform.position;
        position = transform.position;
        mass = Random.Range(10, 100);

        agents.Add(this);
    }


    void Update()
    {
        
        DoSteeringForce();
        DoEuler();
    }


    void DoSteeringForce(){
        // find desired velocity
        // desired velocity = clamp(target position - current position)

        Vector3 desiredVelocity = targetPos - position;
        //desiredVelocity.sqrMagnitude(MAX_SPEED);

        // find steering force
        // steering force = desired velocity - current velocity

        Vector3 steeringForce = desiredVelocity - velocity;
        //steeringForce.limit(MAX_FORCE);

        force = force + steeringForce;
    }
    private void DoEuler()
    {
        Vector3 acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        this.position += velocity * Time.deltaTime;
        force *= 0;
    }
}
