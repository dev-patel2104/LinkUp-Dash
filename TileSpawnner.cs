using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnner : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject player;
    [SerializeField] int tileAmount = 2;
    [SerializeField] float tileLength = 11.3f;

    int cnt;
    private float spawnX = 0f;
    private float DestroyX = 0f;
    List<GameObject> tileParts = new List<GameObject>();

    
    void Start()
    {
        cnt = 0;
       for(int i=0; i< tileAmount; i++)
        {
            spawn();
        }
        DestroyX = transform.position.x;
        tileParts.Capacity = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > (spawnX - tileAmount * tileLength))
        {
            spawn(); 
        }

        if(player.transform.position.x > (DestroyX * 2))
        {
            GameObject tile = tileParts[cnt];
            Destroy(tile);
            DestroyX += tileLength;
            cnt++;
           
        }
    }

    private void spawn()
    {
        GameObject tile;
        tile = Instantiate(tilePrefab, transform.position + spawnX * Vector3.right, Quaternion.identity) as GameObject;
        spawnX += tileLength;
        tileParts.Add(tile);     
    }

    
}
