using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlledCannon : MonoBehaviour
{

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private Quaternion shootingAngle;

    [SerializeField] private float smoothing = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Shoot(Target target)
    {
        shootingAngle.eulerAngles = new Vector3(0f, 0f, 0f);

    // Todo spawn bullets more often over time
    // Will now operate in a big while loop that is running whilst there are more than 1 player left

        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.transform.position, shootingAngle);

        StartCoroutine(FlyTowardsTarget(target, bulletInstance));
        
    }

    private IEnumerator FlyTowardsTarget(Target target, GameObject bulletInstance)
    {

        // Does not work at the moment
        while(Vector3.Distance(bulletInstance.transform.position, target.targetCenter.position) > 0.05f)
        {
            // Moves the cannonbullet towards the target
            bulletInstance.transform.position = Vector3.Lerp(this.transform.position, target.targetCenter.position, smoothing * Time.deltaTime); 
            yield return null;
        }
        
        target.targetPlatform.SetActive(true);
    }


}
