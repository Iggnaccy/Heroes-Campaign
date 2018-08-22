using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatsPanel : MonoBehaviour
{
    public Text PlayerStatsPanelGold;
    public Text PlayerStatsPanelFame;
    Player player;

    public void SetPlayer(Player _player)
    {
        player = _player;
    }
    void Update()
    {
        if (player != null)
        {
            PlayerStatsPanelFame.text = player.Fame.ToString();
            PlayerStatsPanelGold.text = player.Gold.ToString();
        }
        
    }
}
