using UnityEngine;

public class BulletScript : MonoBehaviour {
    [SerializeField]
    private float speed = 2;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
