﻿using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        Instantiate(Player, new Vector3(1, 1, 50), Quaternion.identity);
    }
}
