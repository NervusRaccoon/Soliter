using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoliterGame.Cards.CardModel;

public class ComboCardPack : CardPack
{
    protected virtual void Start()
    {
        base.Start();
        UpdateView(0, null);
    }

    protected override void UpdateView(int pos, CardView card)
    {
        _currentCardData = Game.Card.GetCurrentComboCardData();
        _cardNameTexts.ForEach(x => x.text = _currentCardData.CardData.CardName);
        _cardImg.sprite = _currentCardData.Sprite;
    }

    public override void OnClick()
    {
    }
}
