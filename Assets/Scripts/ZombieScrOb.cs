using UnityEngine;

[CreateAssetMenu(menuName ="Entities/Zombie", fileName ="New Zombie")]

public class ZombieScrOb : ScriptableObject
{
    public GameObject zombieDefault;
    public GameObject zombieAccessory;

   // public ZombieType zombieType;

    public float accessoryHealth;
    public float zombieHealth;
    public float zombieHandHealth;
    public float zombieDamage;
    public float zombieSpeed;
    public float attackInterval;

    //public bool ridDefaultSprite;
    //public bool useChildObjects = true;

    //public Sprite newSprite;

   // void enum ZombieType
    //{
       // Normal,
        //ConeZombie
    //}
}
