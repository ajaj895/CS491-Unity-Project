using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform; //attach what you want the tiles to follow. In this case we're using the player.
    public float spawnX = -7.5f; //half the length of tile and spawns first tile in player view hiding empty world behind the player
    public float spawnY = -1f;// spawns tile at -1Y
    public float tileLength = 15.0f;// each prefab must be this length. Once we have more prefabs, this number should be bigger to allow for more objects to spawn.
    public float safeZone = 100.0f; //how fart the player can move from the origin of the spawner until prefabs start spwaning in.
    public int amountOfTilesOnScreen = 20; // the mamximum amount of prefabs that can spawn before the game deletes a tile.
    private int lastTileIndex = 0;

    private List<GameObject> activeTiles;

    void Start()
    {
        activeTiles = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amountOfTilesOnScreen; i++)
        {
            if (i < 5)//controls how many start tiles spawn. We can make this a variable later if we want.
                SpawnTile(0);
            else
                SpawnTile();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerTransform.position.x - safeZone > (spawnX - amountOfTilesOnScreen * tileLength))// checks if player is is outside the "safezone" to start spawning and deleting tiles.
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile(int prefabIndex = -1)
    {
        GameObject groundObject;

        if (prefabIndex == -1)
            groundObject = Instantiate(tilePrefabs[RandomTileIndex()]) as GameObject; // spawn random tile
        else
            groundObject = Instantiate(tilePrefabs[prefabIndex]) as GameObject;

        groundObject.transform.SetParent(transform);
        groundObject.transform.position = new Vector3(spawnX, spawnY, 0); // moves tile in the direction the player moves in.
        spawnX += tileLength;
        activeTiles.Add(groundObject);
    }

    void DeleteTile() //removes the first tile once the "amount of tiles on screen" variable is reached
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomTileIndex()// Early randomizer. Currently this method will make sure that one tile won't spawn twice in a row and randomly pick another tile.
    {
        if (tilePrefabs.Length <= 1)
        {
            return 0;
        }
        int randomIndex = lastTileIndex;
        while (randomIndex == lastTileIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastTileIndex = randomIndex;
        return randomIndex;
    }
}
