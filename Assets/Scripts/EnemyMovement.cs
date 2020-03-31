using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {
    private Transform target;
    private int waypointIndex = 0;
    private Enemy enemy;

    void Start() {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    void Update() {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.55f) {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void EndPath() {
        PlayerStats.Life--;
        Destroy(gameObject);
    }

    void GetNextWayPoint() {
        if (waypointIndex >= Waypoints.points.Length - 1) {
            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }
}