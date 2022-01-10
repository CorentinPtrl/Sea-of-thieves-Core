#include "pch.h"
#include "Player.h"
namespace SoT
{
	Player::Player(UE4Actor^ actor) : UE4Actor(actor)
	{

	}

	System::String^ Player::getPlayerName()
	{
		return gcnew System::String(getActor().GetPlayerState().GetName().c_str());
	}

	System::String^ Player::getWieldedItem()
	{
		return  gcnew System::String(getActor().GetWieldedItemComponent().GetWieldedItem().GetItemInfo().GetItemDesc().GetName().c_str());
	}

	int Player::getHealth()
	{
		return getActor().GetHealthComponent().GetHealth();
	}

	int Player::getMaxHealth()
	{
		return getActor().GetHealthComponent().GetMaxHealth();
	}

}