using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoliterGame.Cards.Db.CardDB;

public class ComboCardPack : CardPack
{
    protected override void UpdateView(int pos, CardData card)
    {
        _currentCardData = Game.Card.GetCurrentComboCardData();
        _cardNameText.text = _currentCardData.CardName;
    }

    public override void OnClick()
    {
    }
}
