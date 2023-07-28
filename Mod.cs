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
			WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.CookingIdea, "SomeMediaCards_blueprint_oran_berry", 1);
			WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.Animal, "SomeMediaCards_rattata", 1);
			WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.BasicEnemy, "SomeMediaCards_minecraft_zombie", 1);
            Logger.Log("Ready!");
        }
    }
	
	public class Pokemon : Animal
	{
		// this method decides whether a card should stack onto this one
		protected override bool CanHaveCard(CardData otherCard)
		{
			if (otherCard.Id == "SomeMediaCards_oran_berry")
				return true; // if the other card is an apple, we will let it stack
			return base.CanHaveCard(otherCard); // otherwise, we will let Animal.CanHaveCard decide
		}

		// this method is called every frame, it is the CardData equivalent of the Update method
		public override void UpdateCard()
		{
			// the ChildrenMatchingPredicate method will return all child cards (cards stacked on the current one) that match a given predicate function
			// the given function checks if the card is an apple, so the apples variable will be a list of the apple cards on the red panda
			var orans = ChildrenMatchingPredicate(childCard => childCard.Id == "SomeMediaCards_oran_berry");
			if (orans.Count > 0) // if there are any apples on the red panda
			{
				int healed = 0; // create a variable to keep track of how much health the red panda gained
				foreach (CardData oran in orans) // for each apple on the red panda
				{
					if(HealthPoints >= BaseCombatStats.MaxHealth)
					{
						oran.MyGameCard.RemoveFromStack();
					}
					else
					{
						oran.MyGameCard.DestroyCard(); // destroy the apple card
						HealthPoints += 1; // increase the red pandas health by 2
						healed += 1; // keep track of how much it healed in total
					}
				}
				
				if(HealthPoints < BaseCombatStats.MaxHealth)
				{
					AudioManager.me.PlaySound(AudioManager.me.Eat, Position); // play the eating sound at the red pandas position
					WorldManager.instance.CreateSmoke(Position); // create smoke particles at the red pandas position
					CreateHitText($"+{healed}", PrefabManager.instance.HealHitText); // create a heal text that displays how much it healed in total
				}
			}
			base.UpdateCard(); // call the Animal.UpdateCard method
		}
	}
}