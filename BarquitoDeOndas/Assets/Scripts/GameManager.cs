using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int lifes;

    public GameObject lifeContainer;

    public List<GameObject> lifeList;

    public GameObject lifeSprite;

    public GameObject obstacle;

    public Vector3 spawnObstacle;

    public float spawnWait;

    public float startWait;

    public float waveWait;

    void Awake() {
        instance = this;
    }

    void Start() {
        for( int i = 0; i < lifes; i++ ) {
            GameObject life = GameObject.Instantiate(lifeSprite, lifeContainer.transform);
            life.gameObject.transform.localScale = Vector3.one;
            lifeList.Add( life);
        }
        StartCoroutine(SpawnWaves());
    }

    public void LoseLife() {
        if( lifes != 1 ) {
            GameObject.Destroy(lifeList[lifes-1]);
            lifes--;
        }
        else {
            GameOver();
        }
        lifeList.RemoveAt(0);
    }

    public void AddLife() {
        lifes++;
        GameObject life = GameObject.Instantiate(lifeSprite, lifeContainer.transform);
        lifeList.Add(life);
    }

    public void GameOver() {

    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        while (true) {
            for (int zpos = -3; zpos < 4; zpos += 3) {
                if (Random.value > 0.7f) {
                    Vector3 spawnPosition = new Vector3(spawnObstacle.x,
                                                         spawnObstacle.y, zpos);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(obstacle, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
