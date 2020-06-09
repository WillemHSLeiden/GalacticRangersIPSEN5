

using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject chargedLaserPrefab;
    [SerializeField] private KeyCode fire;
    [SerializeField] private int initiateChargedLaserTime;
    [SerializeField] private Transform controller;
    [SerializeField] private string lockOnTag = "Enemy";
    private float chargeTimer;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject targetReticle;

    void Update()
    {
        Vector3 relativePos = controller.position - transform.position;
        Quaternion aimDir = controller.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDir, 0.2f);

        var v3 = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0.0f) * 100 * Time.deltaTime;
        //controller.Rotate(v3 * 50 * Time.deltaTime);
        controller.eulerAngles = new Vector3(controller.eulerAngles.x + v3.x, controller.eulerAngles.y + v3.y, 0);
        RayCastLockOn();
        CountChargeTimer();
        Fire();
    }

    private void RayCastLockOn()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.CompareTag(lockOnTag))
            {
                Debug.DrawLine(transform.position, hit.transform.position);
                target = hit.collider.gameObject;
                targetReticle.GetComponent<LockOnReticle>().target = target.transform;
            }
        }
    }

    private void CountChargeTimer()
    {
        if (Input.GetKey(fire))
        {
            chargeTimer += Time.deltaTime;
        }
    }

    private void RestartChargedTimer()
    {
        chargeTimer = 0;
        Quaternion rotation = transform.rotation;
        target = null;
        transform.rotation = rotation;

    }

    private void ShootChargedLaser()
    {
        SpawnLaser(chargedLaserPrefab);
        RestartChargedTimer();
    }

    private void ShootLaser()
    {
        if ((Input.GetKeyUp(fire)) && (chargeTimer < initiateChargedLaserTime))
        {
            SpawnLaser(laserPrefab);
            RestartChargedTimer();
        }
    }

    private void SpawnLaser(GameObject _laserPrefab)
    {
        GameObject laser = Instantiate(_laserPrefab, transform.position, transform.localRotation);
        laser.GetComponent<LockOn>().target = target;
    }

    private void Fire()
    {
        if ((Input.GetKey(fire)) && (chargeTimer > initiateChargedLaserTime))
        {
            ShootChargedLaser();
            return;
        }
        ShootLaser();
    }

}