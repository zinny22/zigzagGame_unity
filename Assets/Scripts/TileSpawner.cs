using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
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
        SpawnTile(clone.transform);
    }

    public void SpawnTile(Transform tile)
    {
        tile.gameObject.SetActive(true);

        int index = Random.Range(0, 2);
        Vector3 addPosition = index == 0 ? Vector3.right : Vector3.forward;
        tile.position = currentTile.position + addPosition;

        currentTile = tile;
    }
}
