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
                    // probably here or occuring from the minimumdistancetotarget from the function below
                    Vector3 direction = (waypoints[index].worldPosition - currentRigidbody.position).normalized;
                    currentRigidbody.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
                    Rotate(direction);
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

        private void Rotate(Vector3 dir)
        {
           
           // TargetDir = new Vector3(0, TargetDir.y, 0);
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, 0.5f * Time.deltaTime, 0.0f);
            rotationGoal = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, TurnSpeed);
        }
    }
}