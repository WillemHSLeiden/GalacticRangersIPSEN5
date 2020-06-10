using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentiamBody : MonoBehaviour {
    public List<Transform> bodyParts = new List<Transform>();

    [SerializeField]
    private float minDist = 0.25f, speed = 1, rotationSpeed = 2;

    [SerializeField]
    private int startingSize = 10, bodyHits = 0;

    public GameObject bodyPrefab;

    public Boss boss;

    private float dist;

    private Transform currBodyPart, prevBodyPart;
    public Transform playerPos;

    public GameObject projectile;

    public Material vulnerableMat, asteroidMat, hitFlashMat; 

    private void Start() {
        for(int i = 0; i < startingSize - 1; i++) {
            AddBodyPart();
            if (i > 0) {
                var _particleSystem = bodyParts[i].GetChild(1).gameObject.GetComponent<ParticleSystem>();
                var _main = _particleSystem.main;
                var _emission = _particleSystem.emission;
                _main.startSpeed = startingSize - i;
                _main.maxParticles = (startingSize - i) * 2;
                _emission.rateOverTime = (startingSize - i) * 1.5f;
            }
        }
    }

    void Update() {
        Move();
    }

    public void Move() {
        float currSpeed = speed;

        bodyParts[0].Translate(bodyParts[0].forward * currSpeed * Time.deltaTime, Space.World);

        for (int i = 1; i < bodyParts.Count; i++) {
            currBodyPart = bodyParts[i];
            prevBodyPart = bodyParts[i - 1];

            dist = Vector3.Distance(prevBodyPart.position, currBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            float T = Time.deltaTime * dist / minDist * currSpeed;

            if (T > 0.5f)
                T = 0.5f;

            currBodyPart.position = Vector3.Slerp(currBodyPart.position, newPos, T);
            currBodyPart.LookAt(prevBodyPart);
            float rotMultiplier = (bodyParts.Count - i + 1) / 2; 
            currBodyPart.GetChild(0).gameObject.transform.Rotate(Vector3.forward * rotationSpeed * rotMultiplier * Time.deltaTime);
        }

    }

    public void AddBodyPart() {
        Transform newPart = Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation).transform;
        float size = ((startingSize - bodyParts.Count) * 0.2f) + 1;
        newPart.localScale = new Vector3(size, size, size);

        newPart.SetParent(transform);
        newPart.GetChild(0).gameObject.GetComponent<SerpentiamBodypart>().serpentiamBody = this;

        bodyParts.Add(newPart);
    }

    public void AddBodyHit() {
        bodyHits++;
        checkVulnerability();
    }

    private void checkVulnerability() {

        if (bodyHits >= startingSize - 1) {
            boss.switchStateWrapper(Boss.BossState.VULNERABLE);
            StopAllCoroutines();
            stopFiring();

            changeBodyMaterial(vulnerableMat);

            changeBodypartsState(SerpentiamBodypart.BodypartState.VULNERABLE);

            bodyHits = 0;
            StartCoroutine(resetMesh());
        }
    }

    public void Fire(float timeStamp) {
        StartCoroutine(fireTimer(timeStamp));
    }

    private void stopFiring() {
        rotationSpeed = 50;
        boss.setSpeed(5);

        for (int i = 1; i < bodyParts.Count; i++) {
            bodyParts[i].GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }

    public void damage() {
        changeBodyMaterial(hitFlashMat);
        StartCoroutine(flashReset());
        boss.setHealth(boss.getHealth() - 1);
        checkBossHealth();
    }

    private void checkBossHealth() {
        if (boss.getHealth() <= 0) {
            boss.switchStateWrapper(Boss.BossState.DYING);
            StopAllCoroutines();
            StartCoroutine(killSerpentiam());
        }
    }

    private void changeBodyMaterial(Material mat) {
        for (int i = 1; i < bodyParts.Count; i++) {
            bodyParts[i].GetChild(0).gameObject.GetComponent<Renderer>().material = mat;
        }

        transform.GetChild(0).GetChild(1).gameObject.GetComponent<Renderer>().material = mat;
    }

    private void changeBodypartsState(SerpentiamBodypart.BodypartState state) {
        for (int i = 1; i < bodyParts.Count; i++) {
            bodyParts[i].GetChild(0).gameObject.GetComponent<SerpentiamBodypart>().state = state;
        }
    }


    IEnumerator fireTimer(float timeStamp) {
        yield return new WaitForSeconds(timeStamp);
        boss.setSpeed(2);

        for (int i = 1; i < bodyParts.Count; i++) {
            bodyParts[i].GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
        }

        for (int i = 0; i < bodyParts.Count - 1; i++) {
            rotationSpeed = Mathf.Lerp(rotationSpeed, 300, 0.1f);
        }

        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(1f);
            int index = Random.Range(0, bodyParts.Count);
            GameObject _projectile = Instantiate(projectile, bodyParts[index].position, bodyParts[index].rotation);
            _projectile.GetComponent<homingProjectile>().target = playerPos;
        }

        stopFiring();
    }

    IEnumerator resetMesh() {
        yield return new WaitForSeconds(boss.getVulnerabilityLength());
        changeBodyMaterial(asteroidMat);
        changeBodypartsState(SerpentiamBodypart.BodypartState.NEUTRAL);
    }
    IEnumerator flashReset() {
        yield return new WaitForSeconds(0.1f);
        changeBodyMaterial(vulnerableMat);
    }

    IEnumerator killSerpentiam() {
        yield return new WaitForSeconds(1f);
        for (int i = bodyParts.Count - 1; i > 0; i--) {
            yield return new WaitForSeconds(0.2f);
            bodyParts[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}