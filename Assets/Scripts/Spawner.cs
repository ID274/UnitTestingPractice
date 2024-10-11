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
    [Header("CAN'T BE MORE THAN HALF OF AREA")][SerializeField] public int mines;


    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private int blockCount = 0;
    List<Vector3> minePos = new List<Vector3>();
    [SerializeField] List<GameObject> blocks = new List<GameObject>();

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
                blocks.Add(blockInstance);
            }
        }
        foreach (GameObject block in blocks)
        {
            HashSet<GameObject> countedMines = new HashSet<GameObject>(); // Avoid double-counting

            foreach (GameObject block1 in blocks)
            {
                if (block != block1) // Prevent comparing the block with itself
                {
                    // Check horizontal, vertical, and diagonal neighbors
                    float absoluteX = Mathf.Abs(block.transform.position.x - block1.transform.position.x);
                    float absoluteZ = Mathf.Abs(block.transform.position.z - block1.transform.position.z);
                    if ((absoluteX == 2 && absoluteZ == 0) || (absoluteX == 0 && absoluteZ == 2) || (absoluteX == 2 && absoluteZ == 2))
                    {
                        // Check if the neighboring block is a mine and hasn't already been counted
                        if (block1.GetComponent<BlockScript>().mine == true && !countedMines.Contains(block1))
                        {
                            block.GetComponent<BlockScript>().mineCount++;
                            countedMines.Add(block1); // Add to HashSet to avoid double-counting
                        }
                    }
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

