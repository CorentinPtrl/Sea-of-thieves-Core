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

}