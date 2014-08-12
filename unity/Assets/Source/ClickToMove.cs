using UnityEngine;

namespace Assets.Source
{
    public class ClickToMove : MonoBehaviour
    {

        public Vector3 Position;
        public float MoveSpeed;
        public float RotateSpeed;
        private CharacterController _controller;
        public AnimationClip Idle;
        public AnimationClip Run;
        private Player _player;

        public void Awake()
        {
            _controller = GetComponent<CharacterController>();
            Position = transform.position;
            _player = GetComponent<Player>();
        }

        public void Update () {

            if (_player.IsAttacking || _player.IsDying)
                return;

            if (Input.GetMouseButton(0))
            {
                LocatePosition();
            }

            MovePosition();
        }

        internal void LocatePosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.collider.tag != "Player")
                    Position = new Vector3(hit.point.x,hit.point.y,hit.point.z);
            }
        }

        internal void MovePosition()
        {
            if (Vector3.Distance(transform.position, Position) > 1.3)
            {
                var rot = Quaternion.LookRotation(Position - transform.position);
                rot.x = transform.rotation.x;
                rot.z = transform.rotation.z;
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime*RotateSpeed);
                _controller.SimpleMove(transform.forward*MoveSpeed);
                animation.CrossFade(Run.name);
            }
            else
            {
                animation.CrossFade(Idle.name);
            }
        }
    }
}
