using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SoliterGame.Cards.Db.CardDB;

namespace SoliterGame.Cards
{
    public partial class CardModel : ICard
    {
        public CardModel()
        {
        }

        private class CardInCombo
        {
            public CardData CardData;
            public int Pos;
        }

        public List<CardData> CardsData;

        private Dictionary<int, List<CardInCombo>> _comboDict = new Dictionary<int, List<CardInCombo>>();
        private Dictionary<int, List<CardData>> _cardPacks = new Dictionary<int, List<CardData>>();

        public Action OnCardPacksGenerated { get; set; }

        public void Start()
        {
            CardsData = Game.Databases.Cards.GetStateCardsData();
            int cardsCount = Game.Databases.Cards.GetBoardCardsCount();
            (int, int) comboCardsCount = Game.Databases.Cards.GetComboCardsCount();
            int increasingComboChance = Game.Databases.Cards.GetIncreasingComboChance();
            int mixedComboChance = Game.Databases.Cards.GetMixedComboChance();
            int cardPacksCount = Game.Databases.Cards.GetCardPacksCount();
            GenerateCombo(cardsCount, comboCardsCount, increasingComboChance, mixedComboChance, cardPacksCount);
            GenerateCardPacks(cardPacksCount);
        }

        public List<CardData> GetCardPackData(int pos)
        {
            return _cardPacks.ContainsKey(pos) ? _cardPacks[pos] : new List<CardData>();
        }

        public CardData GetCardData(int packPos, int cardPos)
        {
            return _cardPacks.ContainsKey(packPos) ? _cardPacks[packPos][cardPos] : new CardData();
        }

        public Dictionary<int, List<CardData>> GetCardPacksData()
        {
            return _cardPacks;
        }

        private void GenerateCombo(int cardsCount, (int, int) comboCardsBorderCount, int increasingComboChance, int mixedComboChance, int cardPacksCount)
        {
            int cardsLeft = cardsCount;
            while (cardsLeft > 0)
            {
                int comboCardsCount = UnityEngine.Random.Range(comboCardsBorderCount.Item1, comboCardsBorderCount.Item2 + 1);
                cardsLeft -= comboCardsCount;

                List<CardInCombo> combo = new List<CardInCombo>();
                CardInCombo lastCardInCombo = new CardInCombo
                {
                    CardData = CardsData[UnityEngine.Random.Range(0, CardsData.Count)],
                    Pos = 0
                };
                combo.Add(lastCardInCombo);

                bool isIncreasing = UnityEngine.Random.Range(0, 100) <= increasingComboChance;
                for (int i = 1; i < comboCardsCount; i++)
                {
                    if (UnityEngine.Random.Range(0, 100) <= mixedComboChance)
                    {
                        isIncreasing = !isIncreasing;
                    }
                    lastCardInCombo = new CardInCombo
                    {
                        CardData = CardsData.Find(x => isIncreasing ? lastCardInCombo.CardData.NextIndex == x.Index : lastCardInCombo.CardData.PrevIndex == x.Index),
                        Pos = UnityEngine.Random.Range(1, cardPacksCount + 1)
                    };
                    combo.Add(lastCardInCombo);
                }
                _comboDict.Add(_comboDict.Count, combo);
            }
        }

        private void GenerateCardPacks(int cardPacksCount)
        {
            for (int i = _comboDict.First().Key; i <= _comboDict.Last().Key; i++)
            {
                _comboDict[i].ForEach(x =>
                {
                    if (_cardPacks.ContainsKey(x.Pos))
                    {
                        _cardPacks[x.Pos].Add(x.CardData);
                    }
                    else
                    {
                        _cardPacks.Add(x.Pos, new List<CardData> { x.CardData });
                    }
                });
            }

            OnCardPacksGenerated?.Invoke();
        }
    }
}
