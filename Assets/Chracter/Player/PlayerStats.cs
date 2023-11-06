using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : BaseStats
{
    [SerializeField]
    protected int gold;

    private void Start()
    {
        _Hp = 100;
        _Mp = 100;
        _MaxHp = 100;
        _MaxMp = 100;
        _AttackDamage = 10;
        _Armor = 5;
        _MoveSpeed = 5f;
    }
}
