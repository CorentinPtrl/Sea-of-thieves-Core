#include "pch.h"
#include "VectorUE4.h"
namespace SoT
{
	VectorUE4::VectorUE4(Vector3 vec) : ManagedObject(&vec)
	{

	}

	float VectorUE4::getX()
	{
		return this->m_Instance->x;
	}

	float VectorUE4::getY()
	{
		return this->m_Instance->y;
	}

	float VectorUE4::getZ()
	{
		return this->m_Instance->z;
	}

	float VectorUE4::Length()
	{
		return this->m_Instance->Length();
	}

	float VectorUE4::LengthSqr(void)
	{
		return this->m_Instance->LengthSqr();
	}

	float VectorUE4::Length2d()
	{
		return this->m_Instance->Length2d();
	}

	float VectorUE4::Length2dSqr()
	{
		return this->m_Instance->Length2dSqr();
	}

	float VectorUE4::DistTo(VectorUE4^ v)
	{
		return this->m_Instance->DistTo(*(v->GetInstance()));
	}

	float VectorUE4::DistToSqr(VectorUE4^ v)
	{
		return this->m_Instance->DistToSqr(*(v->GetInstance()));
	}

	float VectorUE4::Dot(VectorUE4^ v)
	{
		return this->m_Instance->Dot(*(v->GetInstance()));
	}

	VectorUE4^ VectorUE4::Cross(VectorUE4^ v)
	{
		return gcnew VectorUE4(this->m_Instance->Cross(*(v->GetInstance())));
	}

	System::Boolean^ VectorUE4::IsZero()
	{
		return gcnew System::Boolean(this->m_Instance->IsZero());
	}

}