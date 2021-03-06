using UnityEngine;

/*
 * PlayerProfiler
 * Script to control movement and animations of player.
 */
namespace Player
{
    public class PlayerProfiler : MonoBehaviour
    {
        private Vector3 _direction;
        public float highProfile = 5f;
        public float zProfile = 1f;
        public CharacterController controller;
        public Transform model;

        public float jumpForce = 5;
        public float gravity = -20;
        public Transform groundCheck;
        public LayerMask groundLayer;

        private Animator _animator;
        private int _profile, _profileZ, _grounded;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _profile = Animator.StringToHash("profile");
            _profileZ = Animator.StringToHash("profileZ");
            _grounded = Animator.StringToHash("grounded");
        }
        
        private void Update()
        {
            Movement();
            JumpActive();
        }

        private void Movement()
        {
            // Move left & right.
            var hInput = Input.GetAxisRaw("Horizontal");
            _direction.x = hInput * highProfile;
            _animator.SetFloat(_profile, Mathf.Abs(hInput));

            // Move forward & back.
            var vInput = Input.GetAxisRaw("Vertical");
            _direction.z = vInput * zProfile;
            _animator.SetBool(_profileZ, vInput != 0);

            // Move Character.
            controller.Move(_direction * Time.deltaTime);

            // Switch Look rotation.
            if (hInput != 0)
            {
                var newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, 0));
                model.rotation = newRotation;
            }

            // Face camera when not moving.
            if (hInput == 0) model.rotation = Quaternion.LookRotation(Vector3.back);
        }
 
        private void JumpActive()
        {
            // Jump and Animate only if grounded.
            var grounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
            _direction.y += gravity * Time.deltaTime;
            _animator.SetBool(_grounded, grounded);
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                _direction.y = jumpForce;
            }
        }
    }
}