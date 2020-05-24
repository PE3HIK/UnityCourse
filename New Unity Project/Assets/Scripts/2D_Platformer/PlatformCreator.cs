using UnityEngine;

namespace _2D_Platformer
{
    public class PlatformCreator : MonoBehaviour
    {
        [SerializeField]private int minTile = 2;
        [SerializeField]private int maxTile = 5;

        [SerializeField] private GameObject startTile; 
        [SerializeField] private GameObject finishTile; 
        [SerializeField] private GameObject[] middleTiles;

        [ContextMenu("generate")]
        private void PlatformGenerate()
        {
            Vector3 tilePos = Vector3.zero;
            GameObject platform = new GameObject("platform");
            GameObject tile = Instantiate(startTile, platform.transform);
            tile.transform.localPosition = tilePos;
            tilePos.x += tile.GetComponent<BoxCollider2D>().size.x/2;
            
            int rndTiles = Random.Range(minTile, maxTile+1)-2;
            
            
            for (int i = 0; i < rndTiles; i++)
            {
                tile = Instantiate(middleTiles[Random.Range(0, middleTiles.Length)], platform.transform);
                tilePos.x += tile.GetComponent<BoxCollider2D>().size.x/2 ; 
                tile.transform.localPosition = tilePos;
                tilePos.x += tile.GetComponent<BoxCollider2D>().size.x/2 ;
            }
            
            tile = Instantiate(finishTile, platform.transform);
            tilePos.x += tile.GetComponent<BoxCollider2D>().size.x/2 ; 
            tile.transform.localPosition = tilePos;
        }
    }
}