using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlledCannon : MonoBehaviour
{

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private Quaternion shootingAngle;

    [SerializeField] private float smoothing = 4f;
    public PlayerCannonController assignedPlayer;




    public void Shoot(Target target)
    {
        assignedPlayer.isShooting = true;
        shootingAngle.eulerAngles = new Vector3(0f, 0f, 0f);

    // Todo spawn bullets more often over time
    // Will now operate in a big while loop that is running whilst there are more than 1 player left

        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.transform.position, shootingAngle);

        StartCoroutine(FlyTowardsTarget(target, bulletInstance));
        
    }

    private IEnumerator FlyTowardsTarget(Target target, GameObject bulletInstance)
    {

        // Does not work at the moment
        while(Vector2.Distance(bulletInstance.transform.position, target.targetCenter.position) > 0.05f || Vector2.Distance(bulletInstance.transform.position, target.targetCenter.position) < -0.05f)
        {
            // Moves the cannonbullet towards the target
            // Whils the bullet moves towards the target its x and y scale should be smaller and smaller and upon reaching its target
            // the scale should be x = 0.6, y = 0.6 ~ as this matches the targetplatform sizes ca.

            // Todo smoothing should be based on distance. Bullets should always arrive in the same amount of time.
            bulletInstance.transform.position = Vector2.MoveTowards(bulletInstance.transform.position, target.targetCenter.position, smoothing * Time.deltaTime); 
            yield return null;
        }

        assignedPlayer.isShooting = false;
        Destroy(bulletInstance);

        target.targetPlatform.SetActive(true);
    }


}
