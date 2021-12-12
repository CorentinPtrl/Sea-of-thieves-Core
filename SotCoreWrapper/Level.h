#pragma once
#include "globals.h"
#include "MemoryManager.h"
#include "process_manager.h"
#include "SotStuff.h"
#include "VectorUE4.h"
namespace Core
{
	class Level
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
		};
		std::vector<Core::Level::UE4Actor> getActors();

	};

}