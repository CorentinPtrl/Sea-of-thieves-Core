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

	bool UE4Actor::isValid()
	{
		AActor actor = Core::SotCore::singleton->TempActors[(int)this->IDActors];
		std::string test = Core::SotCore::singleton->getNameFromIDmem(actor.GetID());
		std::string standardString = ToUnmanagedString(this->BaseName);
		return test.compare(standardString) == 0;
	}

	VectorUE4^ UE4Actor::getPos()
	{
		if (isValid())
		{
			AActor actor = Core::SotCore::singleton->TempActors[(int)this->IDActors];
			return gcnew VectorUE4(actor.GetRootComponent().GetPosition());
		}
	}

	System::String^ UE4Actor::getName()
	{
		if (isValid())
		{
			AActor actor = Core::SotCore::singleton->TempActors[(int)this->IDActors];
			std::string test = Core::SotCore::singleton->getNameFromIDmem(actor.GetID());
			return gcnew System::String(test.c_str());
		}
		return gcnew System::String("NonePasCool");
	}

}