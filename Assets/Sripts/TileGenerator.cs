using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnPos = 0;
    private float tileLength = 100;
    [SerializeField] private Transform player;
    private int startTiles = 6;
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTile(0);
        SpawnTile(9);
        SpawnTile(10);
        SpawnTile(11);
        SpawnTile(12);
        SpawnTile(13);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - 60 > spawnPos - (startTiles * tileLength))
        {
            SpawnTile(i);
            if (i == 20)
            {
                i = 0;
            }
            i+=1;
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
