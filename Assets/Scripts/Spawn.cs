using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject spawnItem;
    public GameObject body;

    public void SpawnItem()
    {
        GameObject spawnItemClone = Instantiate(spawnItem, transform.position, transform.rotation) as GameObject;

        if(spawnItemClone.GetComponent<Arrow>())
        {
            MainController.mainController.activeArrowList.Add(spawnItemClone);
        }

        spawnItemClone.GetComponent<Item>().spawner = gameObject;
        body.SetActive(false);
    }

}
