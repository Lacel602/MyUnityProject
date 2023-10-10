using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteBackgroundScroll : MonoBehaviour
{
    public Tilemap backgroundTilemap;
    public int numberOfClones = 2;
    public float moveSpeed = 2.0f;

    private Vector3 cloneOffset;
    private float tileSizeX;

    private void Start()
    {
        tileSizeX = backgroundTilemap.GetComponent<TilemapRenderer>().bounds.size.x;
        cloneOffset = new Vector3(tileSizeX, 0, 0);

        // Clone and arrange Tilemaps
        for (int i = 1; i < numberOfClones; i++)
        {
            Tilemap clone = Instantiate(backgroundTilemap, transform);
            clone.transform.Translate(cloneOffset * i);
        }
    }

    private void Update()
    {
        // Move Tilemaps to the left
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (child.position.x <= -tileSizeX)
            {
                child.Translate(cloneOffset * (numberOfClones - 1));
            }
        }
    }
}
