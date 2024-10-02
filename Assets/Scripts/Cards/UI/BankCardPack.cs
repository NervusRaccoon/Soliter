using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoliterGame.Cards.CardModel;

public class BankCardPack : CardPack
{
    protected override void UpdateView(int pos, CardView card)
    {
        if (pos == _cardPackPosIndex)
        {
            CheckPackVisability(card.CardData.CardName);
        }
    }

    protected override void ShowHidePack(bool isShow)
    {
        _cardBack.enabled = isShow;
    }

    public override void OnClick()
    {
        Game.Card.OpenNextBankCard(_cardPackPosIndex);
    }
}
