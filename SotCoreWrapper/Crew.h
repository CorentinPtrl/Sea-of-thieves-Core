#pragma once
#include "Core.h"
#include "Player.h"
#include "Guid.h"
#include "ManagedObject.h"

using namespace Core;
namespace SoT
{
	public ref class Crew
	{
		private:
			Guid^ CrewID;
			array<String^>^ players;
			System::String^ shipType;

		public:
			Crew(FCrew crew);
			array<String^>^ getPlayers();
			System::String^ getShipType();
			Guid^ getCrewID();

	};
}

