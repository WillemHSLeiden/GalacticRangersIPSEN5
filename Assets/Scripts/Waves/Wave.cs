using PathCreation;
using UnityEngine;
[System.Serializable]
    public class Wave
    {
        // Zo kunnen we BV kenbaar maken dat er een boss aankomt.
        public string waveName;
        public Enemy[] enemies;
        //De rate waarop enemies spawnen
        public float rate;
        public PathCreator pathCreator;

        public float timeTillWaveStarts = 5f;

        public Transform spawnPoint; 
    }