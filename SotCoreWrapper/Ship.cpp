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

    float Ship::getDragWhenGrindingToHalt()
    {
        return getActor().GetSinkingComponent().SinkingParams.DragWhenGrindingToHalt;
    }

    float Ship::getMinSpdToStopToBeforeLowering()
    {
        return getActor().GetSinkingComponent().SinkingParams.MinSpdToStopToBeforeLowering;

    }

    float Ship::getLowerIntoWaterTime()
    {
        return getActor().GetSinkingComponent().SinkingParams.LowerIntoWaterTime;
    }

    float Ship::getTimeIntoLoweringToStartOcclusionZoneShrinkage()
    {
        return getActor().GetSinkingComponent().SinkingParams.TimeIntoLoweringToStartOcclusionZoneShrinkage;
    }

    float Ship::getAngularDragDuringSinkingSequence()
    {
        return getActor().GetSinkingComponent().SinkingParams.AngularDragDuringSinkingSequence;
    }

    float Ship::getKeeledOverTime()
    {
        return getActor().GetSinkingComponent().SinkingParams.KeeledOverTime;
    }

    float Ship::getTurnOffBuoyancyTime()
    {
        return getActor().GetSinkingComponent().SinkingParams.TurnOffBuoyancyTime;
    }

    float Ship::getFinalSinkingBuoyancy()
    {
        return getActor().GetSinkingComponent().SinkingParams.FinalSinkingBuoyancy;
    }

    float Ship::getSinkingTimeUntilDestroy()
    {
        return getActor().GetSinkingComponent().SinkingParams.SinkingTimeUntilDestroy;
    }
    
    float Ship::getReduceWaterOcclusionZoneTime()
    {
        return getActor().GetSinkingComponent().SinkingParams.ReduceWaterOcclusionZoneTime;
    }

    float Ship::getReduceWaterOcclusionZoneTimeHurryUp()
    {
        return getActor().GetSinkingComponent().SinkingParams.ReduceWaterOcclusionZoneTimeHurryUp;
    }

    float Ship::getTimeIntoKeelingOverToTeleportPlayer()
    {
        return getActor().GetSinkingComponent().SinkingParams.TimeIntoKeelingOverToTeleportPlayer;
    }
    
    float Ship::getMinSampleSubmersionToConsiderInWater()
    {
        return getActor().GetSinkingComponent().SinkingParams.MinSampleSubmersionToConsiderInWater;
    }

    float Ship::getMinPctSamplesRequiredSubmergedToBeAbleToSink()
    {
        return getActor().GetSinkingComponent().SinkingParams.MinPctSamplesRequiredSubmergedToBeAbleToSink;
    }
}