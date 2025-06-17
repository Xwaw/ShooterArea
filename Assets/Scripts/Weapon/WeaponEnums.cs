using System;
using System.Collections.Generic;
using Autodesk.Fbx;
using Unity.VisualScripting;
using UnityEngine;

public enum ShootType
{
    Line, ProjectileDirection, ProjectilePhysics, Gas
}

public enum ProjectileProperty
{
    Normal, Piercing, GasEffect, Explosive, Laser
}

public enum ProjectileImage
{
    Line, Arrow, Fire, Rocket, RocketRpg7, LandMine, Granate, FlakBullet, Freeze, Knife
}

public enum RecoilStrength
{
    NoRecoil = 0, Low = 2, Medium = 5, High = 7, Extreme = 10
}

public enum RangeStrength
{
    Low = 30, Medium = 50, High = 70, Great = 90, Extreme = 100, Infinite = 1000
}

