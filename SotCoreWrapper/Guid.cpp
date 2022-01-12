#include "pch.h"
#include "Guid.h"
namespace SoT
{
	Guid::Guid(FGuid guid) : ManagedObject(&guid)
	{

	}

	int Guid::getA()
	{
		return this->GetInstance()->A;
	}

	int Guid::getB()
	{
		return this->GetInstance()->B;
	}

	int Guid::getC()
	{
		return this->GetInstance()->C;
	}

	int Guid::getD()
	{
		return this->GetInstance()->D;
	}

	System::Boolean^ Guid::operator==(Guid^ act)
	{
		return GetInstance() == act->GetInstance();
	}

	System::Boolean^ Guid::operator!=(Guid^ act)
	{
		return GetInstance() != act->GetInstance();
	}
}
