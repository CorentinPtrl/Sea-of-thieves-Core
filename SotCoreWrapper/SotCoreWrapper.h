#pragma once
#include "pch.h"
#include "Core.h"
#include "ManagedObject.h"
#include "UE4ActorWrapper.h"
#include "VectorUE4.h"
#include "process_manager.h"

using namespace System;
namespace SoT
{
    public ref class SotCore : public ManagedObject<Core::SotCore>
    {
    public:

        SotCore();
        bool Prepare();
        array<UE4Actor^>^ GetActors();
    };
}