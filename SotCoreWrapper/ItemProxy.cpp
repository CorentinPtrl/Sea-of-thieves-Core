#include "pch.h"
#include "ItemProxy.h"
namespace SoT
{
	ItemProxy::ItemProxy(UE4Actor^ actor) : UE4Actor(actor)
	{

	}

	AItemProxy ItemProxy::getActor()
	{
		return *reinterpret_cast<AItemProxy*>(&UE4Actor::getActor());
	}

	int ItemProxy::getTreasureType()
	{
		return getActor().GetBootyItemInfo().GetBootyType();
	}

	int ItemProxy::getTreasureRarity()
	{
		return getActor().GetBootyItemInfo().GetRareityId();
	}

	System::String^ ItemProxy::getTreasureName()
	{
		return gcnew System::String(getActor().GetBootyItemInfo().GetItemDesc().GetName().c_str());
	}
}