using Unity.Netcode;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using System.ComponentModel;

namespace OnePieceTCGCheat
{
    internal class Hacks : MonoBehaviour
    {
        GameplayLogicScript gameplayLogicScript = FindObjectOfType<GameplayLogicScript>();
        CardLogicScript cardLogicScript = FindObjectOfType<CardLogicScript>();
        ChoiceButtonScript choiseButton = FindObjectOfType<ChoiceButtonScript>();
        public GameObject goInitClicked;
        public GameObject selectedCard;

        public void OnGUI()
        {
           

            UIHelper.Begin("OP TCG Haxor", 0f, 0f, 300f, 1000f, 2f, 50f, 50f);
            if (gameplayLogicScript.go_FocusedObject && goInitClicked == gameplayLogicScript.go_FocusedObject)
            {
                CardLogicScript Card = gameplayLogicScript.go_FocusedObject.GetComponent<CardLogicScript>();
                UIHelper.Label(Card.myCard.cardDef.characterName + ", " + Card.myCard.cardPower.ToString() + ", " + Card.myCard.bTapped.ToString());
            }
            if (UIHelper.Button("Draw 2 Don") && gameplayLogicScript.e_GameStyle == GameStyle.Multiplayer)
            {
                gameplayLogicScript.e_CurrentState = GameplayState.PlayerTurn_DrawDon;
            }
            if (UIHelper.Button("Untap Cards, Draw 2 Don, Draw 1 Card") && gameplayLogicScript.e_GameStyle == GameStyle.Multiplayer)
            {
                gameplayLogicScript.e_CurrentState = GameplayState.PlayerTurn_Untap;
            }

            if (UIHelper.Button("Hit Leader For 2"))
            {
                gameplayLogicScript.e_CurrentState = GameplayState.PostAttack_HitLeaderDouble;
            }
            if (UIHelper.Button("Win Game (Need Blue Nami Leader)"))
            {
                StartCoroutine(WinGameCoroutine());
            }
            if (UIHelper.Button("Draw Card"))
            {
                gameplayLogicScript.e_CurrentState = GameplayState.PlayerTurn_DrawCardWait;
                gameplayLogicScript.ChoiceButtonClicked(ButtonChoiceType.DrawCard, 0);
            }
            if (UIHelper.Button("Increase Power Of Selected Card"))
            {
                if (selectedCard)
                {
                    CardLogicScript component = selectedCard.GetComponent<CardLogicScript>();
                    component.myCard.cardPower = 10000;
                }
            }
            if (UIHelper.Button("Decrease Cost Of Selected Card"))
            {
                if (selectedCard)
                {
                    CardLogicScript component = selectedCard.GetComponent<CardLogicScript>();
                    component.myCard.cardDef.cardCost = 0;
                }
            }
            if (UIHelper.Button("UnTap Selected Card"))
            {
                if (selectedCard)
                {
                    CardLogicScript Card = selectedCard.GetComponent<CardLogicScript>();
                    Card.myCard.bTapped = !Card.myCard.bTapped;

                }
            }

        }

        IEnumerator WinGameCoroutine()
        {
            for (int i = 0; i < 50; i++)
            {
                gameplayLogicScript.e_CurrentState = GameplayState.PlayerTurn_DrawCardWait;
                gameplayLogicScript.ChoiceButtonClicked(ButtonChoiceType.DrawCard, 0);
                yield return new WaitForSeconds(0.05f); // Add a delay of 1 second between iterations
            }
        }
        public void Start()
        {

        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (gameplayLogicScript.go_FocusedObject && gameplayLogicScript.go_FocusedObject.GetComponent<CardLogicScript>())
                {
                    goInitClicked = gameplayLogicScript.go_FocusedObject;
                    selectedCard = gameplayLogicScript.go_FocusedObject;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                goInitClicked = null;
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Loader.Unload();
            }
        }
    }
}
