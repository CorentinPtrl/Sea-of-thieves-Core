#pragma once
#include "pch.h"
#include "Core.h"
#include "ManagedSoTObject.h"
#include "UE4ActorWrapper.h"
#include "VectorUE4.h"
#include "CameraManager.h"
#include "Player.h"
#include "process_manager.h"

using namespace System;
namespace SoT
{
    public ref class SotCore : public ManagedSoTObject<Core::SotCore>
    {
    private:
        CameraManager^ cameraManager;
        Player^ localPlayer;
    public:
        SotCore();
        bool Prepare(System::Boolean^ IsSteam);
        Player^ GetLocalPlayer();
        CameraManager^ GetCameraManager();
        array<UE4Actor^>^ GetActors();
    };
}