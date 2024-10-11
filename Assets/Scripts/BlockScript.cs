using TMPro;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] public bool mine, clicked, flagged;
    [SerializeField] public Material clearMat, mineMat, dangerMat, flagMat, defaultMat;

    [SerializeField] public TMP_Text minesText;
    [SerializeField] public GameObject minesTextObject;
    [SerializeField] public int mineCount;


    private void Awake()
    {
        GameManager.Instance.blocks.Add(gameObject);
    }
    private void Start()
    {
        minesText = minesTextObject.GetComponent<TMP_Text>();
        defaultMat = GetComponent<MeshRenderer>().material;
    }

    private void OnMouseDown()
    {
        if (!clicked)
        {
            Debug.Log($"{transform.position} position, mineCount = {mineCount}.");
            ChangeColour();
            if (GameManager.Instance.gameOver)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (flagged == false)
            {
                ChangeColour(flagMat);
                flagged = true;
            }
            else
            {
                ChangeColour(defaultMat);
            }
        }
    }

    public void ChangeColour()
    {
        if (flagged == false)
        {
            clicked = true;  // Mark block as clicked

            // Set block color based on whether it's a mine or based on mine count
            if (!mine && mineCount > 0)
            {
                GetComponent<MeshRenderer>().material = dangerMat;
            }
            else if (mine)
            {
                GetComponent<MeshRenderer>().material = mineMat;
                GameManager.Instance.gameOver = true;
            }
            else
            {
                GetComponent<MeshRenderer>().material = clearMat;
            }

            minesText.text = mineCount.ToString();
            if (mineCount != 0 && !mine)
            {
                minesTextObject.SetActive(true);
            }

            // Only flood fill if there are no neighboring mines and game isn't over
            if (mineCount == 0 && !GameManager.Instance.gameOver)
            {
                FloodFill();
            }
        }
    }

    public void ChangeColour(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }

    private void FloodFill()
    {
        foreach (GameObject block in GameManager.Instance.blocks)
        {
            BlockScript neighborBlock = block.GetComponent<BlockScript>();

            // Prevent recursion on the current block itself and already clicked blocks
            if (!neighborBlock.clicked && mineCount == 0)
            {
                // Check horizontal, vertical, and diagonal neighbors
                float absoluteX = Mathf.Abs(transform.position.x - block.transform.position.x);
                float absoluteZ = Mathf.Abs(transform.position.z - block.transform.position.z);

                if ((absoluteX == 2 && absoluteZ == 0) || (absoluteX == 0 && absoluteZ == 2) || (absoluteX == 2 && absoluteZ == 2))
                {
                    if (!neighborBlock.mine)
                    {
                        neighborBlock.ChangeColour();  // Recursively reveal the neighbor
                    }
                }
            }
        }
    }
}
