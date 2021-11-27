using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject specialFruit;
    [SerializeField] private GameObject standardFruit;

    [SerializeField] private BoxCollider limits;
    private float xMinBound;
    private float xMaxBound;
    private float zMinBound;
    private float zMaxBound;



    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private int scoreStandard, scoreSpecial;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        Bounds();
        SpawnFruit();
        StartCoroutine("SpawnSpecialFruit");
    }

    private void Update()
    {
        Bounds();
        scoreText.text = "Points: " + score;
    }

    /// <summary>
    /// Salva i limiti del campo
    /// </summary>
    private void Bounds()
    {
        Bounds bound = limits.bounds;
        xMinBound = bound.min.x;
        xMaxBound = bound.max.x;
        zMinBound = bound.min.z;
        zMaxBound = bound.max.z;
    }

    /// <summary>
    /// Instanzia la posizione della frutta in punti casuali del campo
    /// </summary>
    private void SpawnFruit()
    {

        Vector3 spawnStandardPos = new Vector3(Random.Range(xMinBound, xMaxBound), transform.position.y, Random.Range(zMinBound, zMaxBound));
        Instantiate(standardFruit, spawnStandardPos, standardFruit.transform.rotation);
    }

    /// <summary>
    /// Instanzia la posizione della frutta speciale ogni 15 secondi in punti casuali del campo
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnSpecialFruit()
    {
        yield return new WaitForSeconds(15f);

        Vector3 spawnStandardPos = new Vector3(Random.Range(xMinBound, xMaxBound), transform.position.y, Random.Range(zMinBound, zMaxBound));
        Instantiate(specialFruit, spawnStandardPos, specialFruit.transform.rotation);

        StartCoroutine("DestroySpecialFruit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            GameObject fruitToDestroy = GameObject.FindGameObjectWithTag("Fruit");
            score += scoreStandard;
            Destroy(fruitToDestroy);

            SpawnFruit();
        }

        if (other.CompareTag("SpecialFruit"))
        {
            GameObject fruitToDestroy = GameObject.FindGameObjectWithTag("SpecialFruit");
            score += scoreSpecial;

            StopCoroutine("DestroySpecialFruit");
            StartCoroutine("SpawnSpecialFruit");

            Destroy(fruitToDestroy);            
        }
    }

    /// <summary>
    /// Distrugge il frutto speciale se non viene raccolto dopo 5 secondi
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroySpecialFruit()
    {
        yield return new WaitForSeconds(5f);
        GameObject fruitToDestroy = GameObject.FindGameObjectWithTag("SpecialFruit");

        StopCoroutine("SpawnSpecialFruit");
        StartCoroutine("SpawnSpecialFruit");

        Destroy(fruitToDestroy);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Body"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }
}
