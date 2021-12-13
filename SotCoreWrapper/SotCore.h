#pragma once
#include <Process.h>
#include <dwmapi.h>
#include <iostream>
#include <sstream>
#include <thread>
#include <chrono>
#include <vector>
#include "UE4ActorWrapper.h"
namespace Core
{
	class SotCore
	{
	public:
		struct Vector {
		public:
			float x;
			float y;
			float z;
		};
		struct UE4Actor {
		public:
			std::string name;
			Vector pos;
			SoT::UE4Actor^ ActorToManagedActor();
		};

	public:
		SotCore();
		bool Prepare();
		Core::SotCore::UE4Actor GetLocalPlayer();
		std::vector<Core::SotCore::UE4Actor> getActors();

	};
}
