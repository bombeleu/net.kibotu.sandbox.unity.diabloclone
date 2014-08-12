using UnityEngine;

namespace Assets.Source
{
    public class SmoothFollow : MonoBehaviour {

        // The target we are following
        public Transform target;
        // The distance in the x-z plane to the target
        public float distance = 10.0f;
        // the height we want the camera to be above the target
        public float height = 5.0f;
        // How much we 
        public float heightDamping = 2.0f;

        public void LateUpdate()
        {
            // Early out if we don't have a target
            if (!target)
                return;

            // Calculate the current rotation angles
            var wantedHeight = target.position.y + height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping*Time.deltaTime);
            
            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            var pos = target.position;
            pos -= Vector3.forward*distance;

            // Set the height of the camera
            pos.y = currentHeight;

            transform.position = pos;

            // Always look at the target
            transform.LookAt(target);
        }
    }
}