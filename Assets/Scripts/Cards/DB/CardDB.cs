using System.Collections.Generic;
using UnityEngine;

namespace SoliterGame.Cards.Db
{
    [CreateAssetMenu(fileName = "Cards", menuName = "SoliterGame/Databases/Cards")]
    public class CardDB : ScriptableObject
    {
        [System.Serializable]
        public class CardData
        {
            public int Index;
            public int PrevIndex;
            public int NextIndex;
            public string CardName;
        }

        public List<CardData> CardsData;
        public int OnBoardCardsCount;
        public Vector2Int ComboCardsCount;
        public int IncreasingComboChance;
        public int MixedComboChance;

        public List<CardData> GetStateCardsData()
        {
            return CardsData;
        }

        public int GetBoardCardsCount()
        {
            return OnBoardCardsCount;
        }

        public (int, int) GetComboCardsCount()
        {
            return (ComboCardsCount.x, ComboCardsCount.y);
        }

        public int GetIncreasingComboChance()
        {
            return IncreasingComboChance;
        }

        public int GetMixedComboChance()
        {
            return MixedComboChance;
        }
    }
}