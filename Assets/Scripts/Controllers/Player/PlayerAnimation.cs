using System;
using UnityEngine;

public class PlayerAnimation : Singleton<PlayerAnimation>
{
    private Animator animator;

    public bool backward = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayMovingAnimation(float moveX, bool jump, bool crounch)
    {   
        if (moveX > 0f)
        {
            backward = false;
        }
        if (moveX < 0f)
        {
            backward = true;
        }

        float _value;
        if (!jump && !crounch)
        {
            _value = 0f;
        }
        else if (jump)
        {
            _value = 1f;
        }
        else if (crounch)
        {
            _value = -1f;
        } else
        {
            _value = 0f;
        }


        animator.SetFloat("Speed", Mathf.Abs(moveX));
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("_Horizontal", backward ? -1 : 1);
        animator.SetFloat("Vertical", _value);
    }

    public void Update()
    {
        animator.SetBool("IsBackward", backward);
    }
}