using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("DON'T CHANGE")][SerializeField] GameObject[] blocks;
    public static GameManager Instance { get; private set; }

    [SerializeField] public bool gameOver;

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
        //game over
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject block in blocks)
        {
            BlockScript blockScript = block.GetComponent<BlockScript>();
            blockScript.ChangeColour();
        }
    }
}
