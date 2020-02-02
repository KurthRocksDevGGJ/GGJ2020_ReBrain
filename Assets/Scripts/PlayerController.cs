using UnityEngine;
using UnityEngine.Events;

namespace Photon.Pun.Demo.PunBasics {
    public class PlayerController : MonoBehaviourPunCallbacks {
        [Header("Player Preferences")]
        [SerializeField]
        private int _playerLives = 3;
        [SerializeField]
        private int _playerCoins = 0;
        [SerializeField]
        private int _hammerUsage = 0;
        [SerializeField]
        private bool _canUseRewind = true;

        [Header("Debug")]
        [SerializeField]
        private TimeBody _timeBody = null;
        //if (_canUseRewind) _timeBody.StartRewindTillEnd();
        [SerializeField]
        private Vector2 _startPosition = Vector2.zero;

        [Header("Player SFX Settings")]
        //[SerializeField]
        private AudioSource _playerAudioSource = null;
        [SerializeField]
        private AudioClip _soundClipJump = null;
        [SerializeField]
        private AudioClip _soundClipLanding = null;
        [SerializeField]
        private AudioClip _soundClipAttack = null;
        [SerializeField]
        private AudioClip _soundClipPowerUp = null;

        [Header("Other")]
        [SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
        [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
        [SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
        [SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
        [SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
        [SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private Vector3 m_Velocity = Vector3.zero;

        [Header("Events")]
        [Space]

        public UnityEvent OnLandEvent;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        public BoolEvent OnCrouchEvent;
        private bool m_wasCrouching = false;

        private void Awake() {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();

            if (OnCrouchEvent == null)
                OnCrouchEvent = new BoolEvent();
        }

        private void Start() {
            if (!UIManager.Instance)
                Debug.Log("Player::Start(): Could not find UIManager instance.");
            else {
                UIManager.Instance.UpdatePlayerCoins(_playerCoins);
                UIManager.Instance.UpdatePlayerLives(_playerLives);
                //UIManager.Instance.UpdateHammerUsage(_hammerUsage);
                UIManager.Instance.UpdateTravelDistance(0);
            }

            _timeBody = GetComponent<TimeBody>();
            if (_timeBody == null)
                Debug.LogWarning("Player::Start(): timebody not assigned.");

            _startPosition = transform.position;

            _playerAudioSource = GetComponent<AudioSource>();
        }

        private void FixedUpdate() {
            bool wasGrounded = m_Grounded;
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].gameObject != gameObject) {
                    m_Grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }

            UIManager.Instance.UpdateTravelDistance((int) (transform.position.x - _startPosition.x));
        }

        public int GetCoins()
        {
            //Debug.Log("Aktuelle Coins: " +_playerCoins);
            return _playerCoins;
        }
        public void RemoveCoins(int i)
        {
           // Debug.Log("RM Aktuelle Coins: " + _playerCoins);
           // Debug.Log("RM abgezogen werden: " + i);
            if (i >=_playerCoins )
            {
                _playerCoins = 0;                
            }
            else
            {                
                _playerCoins -= i;                
            }
            UIManager.Instance.UpdatePlayerCoins(_playerCoins);
        }
        public void Move(float move, bool crouch, bool jump) {
            // If crouching, check to see if the character can stand up
            if (!crouch) {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) {
                    crouch = true;
                }
            }

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl) {

                // If crouching
                if (crouch) {
                    if (!m_wasCrouching) {
                        m_wasCrouching = true;
                        OnCrouchEvent.Invoke(true);
                    }

                    // Reduce the speed by the crouchSpeed multiplier
                    move *= m_CrouchSpeed;

                    // Disable one of the colliders when crouching
                    if (m_CrouchDisableCollider != null)
                        m_CrouchDisableCollider.enabled = false;
                } else {
                    // Enable the collider when not crouching
                    if (m_CrouchDisableCollider != null)
                        m_CrouchDisableCollider.enabled = true;

                    if (m_wasCrouching) {
                        m_wasCrouching = false;
                        OnCrouchEvent.Invoke(false);
                    }
                }

                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight) {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight) {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump) {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                
                if (_playerAudioSource != null && _soundClipJump != null) {
                    _playerAudioSource.volume = 1;
                    _playerAudioSource.clip = _soundClipJump;
                    _playerAudioSource.Play();
                }
            }
        }

        private void Flip() {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void Damage() {
            _playerLives -= 1;
            if (_playerLives <= 0) {
                Debug.LogError("Player::Damager(): Lives from player reached zero. Player is dead now.");
                if (UIManager.Instance)
                    UIManager.Instance.ShowGameOvahPanel();
            }
        }

        private void OnTriggerEnter2D(Collider2D _other) {
            if (_other.gameObject.CompareTag("Collectibles")) {
                if (_other.gameObject.name.Contains("Diamond")) {
                    Debug.Log($"Player::Found coin '{_other.gameObject.name}'");

                    if (_playerAudioSource != null && _soundClipPowerUp != null) {
                        _playerAudioSource.volume = .3F;
                        _playerAudioSource.clip = _soundClipPowerUp;
                        _playerAudioSource.Play();
                    }

                    Collectible coinCollectible = _other.gameObject.GetComponent<Collectible>();
                    if (coinCollectible != null) {
                        _playerCoins += coinCollectible.GetCoinValue();
                        
                        /*
                        if(_playerCoins >= 30) {
                            _hammerUsage += 1;
                            _playerCoins -= 30;
                            UIManager.Instance.UpdateHammerUsage(_hammerUsage);
                        }
                        */

                        UIManager.Instance.UpdatePlayerCoins(_playerCoins);
                    }

                    Destroy(_other.gameObject);
                }
            }
        }
    }
}