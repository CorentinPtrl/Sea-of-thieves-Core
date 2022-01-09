#pragma once
#include "ManagedObject.h"
#include "SOTStuff.h"
#include "VectorUE4.h"

namespace SoT
{
	public ref class CameraManager : public ManagedObject<Core::APlayerCameraManager>
	{
		private:
			VectorUE4^ pos;
			VectorUE4^ rot;
			bool isValid();
		public:
			CameraManager(Core::APlayerCameraManager act);
			VectorUE4^ getPos();
			VectorUE4^ getRot();
			float getFOV();

	};
}

