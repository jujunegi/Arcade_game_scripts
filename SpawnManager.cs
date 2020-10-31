using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyprefab;

    [SerializeField]
    private GameObject[] powerup;
    [SerializeField]
    private GameObject enemycontainer;
    private bool stopspawning;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnenemy());
        StartCoroutine(spawnpowerup());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawnenemy()
    {
        while(stopspawning==false)
        {
            Vector3 pos = new Vector3(Random.Range(-10f, 10f), 8, 0);
            GameObject newenemy= Instantiate(enemyprefab, pos, Quaternion.identity);
            newenemy.transform.parent = enemycontainer.transform;
            yield return new WaitForSeconds(2f);

        }
      

        
    }
    IEnumerator spawnpowerup()
    {
        //spawn a power up every 3,7 seconds
        while (stopspawning == false)
        {
            float t = Random.Range(5f, 8f);
            Vector3 position = new Vector3(Random.Range(-10f, 10f), 8, 0);
            int randompowerup = Random.Range(0, 3);
            GameObject newenemy = Instantiate(powerup[randompowerup], position, Quaternion.identity);
            yield return new WaitForSeconds(t);
        }
    }
    public void death()
    {
        stopspawning = true;
    }
}
