using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private int dmg = 0;

    public void setDamage(int damage) { dmg = damage; }
    public int getDamage() { return dmg; }
}
