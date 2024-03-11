using UnityEngine;

[CreateAssetMenu(menuName ="Cards/Plant Card",fileName ="New Plant Card")]
public class PlantCardScrOb : ScriptableObject
{
    public Sprite plantCard_active;
    public Sprite plantCard_inactive;
    public Sprite plantSprite;
    public LayerMask zombieLayer;
    public GameObject Bullet;
    public int cost;
    public float cooldown;
    public bool isSunFlower;
    public SunSpawner sunSpawnerTemplate;
    public float health;
    public float damage;
    public float range;
    public float speed;
    public float fireRate;

    public string myname;
}
