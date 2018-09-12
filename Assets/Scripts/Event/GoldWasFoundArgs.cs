using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldWasFoundArgs : EventArgs
{
    public Player player;
    public GoldWasFoundArgs(Player _player)
    {
        player = _player;
    }


}
