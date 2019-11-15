using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Damage to Enemy 
public interface IDamage
{
    void AddDamageEnemy(float damage);
}
//for puzzle
public interface IPuzzle
{
    int AddCountPuzzle();
    int GetCountPuzzle();
}

//Key Count
public interface IKey
{
    int GetKeyCount();
    int AddKeyCount();
    int RemoveKeyCount();
}

