using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoliterGame.Cards
{
    public class CardController : MonoBehaviour
    {
        private CardModel _cardModel;
        public ICard Model => _cardModel;

        private void Awake()
        {
            _cardModel = new CardModel();
            Game.Card = this;
        }

        private void Start()
        {
            _cardModel.Start();
        }

        private void OnDestroy()
        {
            Game.Card = null;
        }
    }
}
