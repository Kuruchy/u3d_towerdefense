using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    public Transform enemyPrefab;
    public float timeBetweenWaves = 5.5f;
    public Transform spawnPoint;
    public float waitForSeconds = 0.2f;

    public Text waveCountdownText;

    private float countdown = 2f;
    private int waveIndex = 0;

    void Update() {
        if (countdown <= 0f) {
            /* to call an IEnumerator pass the function as a parameter
            of StartCoroutine() */

            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }


    IEnumerator SpawnWave() {
        waveIndex++;
        PlayerStats.Rounds++;

        for (int i = 0; i < waveIndex; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(waitForSeconds);
        }
    }

    void SpawnEnemy() {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}