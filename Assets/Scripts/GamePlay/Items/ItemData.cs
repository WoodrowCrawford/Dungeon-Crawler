using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Create Item")]

public class ItemData : ScriptableObject
{

    [SerializeField]
    private int _key;

    [SerializeField]
    private string _name;

    [SerializeField]
    private ItemType _itemType;

    [SerializeField]
    private string _description;


    [SerializeField]
    private Image _image;

    [SerializeField]
    private GameObject _gameObject;

    public enum ItemType
    {
        Coin,
        Potion, 
        MagicPotion,


    }
}
