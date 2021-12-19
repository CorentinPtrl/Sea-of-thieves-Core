#include "pch.h"
#include "Core.h"
#include "UE4ActorWrapper.h"
#include <msclr\marshal_cppstd.h>
using namespace msclr::interop;

namespace SoT
{

	std::string ToUnmanagedString(String^ stringIncoming)
	{
		std::string unmanagedString = marshal_as<std::string>(stringIncoming);
		return unmanagedString;
	}

	AActor UE4Actor::getActor()
	{
		AActor actor;
		if (((int)this->IDActors) == -1)
		{
			actor = Core::SotCore::singleton->getLocalPlayer();
		}
		else
		{
			actor = Core::SotCore::singleton->TempActors[((int)this->IDActors)];
		}
		return actor;
	}

	bool UE4Actor::isValid()
	{
		AActor actor = getActor();
		std::string test = Core::SotCore::singleton->getNameFromIDmem(actor.GetID());
		std::string standardString = ToUnmanagedString(this->BaseName);
		return test.compare(standardString) == 0;
	}

	VectorUE4^ UE4Actor::getPos()
	{
		if (isValid())
		{

			return gcnew VectorUE4(getActor().GetRootComponent().GetPosition());
		}
	}

	VectorUE4^ UE4Actor::getRot()
	{
		if (isValid())
		{

			return gcnew VectorUE4(getActor().GetRootComponent().GetRotation());
		}
	}

	System::String^ UE4Actor::getName()
	{
		if (isValid())
		{
			AActor actor = getActor();
			std::string test = Core::SotCore::singleton->getNameFromIDmem(actor.GetID());
			return gcnew System::String(test.c_str());
		}
		return gcnew System::String("None");
	}

}