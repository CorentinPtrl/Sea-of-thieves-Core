#pragma once
#include "Core.h"
#include "ManagedObject.h"
#include "VectorUE4.h"
#include "UE4ActorWrapper.h"
using namespace System;
namespace CLI
{
    public ref class SotLevel : public ManagedObject<Core::Level>
    {
    public:

        SotLevel();
        array<UE4ActorWrapper^>^ getActors();
    };
}