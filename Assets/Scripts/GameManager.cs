using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public bool gameOver;

    [Tooltip("DON'T CHANGE")][SerializeField] public List<GameObject> blocks = new List<GameObject>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log($"More than one {this}. Destroying {gameObject}.");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }



    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
        }
        //game over
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject block in blocks)
        {
            BlockScript blockScript = block.GetComponent<BlockScript>();
            blockScript.ChangeColour();
        }
    }
}
