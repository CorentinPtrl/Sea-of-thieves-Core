#pragma once
#include <string>
#include "VectorUE4.h"
#include <msclr\marshal_cppstd.h>
using namespace msclr::interop;

using namespace System;
using namespace Core;


namespace SoT
{
	public ref class UE4Actor
	{
	private:
		VectorUE4^ pos;
		VectorUE4^ rot;
		bool isValid();
	internal:
		AActor UE4Actor::getActor();
	public:
		UE4Actor(UE4Actor^ actor);
		UE4Actor(std::string BaseName, int IDActors);
		System::String^ getName();
		VectorUE4^ UE4Actor::getPos();
		VectorUE4^ UE4Actor::getRot();
		System::Boolean^ operator==(UE4Actor^ act);
		System::Boolean^ operator!=(UE4Actor^ act);
		System::String^ BaseName;
		System::Int32^ IDActors;
	};
}


