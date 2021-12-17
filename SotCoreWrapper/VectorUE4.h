#pragma once
#include "pch.h"
#include "ManagedObject.h"
#include "vector.h"
namespace SoT
{
	public ref class VectorUE4 : public ManagedObject<Vector3>
	{
	public:
		VectorUE4(Vector3 vec);
		float getX();
		float getY();
		float getZ();

	};
}