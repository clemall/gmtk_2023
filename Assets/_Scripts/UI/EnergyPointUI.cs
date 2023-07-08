using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnergyPointUI : MonoBehaviour
{
    public RectTransform energy;
    public Image  energyIMG;
    public Player player;
    public TextMeshProUGUI uiText;
    
    public Color goodColor;
    public Color warningColor;
    public Color dangerColor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = energy.localScale;
        newScale.x = (float)player.energy/(float)player.maxEnergy;
        energy.localScale = newScale;
        
         uiText.text = player.energy.ToString();
         
        if (player.energy > 20)
        {
            energyIMG.color = goodColor;
        }
        else if (player.energy > 10)
        {
            energyIMG.color = warningColor;
        }
        else
        {
            energyIMG.color = dangerColor;
        }
    }
}
