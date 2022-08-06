using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterGame
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform Target;
        public Vector3 FollowDistance;
        public float FollowSpeed;
        public Vector3 velocity = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            FollowDistance = Target.position - transform.position;
        }

        private void LateUpdate()
        {
            /*
                        Vector3 newPos = Target.position - FollowDistance;
                        transform.position = newPos;
            */

            Vector3 newPos = Target.position - FollowDistance;
            //transform.position = Vector3.MoveTowards(transform.position, newPos, FollowSpeed * Time.deltaTime);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, FollowSpeed);

            
        }
       
    }
}