using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Transform transform;
    GameObject tileManager;
    public int resourcesPerSecond;

    TileManager tiles;
    TileObject whereIStand;

    float mineTime;

    DreamFuel fuel;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    public void construct(GameObject tilemanager, DreamFuel dreamFuel)
    {
        tileManager = tilemanager;
        fuel = dreamFuel;
        whereIStand = tileManager.GetComponent<TileManager>().GetTileFromPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        mineTime += Time.deltaTime;
        if (mineTime > 1) {
            mineTime = 0;
            MineResources();
        }
    }


    void MineResources() {
        if (whereIStand.GetResources() > 0)
        {
            whereIStand.Mine(resourcesPerSecond);
            fuel.currentResourceValue += resourcesPerSecond; 
        }
        if (whereIStand.GetResources() <= 0) {
            tiles.PlaceWalkable(whereIStand.GetLocation());
            Destroy(gameObject);
        }
    }
}