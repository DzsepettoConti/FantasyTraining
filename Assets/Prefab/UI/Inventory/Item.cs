using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gamePLay")]
    public TileBase Tile;
    public itemType type;
    public ActionType actionType;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;



}

public enum itemType
{
    BuildingBlock,
    Tool,
    Weapon
}

public enum ActionType
{
    Attack,
    Dig,
    Mine
}