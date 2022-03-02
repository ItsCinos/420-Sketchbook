using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringTest : MonoBehaviour
{

    static float MAX_FORCE = 10;
    static float MAX_SPEED = 10;
    static List<SteeringTest> agents = new List<SteeringTest>();

    //private Vector3 position;
    private Vector3 force;
    private Vector3 velocity;

    float mass;

    public SteeringTarget target;

    private Vector3 targetPos;

    private Vector3 offset;

    void Start()
    {
        //position = transform.position;
        mass = Random.Range(1, 10);

        agents.Add(this);

        offset = Random.onUnitSphere;
    }


    void Update()
    {
        offset = Quaternion.Euler(0, 0, 90 * Time.deltaTime) * offset;

        targetPos = target.transform.position + offset;
        DoSteeringForce();
        DoEuler();
    }


    void DoSteeringForce(){
        // find desired velocity
        // desired velocity = clamp(target position - current position)

        Vector3 desiredVelocity = targetPos - transform.position;
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
        transform.position += velocity * Time.deltaTime;
        force *= 0;
    }
}
