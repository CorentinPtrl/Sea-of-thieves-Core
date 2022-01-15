#include "pch.h"
#include "SotCore.h"
#include "CameraManager.h"
namespace SoT
{
	CameraManager::CameraManager(Core::APlayerCameraManager act) : ManagedSoTObject(&act)
	{

	}

	bool CameraManager::isValid()
	{
		return (*(uintptr_t*)(GetInstance()) == *(uintptr_t*)(&Core::SotCore::singleton->GetCameraManager()));
	}

	VectorUE4^ CameraManager::getPos()
	{
		if (isValid())
		{
			
			if (this->pos == nullptr)
			{
				this->pos = gcnew VectorUE4(GetInstance()->GetCameraPosition());
				return pos;
			}
			else if (*(uintptr_t*)(this->pos->GetInstance()) == *(uintptr_t*)(&GetInstance()->GetCameraPosition()))
			{
				return this->pos;
			}
			else
			{
				this->pos->UpdateInstance(&GetInstance()->GetCameraPosition());
				return pos;
			}
		}
		else
		{
			this->m_Instance = &Core::SotCore::singleton->GetCameraManager();
			return getPos();
		}
	}

	VectorUE4^ CameraManager::getRot()
	{
		if (isValid())
		{

			if (this->rot == nullptr)
			{
				this->rot = gcnew VectorUE4(GetInstance()->GetCameraRotation());
				return rot;
			}
			else if (*(uintptr_t*)(this->rot->GetInstance()) == *(uintptr_t*)(&GetInstance()->GetCameraRotation()))
			{
				return this->rot;
			}
			else
			{
				this->rot->UpdateInstance(&GetInstance()->GetCameraRotation());
				return rot;
			}
		}
		else
		{
			this->m_Instance = &Core::SotCore::singleton->GetCameraManager();
			return getRot();
		}
	}

	float CameraManager::getFOV()
	{
		if (isValid())
		{
			return GetInstance()->GetCameraFOV();
		}
		else
		{
			this->m_Instance = &Core::SotCore::singleton->GetCameraManager();
			return getFOV();
		}
	}

}