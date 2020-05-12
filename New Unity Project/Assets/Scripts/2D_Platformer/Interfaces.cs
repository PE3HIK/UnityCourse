﻿public interface IPlayer
{
    void RegisterPlayer();
}

public interface IEnemy
{
    void RegisterIEnemy();
}

public interface IDamage
{
    int Damage { get; }
    void SetDamage();
}

public interface IHitBox
{
    int Health { get; }
    void Hit(int damage);
    void Die(); 
}