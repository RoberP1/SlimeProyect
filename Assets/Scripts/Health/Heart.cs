using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{

    public int state = 2;
    public Sprite[] sprites;

    void Start()
    {

    }
    public void SetSprite()
    {
        GetComponent<Image>().sprite = sprites[state];
    }
}
