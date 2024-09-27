using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        // Mendapatkan komponen Rigidbody dari objek ini
        targetRb = GetComponent<Rigidbody>();
        // Menambahkan gaya acak ke objek ini dengan mode Impulse (gaya tiba-tiba)
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        // Menambahkan torsi acak ke objek ini dengan mode Impulse (gaya tiba-tiba)
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        // Mengatur posisi objek ini ke posisi acak
        transform.position = RandomSpawnPos();
        // Mencari objek "Game Manager" dan mendapatkan komponen GameManager-nya
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        // Jika game sedang aktif
        if (gameManager.isGameActive) {
            Destroy(gameObject);
            // Buat efek ledakan di posisi objek ini
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            // Update skor game
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        // Jika objek ini bukan objek "Bad"
        if (!gameObject.CompareTag("Bad")) {
            // Game Over
            gameManager.GameOver();
        }
    }

    // Fungsi untuk mengembalikan gaya acak
    Vector3 RandomForce() {
        // Kembalikan gaya acak yang arahnya ke atas dengan magnitude antara minSpeed dan maxSpeed
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    // Fungsi untuk mengembalikan torsi acak
    float RandomTorque() {
         // Kembalikan torsi acak antara -maxTorque dan maxTorque
        return Random.Range(-maxTorque, maxTorque);
    }
    // Fungsi untuk mengembalikan posisi spawn acak
    Vector3 RandomSpawnPos() {
         // Kembalikan posisi spawn acak dengan x antara -xRange dan xRange, dan y tetap di ySpawnPos
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
