using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatText : MonoBehaviour
{
    // »ó½Â°¡´É ½ºÅÝ
    public Text PlayerATKText;
    public Text PlayerHPText;
    public Text AbilityPointText;
    // Ç¥±â ½ºÅÝ
    public Text PlayerTotalDmgText;  
    public PlayerMoving playerMoving;


    float PlayerAtkDmg2;
    private void Awake()
    {
        AbilityPointText.text = playerMoving.AbilityPoint.ToString();
        PlayerATKText.text = playerMoving.PlayerAtkDmg.ToString();
        PlayerHPText.text = playerMoving.PlayerHp.ToString();
        PlayerAtkDmg2 = playerMoving.PlayerAtkDmg * 2;
        PlayerTotalDmgText.text = playerMoving.PlayerAtkDmg.ToString() + " ~ " + PlayerAtkDmg2.ToString();
    }
    public void StatTextUpLoad()
    {
        AbilityPoint_Text();
        PlayerATK_Text();
        PlayerHP_Text();
        PlayerTotalDmg_Text();
    }

    public void AbilityPoint_Text()
    {
        AbilityPointText.text = playerMoving.AbilityPoint.ToString();
    }
    public void PlayerATK_Text()
    {
        PlayerATKText.text = playerMoving.PlayerAtkDmg.ToString();
    }
    public void PlayerHP_Text()
    {
        PlayerHPText.text = playerMoving.PlayerHp.ToString();
    }
    public void PlayerTotalDmg_Text()
    {
        PlayerAtkDmg2 = playerMoving.PlayerAtkDmg * 2;
        PlayerTotalDmgText.text = playerMoving.PlayerAtkDmg.ToString() + " ~ " + PlayerAtkDmg2.ToString();
    }

    public void StatAtkUpButton()
    {
        if (playerMoving.AbilityPoint >= 1)
        {
            playerMoving.AbilityPoint -= 1;
            playerMoving.PlayerAtkDmg += 1;
            PlayerATK_Text();
            AbilityPoint_Text();
            PlayerTotalDmg_Text();
        }
    }
    public void StatHpUpButton()
    {
        if (playerMoving.AbilityPoint >= 1)
        {
            playerMoving.AbilityPoint -= 1;
            playerMoving.PlayerHp += 10;
            PlayerHP_Text();
            AbilityPoint_Text();
            PlayerTotalDmg_Text();
        }
    }
}
