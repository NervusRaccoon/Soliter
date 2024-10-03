using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SoliterGame.Cards.CardModel;

public class SimpleCardView : MonoBehaviour
{
    [SerializeField] private List<Text> _cardNameTexts;
    [SerializeField] private Image _cardImg;

    public void InitView(int pos, CardView card)
    {
        _cardNameTexts.ForEach(x => x.text = card.CardData.CardName);
        _cardImg.sprite = card.Sprite;
    }
}
