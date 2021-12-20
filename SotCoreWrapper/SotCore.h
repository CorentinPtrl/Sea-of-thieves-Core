#pragma once
#include <Process.h>
#include <dwmapi.h>
#include <iostream>
#include <sstream>
#include <thread>
#include <chrono>
#include <vector>
#include "SOTStuff.h"
#include "UE4ActorWrapper.h"
namespace Core
{
	class SotCore
	{
	public:
		static SotCore* singleton;
		std::vector<AActor> TempActors;
	public:
		SotCore();
		bool Prepare(bool IsSteam);
		AActor getLocalPlayer();
		TArray<Chunk*> SotCore::getLevelActors();
		float SotCore::GetCameraFOV();
		Vector3 GetCameraRotation();
		Vector3 GetCameraPosition();
		std::vector<AActor> getActors();
		std::string getNameFromIDmem(int ID);
		SoT::UE4Actor^ ActorToManaged(int id, AActor actor);


	};
}
