using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brendan
{
    public class WaypointAI : MonoBehaviour
    {
        List<Node> waypoints;
        Rigidbody currentRigidbody;

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

                    Vector3 direction = (waypoints[index].worldPosition - currentRigidbody.position).normalized;
                    currentRigidbody.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
                }
            }
        }

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
    }
}