using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : BaseTurret
{
    Transform transform;
    public int resourcesPerSecond;

    TileManager tiles;
    TileObject whereIStand;

    float mineTime;

    DreamFuel fuel;

    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
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
        if (isActive)
        {
            mineTime += Time.deltaTime;
            if (mineTime > 1)
            {
                MineResources();
                mineTime = 0;
            }
        }
    }


    void MineResources() {
        if (whereIStand.GetResources() > 0)
        {
            Debug.Log("mine");
            whereIStand.Mine(resourcesPerSecond);
            fuel.currentResourceValue += resourcesPerSecond;
        }
        if (whereIStand.GetResources() <= 0) {//checks if the resources have run out
            tiles.PlaceWalkable(whereIStand.GetLocation());
            isActive = false;
            Destroy(gameObject);
        }
    }
}