using System;

namespace SoliterGame.Cards
{
    public interface ICard
    {
        public Action OnCardPacksGenerated { get; set; }
    }
}
