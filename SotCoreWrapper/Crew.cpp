#include "pch.h"
#include "Crew.h"
#include "CrewService.h"
#include "SOTStuff.h"
namespace SoT
{
	Crew::Crew(FCrew crew)
	{
		CrewID = gcnew Guid(crew.GetCrewID());
		TArray<APlayerState*> playersCrew = crew.GetPlayers();
		players = gcnew array< String^ >(playersCrew.Length());
		for (int i = 0; i < playersCrew.Length(); i++)
		{
			players[i] = gcnew System::String(playersCrew[i].GetName().c_str());
		}
		shipType = gcnew System::String(crew.GetShipType().c_str());
	}

	array<String^>^ Crew::getPlayers()
	{
		return players;
	}

	System::String^ Crew::getShipType()
	{
		return shipType;
	}

	Guid^ Crew::getCrewID()
	{
		return CrewID;
	}
}