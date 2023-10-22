using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float destroyTime;

    [SerializeField]
    private Animator _animator;

    private Rigidbody2D _rigidbody;
    private IEnumerator Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var player = PlayerManager.Instance.transform.position;
        
        var direction = (mousePosition - new Vector3(
               PlayerManager.Instance.IsBackward ? -200 : +200,
              200,
              0) - player).normalized;

        _rigidbody.velocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.ContainsInsensitive("Chomper") || collision.name.ContainsInsensitive("Gunner"))
        {
            Debug.Log(collision.name);

            if (collision.name.ContainsInsensitive("Chomper"))
            {
                collision.gameObject.GetComponent<ChomperManager>().Injured();
            } else
            {
                collision.gameObject.GetComponent<GunnerManager>().Injured();
            }
            Destroy(gameObject);
        }
    }
}