#include "pch.h"
#include "Ship.h"

using namespace Core;
namespace SoT
{
	Ship::Ship(UE4Actor^ actor) : UE4Actor(actor)
	{

	}

	AShip Ship::getActor()
	{
		return *reinterpret_cast<AShip*>(&UE4Actor::getActor());
	}

	Guid^ Ship::getGuid()
	{
		return gcnew SoT::Guid(getActor().GetCrewOwnershipComponent().GetCrewId());
	}
}