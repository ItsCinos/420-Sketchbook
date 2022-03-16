using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BoidType
{
    Lil,
    Big,
    ReallyBig,
}

[RequireComponent(typeof(Rigidbody))]
public class Boid : MonoBehaviour
{

    public BoidType type;

    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        BoidManager.AddBoid(this);
    }
    private void OnDestroy()
    {
        BoidManager.RemoveBoid(this);
    }

    void LateUpdate()
    {
        Vector3 dir = body.velocity;
        dir.Normalize();

        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void CalcForces(Boid[] boids)
    {

        BoidSettings settings = BoidManager.GetSettings(type);

        // Alignment
        // Cohesion
        // Separation

        Vector3 avgCenter = Vector3.zero;
        int countCohesion = 0;

        foreach(Boid b in boids)
        {
            if (b == this) continue; // don't update if they are the same boid
            
            Vector3 vToOther = b.transform.position - transform.position;
            float d = vToOther.magnitude;

            if(d < settings.radiusAlignment)
            {

            }
            if (d < settings.radiusCohesion)
            {
                avgCenter += b.transform.position;
                countCohesion++;
            }
            if (d < settings.radiusSeparation)
            {

                Vector3 separation = settings.forceSeparation * -vToOther/d/d * Time.deltaTime;

                body.AddForce(separation);
            }

        }

        // TODO: apply alignment steering force
        // TODO: apply cohesion steering force
        if (countCohesion > 0)
        {
            avgCenter /= countCohesion;

            Vector3 vToCenter = avgCenter - transform.position;
            Vector3 desiredVelocity = vToCenter.normalized * settings.maxSpeed;
            Vector3 forceCohesion = desiredVelocity - body.velocity;

            if (forceCohesion.sqrMagnitude > settings.maxForce * settings.maxForce)
            {
                forceCohesion = forceCohesion.normalized * settings.maxForce;
            }
            body.AddForce(forceCohesion * settings.forceCohesion * Time.deltaTime);


        }


    }

}
