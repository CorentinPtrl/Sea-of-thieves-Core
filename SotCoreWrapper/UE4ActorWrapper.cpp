#include "pch.h"
#include "Core.h"
#include "UE4ActorWrapper.h"

namespace SoT
{

	UE4Actor::UE4Actor(UE4Actor^ actor)
	{
		this->BaseName = actor->BaseName;
		this->IDActors = actor->IDActors;
	}

	UE4Actor::UE4Actor(std::string BaseName, int IDActors)
	{
		this->BaseName = gcnew System::String(BaseName.c_str());
		this->IDActors = gcnew System::Int32(IDActors);
	}

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
			if (this->pos == nullptr)
			{
				this->pos = gcnew VectorUE4(getActor().GetRootComponent().GetPosition());
				return pos;
			}
			else if (*(uintptr_t*)(this->pos->GetInstance()) == *(uintptr_t*)(&getActor().GetRootComponent().GetPosition()))
			{
				return this->pos;
			}
			else
			{
				this->pos->updateInstance(getActor().GetRootComponent().GetPosition());
				return pos;
			}
		}
	}

	VectorUE4^ UE4Actor::getRot()
	{
		if (isValid())
		{
			if (this->rot == nullptr)
			{
				this->rot = gcnew VectorUE4(getActor().GetRootComponent().GetRotation());
				return rot;
			}
			else if (*(uintptr_t*)(this->rot->GetInstance()) == *(uintptr_t*)(&getActor().GetRootComponent().GetRotation()))
			{
				return this->rot;
			}
			else
			{
				this->rot->updateInstance(getActor().GetRootComponent().GetRotation());
				return rot;
			}
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

	System::Boolean^ UE4Actor::operator==(UE4Actor^ act)
	{
		return this->getActor() == act->getActor();
	}

	System::Boolean^ UE4Actor::operator!=(UE4Actor^ act)
	{
		return this->getActor() != act->getActor();
	}

}