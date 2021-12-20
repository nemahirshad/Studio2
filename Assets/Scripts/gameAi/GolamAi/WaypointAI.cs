using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brendan
{
    public class WaypointAI : MonoBehaviour
    {
        List<Node> waypoints;
        Rigidbody currentRigidbody;
        Quaternion rotationGoal;
        public float TurnSpeed;

        float speed;
        float minimumDistanceToTarget;

        int index;

        public bool TargetReached { get; private set; }

        bool start = false;

        void Update()
        {
            if (start)
            {
                if (!TargetReached)
                {
                    float distance = Vector3.Distance(currentRigidbody.position, waypoints[index].worldPosition);
                    // Checks if we've reached the target node within the node list or not.
                    if (distance < minimumDistanceToTarget)
                    {
                        index++;

                        if (index >= waypoints.Count)
                        {
                            TargetReached = true;
                            start = false;
                            return;
                        }
                    }
                    // Calculates direction towards the waypoint we are moving towards.
                    Vector3 direction = (waypoints[index].worldPosition - currentRigidbody.position).normalized;
                    currentRigidbody.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
                    // calls rotate function to rotate the object.
                    Rotate(direction);
                }
            }
        }
        // initiates movement by taking the correct parameteres.
        public void MoveTowards(List<Node> waypoints, Rigidbody currentRigidbody, float speed, float minimumDistanceToTarget)
        {
            index = 0;
            TargetReached = false;
            start = true;
            this.waypoints = waypoints;
            this.currentRigidbody = currentRigidbody;
            this.speed = speed;
            this.minimumDistanceToTarget = minimumDistanceToTarget;
        }

        // takes a direction to rotate towards,
        private void Rotate(Vector3 dir)
        {
            rotationGoal = Quaternion.LookRotation(dir);
            //using slerp to smoothout the rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, TurnSpeed);
        }
    }
}