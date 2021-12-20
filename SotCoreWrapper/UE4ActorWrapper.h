#pragma once
#include <string>
#include "VectorUE4.h"
using namespace System;
namespace SoT
{
	public ref class UE4Actor
	{
	private:
		bool isValid();
		AActor UE4Actor::getActor();
	public:
		System::String^ getName();
		VectorUE4^ UE4Actor::getPos();
		VectorUE4^ UE4Actor::getRot();
		System::Boolean^ operator==(UE4Actor^ act);
		System::Boolean^ operator!=(UE4Actor^ act);
		System::String^ BaseName;
		System::Int32^ IDActors;
	};
}


