using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using static SoliterGame.Cards.Db.CardDB;

public class CardPack : MonoBehaviour
{
    [SerializeField] protected int _cardPackPosIndex;
    [SerializeField] protected Text _cardNameText;
    [SerializeField] protected Image _cardImg;

    protected CardData _currentCardData;

    private void Start()
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

    protected virtual void UpdateView(int pos, CardData card)
    {
        if (pos == _cardPackPosIndex)
        {
            _cardNameText.text = card.CardName;
            _currentCardData = card;

            if (string.IsNullOrEmpty(card.CardName))
            {
                _cardImg.enabled = false;
            }
        }
    }

    public virtual void OnClick()
    {
        Game.Card.CheckCardCompliesRules(_cardPackPosIndex, _currentCardData);
    }
}
