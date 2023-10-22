using System.Collections;
using UnityEngine;

public class GunnerProjectileManager : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float destroyTime;
    private IEnumerator Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        var player = PlayerManager.Instance.transform.position + new Vector3(0, 150, 0);
        var direction = (player - transform.position).normalized;

        _rigidbody.velocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.name == "Player")
        {
            _rigidbody.velocity = Vector3.zero;
            PlayerManager.Instance.Injured();
            Destroy(gameObject);
        }
    }
}