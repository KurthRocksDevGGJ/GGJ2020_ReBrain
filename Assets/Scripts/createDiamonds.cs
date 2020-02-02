using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class createDiamonds : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    Collectible _collectible;
    // Start is called before the first frame update
    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    Instantiate(_collectible, new Vector2(x , y+1), Quaternion.identity);
                    tilemap.SetTile(new Vector3Int(x,y,0), null);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
