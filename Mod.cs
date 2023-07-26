using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace SomeMediaCardsNS
{
    public class SomeMediaCards : Mod
    {
        public override void Ready()
        {
			WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.BasicFood, "SomeMediaCards_oran_berry", 1);
			WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.BasicEnemy, "SomeMediaCards_rattata", 1);
            Logger.Log("Ready!");
        }
    }
}