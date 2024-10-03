using System;
using System.Collections.Generic;
using System.Linq;
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
        public Action OnLevelFinished;
        public Action OnPauseLevel;
        public Action OnUnpauseLevel;
        public Action<int, CardView> OnStartAnimation;
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

        public void CreateCardPacks()
        {
            _cardPacksStatus.Clear();
            var cardPacks = _cardModel.GetCardPacksData();
            _currentComboCard = cardPacks[0][0];
            foreach (var keyValue in cardPacks)
            {
                _cardPacksStatus.Add(keyValue.Key, (0, cardPacks[keyValue.Key].Count - (keyValue.Key == 0 ? 1 : 0)));
                OnUpdateCardPack?.Invoke(keyValue.Key, cardPacks[keyValue.Key][0]);
            }
        }

        public void GenerateLevel()
        {
            _cardModel.GenerateLevel();
        }

        public void PauseLevel()
        {
            OnPauseLevel?.Invoke();
        }

        public void UnpauseLevel()
        {
            OnUnpauseLevel?.Invoke();
        }

        private void UpdateCardPack(int pos)
        {
            var currentCardPos = _cardPacksStatus[pos];
            if (currentCardPos.Item1 + 1 <= currentCardPos.Item2)
            {
                currentCardPos.Item1 = currentCardPos.Item1 + 1;
                _cardPacksStatus[pos] = currentCardPos;
                OnUpdateCardPack?.Invoke(pos, _cardModel.GetCardData(pos, currentCardPos.Item1));
                CheckWinConditions();
            }
            else
            {
                OnUpdateCardPack?.Invoke(pos, new CardView().Default());
            }
        }

        private void CheckWinConditions()
        {
            bool isWin = true;
            foreach(var keyValue in _cardPacksStatus)
            {
                if (keyValue.Value.Item1 != keyValue.Value.Item2)
                {
                    isWin = false;
                    break;
                }
            }

            if (isWin)
            {
                OnLevelFinished?.Invoke();
            }
        }

        public void CheckCardCompliesRules(int pos, CardView card)
        {
            if (card.CardData.Index == _currentComboCard.CardData.PrevIndex || card.CardData.Index == _currentComboCard.CardData.NextIndex)
            {
                OnStartAnimation?.Invoke(pos, card);
                UpdateCardPack(pos);
            }
        }

        public void UpdateCurrentComboCard(CardView card)
        {
           _currentComboCard = card;
            OnUpdateCardPack?.Invoke(-1, _currentComboCard);
        }

        public void OpenNextBankCard(int pos)
        {
            UpdateCardPack(pos);
            OnStartAnimation?.Invoke(pos, _cardModel.GetCardData(pos, _cardPacksStatus[pos].Item1));

            if (_cardPacksStatus[pos].Item1 == _cardPacksStatus[pos].Item2)
            {
                OnUpdateCardPack?.Invoke(pos, new CardView().Default());
            }
        }

        public CardView GetCurrentComboCardData()
        {
            return _currentComboCard;
        }
    }
}
