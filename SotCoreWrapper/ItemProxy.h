#pragma once
#include "Core.h"
#include "UE4ActorWrapper.h"
namespace SoT
{
	public ref class ItemProxy : public UE4Actor
	{
	public:
		ItemProxy(UE4Actor^ actor);
		AItemProxy getActor();
		int getTreasureType();
		int getTreasureRarity();
		System::String^ getTreasureName();
	};
}

