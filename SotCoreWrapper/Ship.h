#pragma once
#include "Core.h"
#include "UE4ActorWrapper.h"
#include "SOTStuff.h"
#include "Guid.h"

using namespace Core;
namespace SoT
{
    public ref class Ship : public UE4Actor
    {
    private:
        AShip getActor();
    public:
        Ship(UE4Actor^ actor);
        Guid^ getGuid();
        
        float getCurrentWaterLevel();
        float getCurrentWaterAmount();

        float getDragWhenGrindingToHalt();
        float getMinSpdToStopToBeforeLowering();
        float getLowerIntoWaterTime();
        float getTimeIntoLoweringToStartOcclusionZoneShrinkage();
        float getAngularDragDuringSinkingSequence();
        float getKeeledOverTime();
        float getTurnOffBuoyancyTime();
        float getFinalSinkingBuoyancy();
        float getSinkingTimeUntilDestroy();
        float getReduceWaterOcclusionZoneTime();
        float getReduceWaterOcclusionZoneTimeHurryUp();
        float getTimeIntoKeelingOverToTeleportPlayer();
        float getMinSampleSubmersionToConsiderInWater();
        float getMinPctSamplesRequiredSubmergedToBeAbleToSink();

    };
}

