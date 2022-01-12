#pragma once
#include "SOTStuff.h"
#include "ManagedObject.h"
using namespace Core;

namespace SoT
{
	public ref class Guid : public ManagedObject<FGuid>
	{
	public:
		Guid(FGuid guid);
		int getA();
		int getB();
		int getC();
		int getD();

		System::Boolean^ operator==(Guid^ act);
		System::Boolean^ operator!=(Guid^ act);
	};
}

