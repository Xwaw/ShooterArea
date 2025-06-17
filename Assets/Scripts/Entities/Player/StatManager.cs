using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.Interfaces;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StatManager : EntityData, IMainStatGetter, IMovementStatGetter
{
    void Start()
    {
        damage = 1f;
        defense = 0f;
        attackSpeed = 1f;
        
        speed = 16f;
        jumpForce = 24f;
    }
    
    public float GetDamage() => damage;
    public float GetAttackSpeed() => attackSpeed;
    public float GetDefence() => defense;
    
    public float GetMovementSpeed() => speed;
    public float GetJumpForce() => jumpForce;
}
