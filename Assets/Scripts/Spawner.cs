using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Grid Area")]
    [Range(1, 10)]
    [SerializeField] public int length;
    [Range(1, 10)]
    [SerializeField] public int height;
    [Range(1, 30)]
    [Header("CAN'T BE MORE THAN HALF OF AREA")] [SerializeField] public int mines;


    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private int blockCount = 0;
    List<Vector3> minePos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        GetGridSize();
        GenerateMines();
        InstantiateGrid();
    }

    public void GetGridSize()
    {
        height = Mathf.Clamp(height, 1, 10);
        length = Mathf.Clamp(length, 1, 10);
    }

    void InstantiateGrid()
    {
        for (int h = 0; h < height; h++)
        {
            for (int l = 0; l < length; l++)
            {
                GameObject blockInstance = Instantiate(blockPrefab, new Vector3(2 * h, 0, 2 * l), Quaternion.identity);
                blockCount++;
                if (minePos.Contains(new Vector3(h, 0, l)))
                {
                    blockInstance.GetComponent<BlockScript>().mine = true;
                }
            }
        }
    }

    public void GenerateMines()
    {
        if (mines > length * height / 2)
        {
            mines = length * height / 2;
        }
        for (int i = 0; i < mines; i++)
        {
            int randomX = Random.Range(0, height);
            int randomZ = Random.Range(0, length);
            if (!minePos.Contains(new Vector3(randomX, 0, randomZ)))
            {
                minePos.Add(new Vector3(randomX, 0, randomZ));
            }
            else
            {
                i--;
            }
        }
    }

}
