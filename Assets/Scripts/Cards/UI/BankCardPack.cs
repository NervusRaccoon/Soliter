using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoliterGame.Cards.Db.CardDB;

public class BankCardPack : CardPack
{
    protected override void UpdateView(int pos, CardData card)
    {
        if (pos == _cardPackPosIndex)
        {
            _currentCardData = card;

            if (string.IsNullOrEmpty(card.CardName))
            {
                _cardImg.enabled = false;
            }
        }
    }

    public override void OnClick()
    {
        Game.Card.OpenNextBankCard(_cardPackPosIndex);
    }
}
