using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Transform transform;
    public int resourcesPerSecond;

    TileManager tiles;
    TileObject whereIStand;

    float mineTime;

    DreamFuel fuel;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void construct(TileManager tilemanager, DreamFuel dreamFuel)
    {
        transform = GetComponent<Transform>();
        tiles = tilemanager;
        fuel = dreamFuel;
        whereIStand = tiles.GetTileFromPosition(transform.position);
        Debug.Log("current resources: " + whereIStand.IsResourceTile());
    }

    // Update is called once per frame
    void Update()
    {
        mineTime += Time.deltaTime;
        if (mineTime > 1) {
            MineResources();
            mineTime = 0;
            
        }
    }


    void MineResources() {
        if (whereIStand.GetResources() > 0)
        {
            whereIStand.Mine(resourcesPerSecond);
            fuel.currentResourceValue += resourcesPerSecond;
            Debug.Log("mine");
        }
        if (whereIStand.GetResources() <= 0) {
            tiles.PlaceWalkable(whereIStand.GetLocation());
            Destroy(gameObject);
        }
    }
}