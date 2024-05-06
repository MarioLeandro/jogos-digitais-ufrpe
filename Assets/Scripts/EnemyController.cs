using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D _enemyRigidbody2D;
    private GameObject _oPlayer;
    public float _enemySpeed;
    public Vector2 _enemyDirection;
    public Animator _enemyAnimator;
    public bool _enemyFaceRight = true;
    [SerializeField] private float _attackDistance;
    public bool _isWalk;
    public bool _isAttack;
    [SerializeField] private float _timePunch = 2;
    public BoxCollider2D collider;
    public int enemyLevel = 1;


   /*  public bool _isWalk;
    private int _punchCount = 0;
    private float _timePunch = 0.75f;
    private bool _isAttack;  */


    // Start is called before the first frame update
    void Start()
    {
        _enemyRigidbody2D = GetComponent<Rigidbody2D>();
        _oPlayer = FindObjectOfType<PlayerController>().gameObject;
        _enemyAnimator = GetComponent<Animator>();
        collider.enabled = false;
        _enemySpeed = Random.Range(1.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        /* PlayerMove(); */
        FollowPlayer();
        UpdateAnimator();

        if(!_isWalk && !_isAttack) {
            _isAttack = true;
            StartCoroutine(PunchController());
            StartAttack();
            StopCoroutine(PunchController());
        }

        /* if(Input.GetKeyDown(KeyCode.X)) {
            if(!_isWalk) {
                StartCoroutine(PunchController());
                if(_punchCount < 2) {
                    PlayerJab();
                    _punchCount++;
                } else if(_punchCount >= 2){
                    PlayerPunch();
                    _punchCount = 0;
                }
                StopCoroutine(PunchController());
            }
        }

         if(Input.GetKeyDown(KeyCode.C) && !_isAttack) {
            if(!_isWalk) {
                _isAttack = true;
               PlayerKick();
            }
        } */
    }

    /* private void FixedUpdate()
    {
        if(_enemyDirection.x != 0 || _enemyDirection.y != 0) {
            _isWalk = true;
        } else {
            _isWalk = false;
        }
    
        _enemyRigidbody2D.MovePosition(_enemyRigidbody2D.position + _enemyDirection.normalized * _enemySpeed * Time.fixedDeltaTime);

    } */

    /* void PlayerMove()
    {
        _enemyDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(_enemyDirection.x < 0 && _playerFaceRight) {
            Flip();
        } else if(_enemyDirection.x > 0 && !_playerFaceRight) {
            Flip();
        }
    } */

     void UpdateAnimator()
    {
        _enemyAnimator.SetBool("isWalk", _isWalk);
    }

     void Flip()
    {
        _enemyFaceRight = !_enemyFaceRight;
        transform.Rotate(0f, 180, 0f);
    }

    /* void PlayerJab()
    {
        _playerAnimator.SetTrigger("isJab");
    }

    void PlayerPunch()
    {
        _playerAnimator.SetTrigger("isPunch");
    }


    void PlayerKick()
    {
        _playerAnimator.SetTrigger("isKick");
    } */

    IEnumerator PunchController() 
    {
        yield return new WaitForSeconds(_timePunch);
        EndAttack();
    }

    public void EndAttack()
    {
        _isAttack = false;
    }

    public void enableCollision() {
        collider.enabled = true;
    }

    public void disableCollision() {
        collider.enabled = false;
    }

    void FollowPlayer()
    {
        EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
        if(Vector2.Distance(transform.position, _oPlayer.transform.position) > _attackDistance && enemyHealth.health > 0) {
           _enemyDirection = (_oPlayer.transform.position - transform.position).normalized;
           _enemyRigidbody2D.velocity = _enemyDirection * _enemySpeed; 
           _isWalk = true;
           disableCollision();
        } else {
            _enemyRigidbody2D.velocity = Vector2.zero;
            _isWalk = false;
        }


        if(_enemyDirection.x < 0 && _enemyFaceRight) {
            Flip();
        } else if(_enemyDirection.x > 0 && !_enemyFaceRight) {
            Flip();
        }
    }

    void StartAttack()
    {
        _enemyAnimator.SetTrigger("isPunch");
    }
}
