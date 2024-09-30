using SoliterGame.Cards.Db;
using UnityEngine;

public class Databases : MonoBehaviour
{
    [SerializeField] public CardDB Cards;

    private void Awake()
    {
        Game.Databases = this;
    }

    private void OnDestroy()
    {
        Game.Databases = null;
    }
}
