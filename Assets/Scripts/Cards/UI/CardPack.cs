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
    [SerializeField] protected Button _cardButton;

    protected CardView _currentCardData;
    protected bool _isShown = true;

    private void Start()
    {
        Game.Card.OnUpdateCardPack += UpdateView;
        Game.Card.OnPauseLevel += BlockButtonClick;
        Game.Card.OnUnpauseLevel += UnblockButtonClick;
    }

    private void OnDestroy()
    {
        if (Game.Card != null)
        {
            Game.Card.OnUpdateCardPack -= UpdateView;
            Game.Card.OnPauseLevel -= BlockButtonClick;
            Game.Card.OnUnpauseLevel -= UnblockButtonClick;
        }
    }

    private void BlockButtonClick()
    {
        _cardButton.interactable = false;
    }

    private void UnblockButtonClick()
    {
        _cardButton.interactable = true;
    }

    protected virtual void UpdateView(int pos, CardView card)
    {
        if (pos == _cardPackPosIndex)
        {
            CheckPackVisability(card.CardData.CardName);

            if (_isShown)
            {
                _cardNameTexts.ForEach(x => x.text = card.CardData.CardName);
                _cardImg.sprite = card.Sprite;
                _currentCardData = card;
            }
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
