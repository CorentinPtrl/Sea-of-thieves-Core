#pragma once
#include "Core.h"
#include "Crew.h"
#include "UE4ActorWrapper.h"
namespace SoT
{
	public ref class CrewService : UE4Actor
	{
		internal:
			ACrewService getActor();
		public:
			static CrewService^ singleton;
			CrewService(UE4Actor^ actor);
			array<Crew^>^ getCrews();
	};
}

