using System;
using System.Collections.Generic;
using UnityEngine;
using static SoliterGame.Cards.CardModel;

namespace SoliterGame.Cards
{
    public class CardController : MonoBehaviour
    {
        private CardModel _cardModel;
        public ICard Model => _cardModel;

        private Dictionary<int, (int, int)> _cardPacksStatus = new Dictionary<int, (int, int)>(); //pos, currCardCount, maxCardCount
        public Action<int, CardView> OnUpdateCardPack;
        private CardView _currentComboCard;

        private void Awake()
        {
            _cardModel = new CardModel();
            Game.Card = this;
        }

        private void Start()
        {
            _cardModel.OnCardPacksGenerated += CreateCardPacks;
            _cardModel.Start();
        }

        private void OnDestroy()
        {
            _cardModel.OnCardPacksGenerated -= CreateCardPacks;
            Game.Card = null;
        }

        private void CreateCardPacks()
        {
            var cardPacks = _cardModel.GetCardPacksData();
            _currentComboCard = cardPacks[0][0];
            foreach (var keyValue in cardPacks)
            {
                _cardPacksStatus.Add(keyValue.Key, (0, cardPacks.Count));
                OnUpdateCardPack?.Invoke(keyValue.Key, cardPacks[keyValue.Key][0]);
            }
        }

        private void UpdateCardPack(int pos)
        {
            var currentCardPos = _cardPacksStatus[pos];
            if (currentCardPos.Item1 + 1 <= currentCardPos.Item2)
            {
                currentCardPos.Item1 = currentCardPos.Item1 + 1;
                _cardPacksStatus[pos] = currentCardPos;
                OnUpdateCardPack?.Invoke(pos, _cardModel.GetCardData(pos, currentCardPos.Item1));
            }
            else
            {
                OnUpdateCardPack?.Invoke(pos, new CardView().Default());
            }
        }

        public void CheckCardCompliesRules(int pos, CardView card)
        {
            if (card.CardData.Index == _currentComboCard.CardData.PrevIndex || card.CardData.Index == _currentComboCard.CardData.NextIndex)
            {
                _currentComboCard = card;
                UpdateCardPack(pos);
            }
        }

        public void OpenNextBankCard(int pos)
        {
            if (_cardPacksStatus[pos].Item1 == _cardPacksStatus[pos].Item2)
            {
                OnUpdateCardPack?.Invoke(pos, new CardView().Default());
                return;
            }

            UpdateCardPack(pos);
            _currentComboCard = _cardModel.GetCardData(pos, _cardPacksStatus[pos].Item1);
            OnUpdateCardPack?.Invoke(pos, _currentComboCard);
        }

        public CardView GetCurrentComboCardData()
        {
            return _currentComboCard;
        }
    }
}
