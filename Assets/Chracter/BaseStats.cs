using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    [SerializeField]
    protected int _Hp;
    [SerializeField]
    protected int _Mp;
    [SerializeField]
    protected int _MaxHp;
    [SerializeField] 
    protected int _MaxMp;
    [SerializeField]
    protected int _AttackDamage;
    [SerializeField]
    protected int _Armor;
    [SerializeField]
    protected float _MoveSpeed;

    public int Hp { get { return _Hp; } set { _Hp = value; } }
    public int MaxHp { get { return _MaxHp; } set { _MaxHp = value; } }
    public int Mp { get { return _Mp; } set { _Mp = value; } }
    public int MaxMp { get { return _MaxMp; } set { _MaxMp = value; } }
    public int Attack { get { return _AttackDamage; } set { _AttackDamage = value; } }
    public int Defense { get { return _Armor; } set { _Armor = value; } }
    public float MoveSpeed { get { return _MoveSpeed; } set { _MoveSpeed = value; } }
    private void Start()
    {
        _Hp = 100;
        _Mp = 100;
        _MaxHp = 100;
        _MaxMp = 100;
        _AttackDamage = 10;
        _Armor = 5;
        _MoveSpeed = 5.0f;
    }
}
