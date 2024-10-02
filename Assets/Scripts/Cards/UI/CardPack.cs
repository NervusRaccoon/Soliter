using SoliterGame.Cards;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using static SoliterGame.Cards.CardModel;

public class CardPack : MonoBehaviour
{
    [SerializeField] protected int _cardPackPosIndex;
    [SerializeField] protected List<Text> _cardNameTexts;
    [SerializeField] protected Image _cardImg;
    [SerializeField] protected Image _cardBack;

    protected CardView _currentCardData;
    protected bool _isShown = true;

    protected virtual void Start()
    {
        Game.Card.OnUpdateCardPack += UpdateView;
    }

    private void OnDestroy()
    {
        if (Game.Card != null)
        {
            Game.Card.OnUpdateCardPack -= UpdateView;
        }
    }

    protected virtual void UpdateView(int pos, CardView card)
    {
        if (pos == _cardPackPosIndex)
        {
            _cardNameTexts.ForEach(x => x.text = card.CardData.CardName);
            _cardImg.sprite = card.Sprite;
            _currentCardData = card;

            CheckPackVisability(card.CardData.CardName);
        }
    }

    protected void CheckPackVisability(string cardName)
    {
        if (_isShown && string.IsNullOrEmpty(cardName))
        {
            _isShown = !_isShown;
            ShowHidePack(_isShown);
        }
        else if (!_isShown && !string.IsNullOrEmpty(cardName))
        {
            _isShown = !_isShown;
            ShowHidePack(_isShown);
        }
    }

    protected virtual void ShowHidePack(bool isShow)
    {
        _cardNameTexts.ForEach(x => x.enabled = isShow);
        _cardImg.enabled = isShow;
        _cardBack.enabled = isShow;
    }

    public virtual void OnClick()
    {
        Game.Card.CheckCardCompliesRules(_cardPackPosIndex, _currentCardData);
    }
}
