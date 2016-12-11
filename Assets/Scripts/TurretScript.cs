using UnityEngine;

public class TurretScript : MonoBehaviour {
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float rateOfFire;

    private float timeSinceShot = 0;
    private void Update()
    {
        timeSinceShot += Time.deltaTime;
        if (timeSinceShot >= rateOfFire)
        {
            Instantiate(bulletPrefab, spawnPos.position, spawnPos.rotation);
            timeSinceShot = 0;
        }
    }
}
