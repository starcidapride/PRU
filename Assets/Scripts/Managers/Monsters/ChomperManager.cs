using System.Collections;
using UnityEngine;

public class ChomperManager : MonoBehaviour
{
    [SerializeField]
    private float chomperSpeed;

    [SerializeField]
    private float attackRanged;

    [SerializeField]
    private float delay;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider;

    private bool isBackward;
    private int Health { get; set; } = 2;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip audioClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private bool isDelay = false;
    private void Update()
    {   
        var playerLocation = PlayerManager.Instance.transform.position;
        var chomperLocation = transform.position;

        var distance = playerLocation - chomperLocation;


        if (Mathf.Sqrt(distance.sqrMagnitude) < attackRanged)
        {
            if (PlayerManager.Instance.Health > 0)
            {
                StartCoroutine(AttackCoroutine());

            }
              
        }
        else
        {
            _animator.SetBool("Attack", false);
        }

        var direction = distance.x > 0; 
        
        var moveX = direction ? 1f : -1f;

        isBackward = !direction;

        _animator.SetFloat("Speed", Mathf.Abs(moveX));
        _animator.SetFloat("Horizontal", moveX);
        _animator.SetBool("IsBackward", isBackward);

        _rigidbody.velocity = new Vector2(moveX * chomperSpeed, _rigidbody.velocity.y);
    }

    private IEnumerator AttackCoroutine()
    {
        if (!isDelay)
        {
            isDelay = true;
            _animator.SetTrigger("Attack");
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.75f);
            PlayerManager.Instance.Injured();

            yield return new WaitForSeconds(delay);
            isDelay = false;
        }
    }

    public void Injured()
    {
        StartCoroutine(InjuredCoroutine());
    }

    public IEnumerator InjuredCoroutine()
    {
        if (Health > 1)
        {
            Health -= 1;
        } else 
        {
            _audioSource.clip = audioClip; _audioSource.Play();

            gameObject.layer = LayerMask.NameToLayer("No Collision");

            _animator.SetTrigger("Die");
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f);

            ScoreManager.Instance.Increase(2);
            Destroy(gameObject);
        }
    }
}