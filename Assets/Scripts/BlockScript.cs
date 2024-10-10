using TMPro;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] public bool mine, clicked;
    [SerializeField] public Material green, red, lime;

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

    public void ChangeColour()
    {
        clicked = true;  // Mark block as clicked

        // Set block color based on whether it's a mine or based on mine count
        if (!mine && mineCount > 0)
        {
            gameObject.GetComponent<MeshRenderer>().material = green;
        }
        else if (mine)
        {
            gameObject.GetComponent<MeshRenderer>().material = red;
            GameManager.Instance.gameOver = true;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = lime;
        }

        minesText.text = mineCount.ToString();
        if (mineCount != 0)
        {
            minesTextObject.SetActive(true);
        }

        // Only flood fill if there are no neighboring mines and game isn't over
        if (mineCount == 0 && !GameManager.Instance.gameOver)
        {
            FloodFill();
        }
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
