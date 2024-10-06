using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] public bool mine, clicked;
    [SerializeField] public Material green, red;


    private void OnMouseDown()
    {
        if (!clicked)
        {
            ChangeColour();
            if (GameManager.Instance.gameOver)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    public void ChangeColour()
    {
        clicked = true;
        if (!mine)
        {
            gameObject.GetComponent<MeshRenderer>().material = green;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = red;
            GameManager.Instance.gameOver = true;
        }
    }
}
