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

        private int _cardsCount;
        private (int, int) _comboCardsCount;
        private List<CardData> _cardsData;
        private int _increasingComboChance;
        private int _mixedComboChance;
        private Dictionary<int, List<CardData>> _comboDict = new Dictionary<int, List<CardData>>();

        public void Start()
        {
            _cardsData = Game.Databases.Cards.GetStateCardsData();
            _cardsCount = Game.Databases.Cards.GetBoardCardsCount();
            _comboCardsCount = Game.Databases.Cards.GetComboCardsCount();
            _increasingComboChance = Game.Databases.Cards.GetIncreasingComboChance();
            _mixedComboChance = Game.Databases.Cards.GetMixedComboChance();
            GenerateCombo();
        }

        private void GenerateCombo()
        {
            int cardsLeft = _cardsCount;
            while (cardsLeft > 0)
            {
                int comboCardsCount = Random.Range(_comboCardsCount.Item1, _comboCardsCount.Item2+1);
                cardsLeft -= comboCardsCount;

                List<CardData> combo = new List<CardData>();
                CardData lastCardInCombo = _cardsData[Random.Range(0, _cardsData.Count)];
                combo.Add(lastCardInCombo);

                bool isIncreasing = Random.Range(0, 100) <= _increasingComboChance;
                Debug.Log((isIncreasing ? "inc" : "dec") + " combo = " + comboCardsCount);
                Debug.Log(lastCardInCombo.CardName);
                for (int i = 1; i < comboCardsCount; i++)
                {
                    if (Random.Range(0, 100) <= _mixedComboChance)
                    {
                        isIncreasing = !isIncreasing;
                        Debug.Log("mixed combination!");
                    }
                    lastCardInCombo = _cardsData.Find(x => isIncreasing ? lastCardInCombo.NextIndex == x.Index : lastCardInCombo.PrevIndex == x.Index);
                    combo.Add(lastCardInCombo);
                    Debug.Log(lastCardInCombo.CardName);
                }
                _comboDict.Add(_comboDict.Count, combo);
            }
        }
    }
}
