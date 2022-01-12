#include "pch.h"
#include "SOTStuff.h"
#include <clocale>
#include <cstdlib>
using namespace System;

namespace Core
{

	cSOT* SOT = new cSOT();

	offsets Offsets;

	Vector3 USceneComponent::GetPosition()
	{
		return this->transform.Translation;
	}

	Vector3 USceneComponent::GetRotation()
	{
		return Vector3(this->transform.Rotation.X, this->transform.Rotation.Y, this->transform.Rotation.Z);
	}

	int AActor::GetID()
	{
		return *(int*)(__pad0x0 + Offsets.AActor.actorId);
	}

	USceneComponent AActor::GetRootComponent()
	{
		return MemoryManager->Read<USceneComponent>(*(uintptr_t*)(__pad0x0 + Offsets.AActor.rootComponent));
	}

	APlayerState AActor::GetPlayerState()
	{
		return MemoryManager->Read<APlayerState>(*(uintptr_t*)(__pad0x0 + Offsets.AActor.PlayerState));
	}


	UWieldedItemComponent AActor::GetWieldedItemComponent()
	{
		return MemoryManager->Read < UWieldedItemComponent>(*(uintptr_t*)(__pad0x0 + Offsets.AActor.WieldedItemComponent));
	}

	UHealthComponent AActor::GetHealthComponent()
	{
		return MemoryManager->Read< UHealthComponent>(*(uintptr_t*)(__pad0x0 + Offsets.AActor.HealthComponent));
	}

	TArray<struct FIsland> AActor::GetIslandarray()
	{
		return MemoryManager->Read< TArray<struct FIsland>>(*(uintptr_t*)(__pad0x0 + 0x0470));
	}

	UDrowningComponent ACharacter::GetDrowningComponent()
	{
		return MemoryManager->Read< UDrowningComponent>(*(uintptr_t*)(__pad0x0 + Offsets.AActor.DrowningComponent));
	}

	TArray<Chunk*> ULevel::GetActors() const
	{
		return this->m_Actors;
	}

	Vector3 APlayerCameraManager::GetCameraPosition()
	{
		return this->position;
	}

	Vector3 APlayerCameraManager::GetCameraRotation()
	{
		return this->rotation;
	}

	float APlayerCameraManager::GetCameraFOV()
	{
		return this->fov;
	}

	AActor APlayerController::GetActor()
	{
		return MemoryManager->Read<AActor>(*(uintptr_t*)(__pad0x0 + Offsets.APlayerController.Pawn));
	}

	ACharacter APlayerController::GetCharacter()
	{
		return MemoryManager->Read<ACharacter>(*(uintptr_t*)(__pad0x0 + Offsets.APlayerController.Character));
	}

	APlayerCameraManager APlayerController::GetCameraManager()
	{
		return MemoryManager->Read<APlayerCameraManager>(*(uintptr_t*)(__pad0x0 + Offsets.APlayerController.CameraManager));
	}

	APlayerController ULocalPlayer::GetPlayerController()
	{
		return MemoryManager->Read<APlayerController>(*(uintptr_t*)(__pad0x0 + Offsets.ULocalPlayer.PlayerController));
	}

	ULocalPlayer UGameInstance::GetLocalPlayer()
	{
		return MemoryManager->Read<ULocalPlayer>(MemoryManager->Read<uintptr_t>(*(uintptr_t*)(__pad0x0 + Offsets.UGameInstance.LocalPlayers)));
	}

	ULevel cUWorld::GetLevel() const
	{
		return MemoryManager->Read<ULevel>(*(uintptr_t*)(__pad0x0 + Offsets.UWorld.PersistentLevel));
	}

	UGameInstance cUWorld::GetGameInstance() const
	{
		return MemoryManager->Read<UGameInstance>(*(uintptr_t*)(__pad0x0 + Offsets.UWorld.OwningGameInstance));
	}

	TArray<struct FAlliance> AAllianceService::GetAlliances()
	{
		return this->Alliances;
	}

	TArray<struct FCrew> ACrewService::GetCrews()
	{
		return *(TArray<struct FCrew>*)(__pad0x0 + Offsets.ACrewService.Crews);
	}

	TArray<class APlayerState*> FCrew::GetPlayers()
	{
		return this->Players;
	}

	std::string FCrew::GetShipType()
	{

		switch (this->maxPlayersOnShip)
		{
		case 2:
			return "Sloop";
			break;
		case 3:
			return "Brigantine";
			break;
		case 4:
			return "Galleon";
			break;
		default:
			return "";
			break;
		}
	}

	FGuid FCrew::GetCrewID()
	{
		return this->CrewID;
	}

	std::wstring APlayerState::GetName()
	{
		return MemoryManager->Read<textx>(*(uintptr_t*)(__pad0x0 + Offsets.APlayerState.PlayerName)).word;
	}

	int UHealthComponent::GetHealth()
	{
		return this->health;
	}

	int UHealthComponent::GetMaxHealth()
	{
		return this->maxHealth;
	}

	float UDrowningComponent::GetOxygenLevel()
	{
		return this->oxygenLevel;
	}


	FGuid UCrewOwnershipComponent::GetCrewId()
	{
		return *(FGuid*)(__pad0x0 + Offsets.UCrewOwnershipComponent.CrewId);
	}

	UCrewOwnershipComponent AShip::GetCrewOwnershipComponent()
	{
		return MemoryManager->Read<UCrewOwnershipComponent>(*(uintptr_t*)(__pad0x0 + Offsets.AShip.CrewOwnershipComponent));
	}

	uintptr_t AShip::GetOwningActor()
	{
		return *(uintptr_t*)(__pad0x0 + Offsets.AShip.ShipOwningActor);
	}

	USinkingComponent AShip::GetSinkingComponent()
	{
		return MemoryManager->Read<USinkingComponent>(*(uintptr_t*)(__pad0x0 + Offsets.AShip.SinkingComponent));
	}


	TArray<struct FXMarksTheSpotMapMark> AXMarksTheSpotMap::GetMarks()
	{
		return this->Marks;
	}

	ABootyItemInfo AItemProxy::GetBootyItemInfo()
	{
		return MemoryManager->Read<ABootyItemInfo>(*(uintptr_t*)(__pad0x0 + Offsets.AItemProxy.AItemInfo));
	}

	UItemDesc ABootyItemInfo::GetItemDesc()
	{
		return MemoryManager->Read<UItemDesc>(*(uintptr_t*)(__pad0x0 + Offsets.AItemInfo.UItemDesc));
	}

	int ABootyItemInfo::GetBootyType()
	{
		return *(int*)(__pad0x0 + Offsets.ABootyItemInfo.BootyType);
	}

	int ABootyItemInfo::GetRareityId()
	{
		return *(int*)(__pad0x0 + Offsets.ABootyItemInfo.Rarity);
	}

	std::wstring UItemDesc::GetName()
	{
		return MemoryManager->Read<textx>(MemoryManager->Read<uintptr_t>(this->m_pName)).word;
	}

	AWieldableItem UWieldedItemComponent::GetWieldedItem()
	{
		return MemoryManager->Read <AWieldableItem>(*(uintptr_t*)(__pad0x0 + Offsets.UWieldedItemComponent.WieldedItem));
	}

	AItemInfo AWieldableItem::GetItemInfo()
	{
		return  MemoryManager->Read <AItemInfo>(*(uintptr_t*)(__pad0x0 + Offsets.AWieldableItem.ItemInfo));
	}

	UItemDesc AItemInfo::GetItemDesc()
	{
		return MemoryManager->Read <UItemDesc>(*(uintptr_t*)(__pad0x0 + Offsets.AItemInfo.UItemDesc));
	}

	std::wstring AFauna::GetName()
	{
		return MemoryManager->Read<textx>(MemoryManager->Read<uintptr_t>(*(uintptr_t*)(__pad0x0 + Offsets.AFauna.Name))).word;
	}

	TArray<class ULevel*> AllWorlds::GetAllLevels()
	{
		return this->AllLevels;
	}
}