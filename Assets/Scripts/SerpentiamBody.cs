using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentiamBody : MonoBehaviour {
    public List<Transform> bodyParts = new List<Transform>();

    [SerializeField]
    private float minDist = 0.25f, speed = 1, rotationSpeed = 2;

    [SerializeField]
    private int startingSize;

    public GameObject bodyPrefab;

    public Boss boss;

    private bool attacking = false;

    private float dist;

    private Transform currBodyPart, prevBodyPart;
    public Transform playerPos;

    public GameObject projectile;

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

        newPart.SetParent(transform);

        bodyParts.Add(newPart);
    }

    public void Fire(float timeStamp) {
        StartCoroutine(fireTimer(timeStamp));
    }

    IEnumerator fireTimer(float timeStamp) {
        yield return new WaitForSeconds(timeStamp);
        boss.boss.speed = 2;

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

        rotationSpeed = 50;
        boss.boss.speed = 5;

        for (int i = 1; i < bodyParts.Count; i++) {
            bodyParts[i].GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }
}