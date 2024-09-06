using Game;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Healer : MonoBehaviour, IBonus
{
    [SerializeField] private int _health;

    public void SetBonus(Presenter player)
    {
        player.Heal(_health);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
