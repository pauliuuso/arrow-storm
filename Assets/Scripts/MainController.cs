using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour
{
    public static MainController mainController;
    public GameObject spawner;
    public int spawnerCount;
    public float spawnStep;
    public float spawnInterval;

    private List<GameObject> spawnerList = new List<GameObject>();
    [System.NonSerialized]
    public List<GameObject> activeArrowList = new List<GameObject>();
    private IEnumerator coroutine;

    void Awake()
    {
        if (mainController == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            mainController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawner.transform.position = new Vector3(0, 1, -41);

        SpawnSpawners();
        coroutine = SpawnArrows(0);
        StartCoroutine(coroutine);
    }

    public void SpawnSpawners()
    {
        for (int a = 0; a < spawnerCount; a++)
        {
            GameObject spawnerClone = Instantiate(
                spawner,
                new Vector3(spawner.transform.position.x + spawnStep * a, spawner.transform.position.y, spawner.transform.position.z),
                spawner.transform.rotation) as GameObject;

            spawnerList.Add(spawnerClone);
        }
    }

    public void ShootAllArrows()
    {
        for(int a = 0; a < activeArrowList.Count; a++)
        {
            activeArrowList[a].GetComponent<Arrow>().Shoot();
        }
    }

    public void RotateAllUp()
    {
        for (int a = 0; a < activeArrowList.Count; a++)
        {
            activeArrowList[a].GetComponent<Arrow>().RotateUp();
        }
    }

    public void RotateAllDown()
    {
        for (int a = 0; a < activeArrowList.Count; a++)
        {
            activeArrowList[a].GetComponent<Arrow>().RotateDown();
        }
    }

    IEnumerator SpawnArrows(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        for (int a = 0; a < spawnerList.Count; a++)
        {
            spawnerList[a].GetComponent<Spawn>().SpawnItem();
        }
    }

}
