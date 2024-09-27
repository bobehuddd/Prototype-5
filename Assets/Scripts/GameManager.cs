using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Daftar objek target yang akan diinstantiasi
    public List<GameObject> targets;
    // Teks untuk menampilkan skor
    public TextMeshProUGUI scoreText;
    // Teks untuk menampilkan pesan game over
    public TextMeshProUGUI gameOverText;
    // Variabel untuk menentukan apakah game sedang aktif
    public bool isGameActive;
    // Tombol untuk restart game
    public Button restartButton;
    // Variabel untuk menyimpan skor
    private int score;
    // Waktu spawn target
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Mulai coroutine untuk spawn target
        StartCoroutine(SpawnTarget());
        // Reset skor
        score = 0;
        // Update skor
        UpdateScore(0);
        // Aktifkan game
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Coroutine untuk spawn target
    IEnumerator SpawnTarget() {
        // Selama game masih aktif
        while (isGameActive) {
            // Tunggu beberapa detik sebelum spawn target
            yield return new WaitForSeconds(spawnRate);

            // Pilih target secara acak dari daftar target
            int index = Random.Range(0, targets.Count);

            // Instantiasi target
            Instantiate(targets[index]);
        }
    }

    // Fungsi untuk update skor
    public void UpdateScore(int scoreToAdd) {
        // Tambahkan skor
        score += scoreToAdd;

        // Update teks skor
        scoreText.text = "Score: " + score;
    }

    // Fungsi untuk mengakhiri game
    public void GameOver() {
        // Aktifkan teks game over
        gameOverText.gameObject.SetActive(true);

        // Nonaktifkan game
        isGameActive = false;

        // Aktifkan tombol restart
        restartButton.gameObject.SetActive(true);
    }

    // Fungsi untuk restart game
    public void RestartGame() {
        // Muat ulang scene yang sedang aktif
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
