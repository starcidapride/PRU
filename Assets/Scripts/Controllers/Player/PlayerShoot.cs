using System;
using System.Collections;
using UnityEngine;

public class PlayerShoot : Singleton<PlayerShoot>
{
    [SerializeField]
    private Transform bullet;

    [SerializeField]
    private Transform bulletArea;

    private CapsuleCollider2D boxCollider;

    private bool blockInput;

    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        boxCollider = GetComponent<CapsuleCollider2D>();    
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
    private IEnumerator UpdateCoroutine()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log(boxCollider.size);

            var bulletObj = Instantiate(bullet, bulletArea);
            bulletObj.position = transform.position + new Vector3(!PlayerAnimation.Instance.backward ? boxCollider.size.x * 1.2f: -boxCollider.size.x * 1.2f , boxCollider.size.y * 7/12) * 128;

            blockInput = true;
            yield return new WaitForSeconds(0.25f);
            blockInput = false;
        }
    }

    private void Update()
    {
        if (blockInput) return;

        StartCoroutine(UpdateCoroutine());
    }

}