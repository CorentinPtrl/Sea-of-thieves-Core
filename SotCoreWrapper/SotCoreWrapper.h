#pragma once
#include "pch.h"
#include "Core.h"
#include "ManagedObject.h"
using namespace System;
namespace CLI
{
    public ref class SotCore : public ManagedObject<Core::SotCore>
    {
    public:

        SotCore();
        bool Prepare();
    };
}