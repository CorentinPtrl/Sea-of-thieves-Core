#pragma once
#include "Core.h"
#include "UE4ActorWrapper.h"
namespace SoT
{
	public ref class Player : public UE4Actor
	{
	public:
		Player::Player(UE4Actor^ actor);
		System::String^ getPlayerName();
		System::String^ getWieldedItem();
		int getHealth();
		int getMaxHealth();

	};
}

