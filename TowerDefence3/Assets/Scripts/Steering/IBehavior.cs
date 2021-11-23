using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Steering
{
    public interface IBehavior
    {
        /// <summary>
        /// Allow the behavior to initialize.
        /// </summary>
        /// /// <param name="context">All the context information needed to perform the task at hand.</param>
        void Start(BehaviorContext context);

        /// <summary>
        ///Calculate the steering contributed by thi behavior
        ///</summary>
        ///<param name="=dt">The Delta time for this step./// </param>
        ///<param name="context">all the context information needed to perform the task at hand.</param>
        Vector3 CalculateSteeringForce(float dt, BehaviorContext context);

        ///<summary>
        ///Draw the gizmos for the behavior
        ///</summary>
        /// <param name="context">All the context information needed to perform the task at hand.</param>
        void OnDrawGizmos(BehaviorContext context);
    }
}