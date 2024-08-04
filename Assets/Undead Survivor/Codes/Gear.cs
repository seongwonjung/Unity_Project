﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public itemData.ItemType type;
    public float rate;

    public void Init(itemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.Instance.player.transform;
        transform.localScale = Vector3.zero;

        // Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case itemData.ItemType.Glove:
                RateUp();
                break;
            case itemData.ItemType.Shoe:
                SpeedUp(); 
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.speed = speed + (speed * rate);
                    break;
                default:
                    speed = 0.5f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3* Character.Speed;
        GameManager.Instance.player.speed = speed + speed * rate;
    }
}