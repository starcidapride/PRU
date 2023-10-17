using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    private Rigidbody2D bulletRigidbody;

    private IEnumerator Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();

        bulletRigidbody.velocity = new Vector3(!PlayerAnimation.Instance.backward ? bulletSpeed : -bulletSpeed, 0);
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}