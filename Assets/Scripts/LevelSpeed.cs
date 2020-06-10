using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpeed : MonoBehaviour
{
    [SerializeField] private float levelSpeed = 5f;

    public float getLevelSpeed()
    {
        return levelSpeed;
    }
}
