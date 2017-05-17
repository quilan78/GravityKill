﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if(health != null)
        {
            health.takeDamage(100); // one shot mdrrrrrr faudrait créer une classe d'arme
        }
        Destroy(gameObject);
    }
}
