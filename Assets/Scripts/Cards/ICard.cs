using System;
using static SoliterGame.Cards.Db.CardDB;
using UnityEngine;

namespace SoliterGame.Cards
{
    public interface ICard
    {
        public Action OnCardPacksGenerated { get; set; }
    }
}
