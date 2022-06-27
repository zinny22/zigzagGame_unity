using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private Transform currentTile;
    [SerializeField]
    private int spawnTileCountAtStart = 100;

    private void Awake()
    {
        for(int i =0; i<spawnTileCountAtStart; i++)
        {
            CreateTile();
        }
    }

    private void CreateTile()
    {
        GameObject clone = Instantiate(tilePrefab);
        clone.transform.SetParent(transform);
        clone.GetComponent<Tile>().Setup(this);
        SpawnTile(clone.transform);
    }

    public void SpawnTile(Transform tile)
    {
        tile.gameObject.SetActive(true);

        int index = Random.Range(0, 2);
        Vector3 addPosition = index == 0 ? new Vector3(2,0,0) : new Vector3(0,0,2);
        tile.position = currentTile.position + addPosition;

        currentTile = tile;

        int spawnItem = Random.Range(0, 100);
        if (spawnItem < 20)
        {
            tile.GetChild(1).gameObject.SetActive(true);
            if (index == 0)
            {
                tile.GetChild(1).gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                tile.GetChild(1).gameObject.transform.rotation = Quaternion.Euler(0, 100, 0);
                tile.GetChild(1).gameObject.transform.position = new Vector3(0, 5.4f, 0);
            }

        }
    }
}
