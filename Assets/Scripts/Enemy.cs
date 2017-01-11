﻿using System;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;

    [HideInInspector]
	public float speed;
    public float health = 100;
    public int moneyReward = 10;

    public GameObject deathEffect;

    private void Start()
    {
        speed = startSpeed;
    }


    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1 - amount);
    }

    void Die()
    {
        PlayerStats.Money += moneyReward;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }
}
