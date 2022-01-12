#include "pch.h"
#include "CrewService.h"
namespace SoT
{
	CrewService::CrewService(UE4Actor^ actor) : UE4Actor(actor)
	{
		if (!singleton)
			singleton = this;
	}
		
	ACrewService CrewService::getActor()
	{
		return *reinterpret_cast<ACrewService*>(&UE4Actor::getActor());
	}

	array<Crew^>^ CrewService::getCrews()
	{
		TArray<FCrew> crews = getActor().GetCrews();
		array<Crew^>^ CrewsArray = gcnew array<Crew^>(crews.Length());
		for (int i = 0; i < crews.Length(); i++)
		{
			for (int d = 0; d < crews[i].GetPlayers().Length(); d++)
			{
				System::String^ name = gcnew System::String(crews[i].GetPlayers()[d].GetName().c_str());
				d = d;
			}
			CrewsArray[i] = gcnew Crew(crews[i]);
		}
		return CrewsArray;
	}
}