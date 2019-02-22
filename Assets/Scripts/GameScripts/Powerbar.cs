using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Powerbar : MonoBehaviour
{

    public GameObject player;
    public Image powerBarFill;
    public Image powerBarFill2;
    public List<Sprite> powerBarSprites;
    // Use this for initialization
    void Start()
    {
        powerBarSprites = new List<Sprite>();

        foreach (Object power in Resources.LoadAll("HUD/Powerbar", typeof(Sprite)))
        {
            powerBarSprites.Add((Sprite)power);
        }
        powerBarFill = GameObject.FindGameObjectWithTag("PowerBarFill").GetComponent<Image>();
        powerBarFill2 = GameObject.FindGameObjectWithTag("PowerBarFill2").GetComponent<Image>();
    }

    //Update the slider to be equal to the power
    public void updatePowerBar()
    {
        for (int i = 0; i < 11; i++)
        {
            if ((int)player.GetComponent<Throw>().getPower() == i)
            {
                powerBarFill.sprite = powerBarSprites[i];
                powerBarFill2.sprite = powerBarSprites[i];
            }
        }

    }
}
