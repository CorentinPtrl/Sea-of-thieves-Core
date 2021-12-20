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
		float Length();
		float LengthSqr();
		float Length2d();
		float Length2dSqr();
		float DistTo(VectorUE4^ v);
		float DistToSqr(VectorUE4^ v);
		float Dot(VectorUE4^ v);
		VectorUE4^ Cross(VectorUE4^ v);
		System::Boolean^ IsZero();
	};
}