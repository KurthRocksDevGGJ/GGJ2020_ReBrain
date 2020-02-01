using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour {
    [Header("Player Preferences")]
    [SerializeField]
    private float _playerSpeed = 3F;
    [SerializeField]
    private float _playerJumpPower = 5F;
    [SerializeField]
    private int _playerHealth = 3;
    [SerializeField]
    private bool _canDoubleJump = true;
    // [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private bool _isGrounded = true; // TODO: IsGrounded Check...
    //[SerializeField]
    private Animator _playerAnimator = null;
    [SerializeField]
    private LayerMask _groundLayers;
    [SerializeField]
    private bool _manualMovement = false;
    [SerializeField]
    private float _pushBackValue = 5F;

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

    // TODO: Not showing on editor!!
    [Header("TEMP: Specific Player Controls")]
    [SerializeField]
    private KeyCode _jumpCode = KeyCode.Space;
    [SerializeField]
    private KeyCode _attackCode = KeyCode.LeftControl;

    [Header("Debug Stuff")]
    //[SerializeField]
    private MainPlayerFeet _playerFeetCollisionBox = null;
    //[SerializeField]
    private MainPlayerBody _playerBodyCollisionBox = null;
    [SerializeField]
    private bool _isJumpPressed = false;
    [SerializeField]
    private bool _isAttackPressed = false;
    [SerializeField]
    private bool _isHittingWall = false;
    [SerializeField]
    private bool _invincible = false;

    // Start is called before the first frame update
    void Start() {
        // TODO: NUll check errors!!
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerAudioSource = GetComponent<AudioSource>();

        _playerBodyCollisionBox = GetComponentInChildren<MainPlayerBody>();
        _playerFeetCollisionBox = GetComponentInChildren<MainPlayerFeet>();
        //Debug.Log(_playerBodyCollisionBox.isHittingWall());

        UIManager.Instance.UpdatePlayerLives(_playerHealth);
    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();

        _isJumpPressed = Input.GetKeyDown(_jumpCode);
        _isAttackPressed = Input.GetKeyDown(_attackCode);
        _isHittingWall = _playerBodyCollisionBox.isHittingWall();
    }

    private void FixedUpdate() {
        // CalculateMovement();

        if (_isJumpPressed)
            Jump();
        if (_isAttackPressed)
            Attack();

        if(_isHittingWall) {
            _isHittingWall = false;
            //Debug.Log("Player hitting wall...");
            _rigidbody2D.AddForce(Vector2.left * _pushBackValue, ForceMode2D.Impulse);
            Damage();

            //_isHittingWall = false;
        }
    }

    // Do we need some boundaries??
    private void CalculateMovement() {
        // Movement for the player
        // Auto-Run mode!
        if(_manualMovement) {
            //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            //transform.Translate(direction * _playerSpeed * Time.deltaTime);
            //_rigidbody2D.AddForce(direction * _playerSpeed, ForceMode2D.Impulse);
            //_rigidbody2D.velocity = (direction * _playerSpeed);

            _rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * _playerSpeed, _rigidbody2D.velocity.y);
            if (DebugManager.Instance) {
                DebugManager.Instance.SetPlayerPosition(transform.position);
                DebugManager.Instance.SetCurrentDebugTestText("" + Input.GetAxis("Horizontal") + " * " + _playerSpeed + ", " + _rigidbody2D.velocity.y);
            }
        } else {
            //transform.Translate(Vector2.right * _playerSpeed * Time.deltaTime);
            //_rigidbody2D.AddForce(Vector2.right * _playerSpeed, ForceMode2D.Impulse);
            //_rigidbody2D.velocity = (Vector2.right   * _playerSpeed);

            //_rigidbody2D.velocity = new Vector2(7F, 0F);
            //_rigidbody2D.velocity = new Vector2(_playerSpeed, 0);

            _rigidbody2D.velocity = (Vector2.right * _playerSpeed);
            if (DebugManager.Instance) {
                DebugManager.Instance.SetPlayerPosition(transform.position);
                DebugManager.Instance.SetCurrentDebugTestText("" + Vector2.right + " * " + _playerSpeed);
            }
        }

        // Reset player
        if (this.transform.position.y < -30F) {
            _rigidbody2D.velocity = Vector3.zero;
            this.transform.position = new Vector3(0, 5, 0);
            Damage();
        }
    }

    private void Jump() {
        // _isGrounded
        if (_playerFeetCollisionBox.isGrounded()) {
            _canDoubleJump = true;
            //_rigidbody2D.AddForce(new Vector2(0, 250));
            _rigidbody2D.AddForce(Vector2.up * _playerJumpPower, ForceMode2D.Impulse);
            //_playerAnimator.SetBool("jumping", true);
            _playerAnimator.SetTrigger("OnJump");
            _playerAudioSource.clip = _soundClipJump;
            _playerAudioSource.Play();
        } else {
            if (_canDoubleJump) {
                _canDoubleJump = false;
                //_rigidbody2D.AddForce(new Vector2(0, 250));
                _rigidbody2D.AddForce(Vector2.up * _playerJumpPower, ForceMode2D.Impulse);
                //_playerAnimator.SetBool("jumping", true);
                _playerAnimator.SetTrigger("OnJump");
                _playerAudioSource.clip = _soundClipJump;
                _playerAudioSource.Play();
            }
        }
    }

    public void IsGrounded(bool _isGrounded) {
        this._isGrounded = _isGrounded;

        if (_isGrounded) {
            _playerAudioSource.clip = _soundClipLanding;
            _playerAudioSource.Play();
        }
    }

    private void Damage() {
        _playerHealth -= 1;
        if (_playerHealth <= 0)
            OnGameOver();

        UIManager.Instance.UpdatePlayerLives(_playerHealth);
    }

    private void Attack() {
        // _playerAnimator.SetBool("attacking", true);
        _playerAnimator.SetTrigger("OnAttack");
        _playerAudioSource.clip = _soundClipAttack;
        _playerAudioSource.Play();
    }

    private void OnGameOver() {
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence() {
        _playerAnimator.SetBool("gameover", true);
        yield return new WaitForSeconds(2F);
        if(!_invincible)
            Destroy(this.gameObject);
    }

    /// <summary>
    /// Play collectible sound when collected
    /// </summary>
    public void CollectedCoin() {
        Debug.Log("Collected coin");
        _playerAudioSource.clip = _soundClipPowerUp;
        _playerAudioSource.Play();
    }
}