#pragma once
#include "process_manager.h"
#include "MemoryManager.h"
#include "offsets.h"
#include "vector.h"
#include <vector>
#include <cstdint>
#include <type_traits>

namespace Core
{

	class text
	{
	public:
		char word[64];
	};

	class textx
	{
	public:
		wchar_t word[64];
	};

	struct FGuid
	{
		int A; // 0x0000(0x0004) (Edit, ZeroConstructor, SaveGame, IsPlainOldData)
		int B; // 0x0004(0x0004) (Edit, ZeroConstructor, SaveGame, IsPlainOldData)
		int C; // 0x0008(0x0004) (Edit, ZeroConstructor, SaveGame, IsPlainOldData)I
		int D; // 0x000C(0x0004) (Edit, ZeroConstructor, SaveGame, IsPlainOldData)

		FGuid() : A(0), B(0), C(0), D(0) {}

		FGuid(int a, int b, int c, int d) : A(a), B(b), C(c), D(d) {}

		bool operator==(const FGuid& other)
		{
			return A == other.A && B == other.B && C == other.C && D == other.D;
		}

		bool operator!=(const FGuid& other)
		{
			return A != other.A || B != other.B || C != other.C || D != other.D;
		}

	};

	struct Camera
	{
		Vector3 position, angles;
		float fov;
	};

	struct Ships
	{
		FGuid crewID;
		std::string type;
	};

	template<class TEnum>
	class TEnumAsByte
	{
	public:
		inline TEnumAsByte()
		{
		}

		inline TEnumAsByte(TEnum _value)
			: value(static_cast<uint8_t>(_value))
		{
		}

		explicit inline TEnumAsByte(int32_t _value)
			: value(static_cast<uint8_t>(_value))
		{
		}

		explicit inline TEnumAsByte(uint8_t _value)
			: value(_value)
		{
		}

		inline operator TEnum() const
		{
			return (TEnum)value;
		}

		inline TEnum GetValue() const
		{
			return (TEnum)value;
		}

	private:
		uint8_t value;
	};


	template<class T>
	class TArray
	{
	public:
		int Length() const
		{
			return m_nCount;
		}

		bool IsValid() const
		{
			if (m_nCount > m_nMax)
				return false;
			if (!m_Data)
				return false;
			return true;
		}

		bool IsValidIndex(int32_t i) const
		{
			return i < m_nCount;
		}

		template<typename U = T>
		typename std::enable_if<std::is_pointer<U>::value, typename std::remove_pointer<U>::type>::type GetValue(int32_t index) const
		{
			auto offset = MemoryManager->Read<uintptr_t>(m_Data + sizeof(uintptr_t) * index);
			return MemoryManager->Read<typename std::remove_pointer<U>::type>(offset);
		}

		template<typename U = T>
		typename std::enable_if<!std::is_pointer<U>::value, U>::type GetValue(int32_t index) const
		{
			return MemoryManager->Read<U>(m_Data + sizeof(U) * index);
		}

		template<typename U = T, typename std::enable_if<std::is_pointer<U>::value, int32_t>::type = 0>
		uintptr_t GetValuePtr(int32_t index) const
		{
			return MemoryManager->Read<uintptr_t>(m_Data + sizeof(uintptr_t) * index);
		}

		template<typename U = T, typename std::enable_if<!std::is_pointer<U>::value, int32_t>::type = 0>
		uintptr_t GetValuePtr(int32_t index) const
		{
			return m_Data + sizeof(U) * index;
		}

		template<typename U = T>
		void SetValue(int32_t index, U value) const
		{
			return MemoryManager->Write(this->GetValuePtr(index), value);
		}

		template<typename U = T>
		typename std::enable_if<std::is_pointer<U>::value, typename std::remove_pointer<U>::type>::type operator[](int32_t index) const
		{
			return GetValue<U>(index);
		}

		template<typename U = T>
		typename std::enable_if<!std::is_pointer<U>::value, U>::type operator[](int32_t index) const
		{
			return GetValue<U>(index);
		}
	private:
		uintptr_t m_Data;
		int32_t m_nCount;
		int32_t m_nMax;
	};

	struct FQuat {
		float X;
		float Y;
		float Z;
		float W;
	};

	struct FTransform
	{
		struct FQuat Rotation;				// 0x0000(0x0010) (Edit, BlueprintVisible, SaveGame, IsPlainOldData)
		struct Vector3 Translation;			// 0x0010(0x000C) (Edit, BlueprintVisible, ZeroConstructor, SaveGame, IsPlainOldData)
		struct Vector3 Scale3D;				// 0x0020(0x000C) (Edit, BlueprintVisible, ZeroConstructor, SaveGame, IsPlainOldData)
		unsigned char UnknownData01[0x4];	// 0x002C(0x0004) MISSED OFFSET
	};

	struct FAlliance
	{
		struct FGuid AllianceId;			// 0x0000(0x0010) (ZeroConstructor, IsPlainOldData)
		TArray<struct FGuid> Crews;			// 0x0010(0x0010) (ZeroConstructor)
		unsigned char AllianceIndex;		// 0x0020(0x0001) (ZeroConstructor, IsPlainOldData)
		unsigned char UnknownData00[0x7];	// 0x0021(0x0007) MISSED OFFSET
	};

	class AAllianceService
	{
	public:
		TArray<struct FAlliance> GetAlliances();

	private:
		char __pad0x4B0[0x4B0];
		TArray<struct FAlliance> Alliances;
	};

	class Chunk
	{
		char __pad0x0[0x1000];
	};

	class USceneComponent
	{
	public:
		Vector3 GetPosition();
		Vector3 GetRotation();
	private:
		char __pad0x130[0x140];
		FTransform transform;
	};

	class APlayerState
	{
	public:
		std::wstring GetName();

	private:
		char __pad0x0[0x1000];
	};

	class UHealthComponent
	{
	public:
		int GetHealth();
		int GetMaxHealth();
	private:
		unsigned char UnknownData00[0xDD];
		float maxHealth;
		float health;
		unsigned char UnknownData01[0xFC];
	};

	class UItemDesc
	{
	public:
		std::wstring GetName();
	private:
		char __pad0x0[0x28];
		uintptr_t m_pName;
	};

	class AItemInfo
	{
	public:
		UItemDesc GetItemDesc();
	private:
		char __pad0x0[0x1000];
	};

	class AWieldableItem
	{
	public:
		AItemInfo GetItemInfo();
	private:
		char __pad0x0[0x1000];
	};

	class UWieldedItemComponent
	{
	public:
		AWieldableItem GetWieldedItem();
	public:
		char __pad0x0[0x1000];
	};

	class AActor
	{
	public:
		int GetID();
		USceneComponent GetRootComponent();
		APlayerState GetPlayerState();
		UWieldedItemComponent GetWieldedItemComponent();
		UHealthComponent GetHealthComponent();
		TArray<struct FIsland> AActor::GetIslandarray();

		bool operator==(const AActor& rhs) const
		{
			return *(uintptr_t*)(this->__pad0x0 + Offsets.AActor.rootComponent) == *(uintptr_t*)(rhs.__pad0x0 + Offsets.AActor.rootComponent);
		}

		bool operator!=(const AActor& rhs) const
		{
			return *(uintptr_t*)(this->__pad0x0 + Offsets.AActor.rootComponent) != *(uintptr_t*)(rhs.__pad0x0 + Offsets.AActor.rootComponent);
		}

	private:
		char __pad0x0[0x1000];
	};

	class UDrowningComponent
	{
	public:
		float GetOxygenLevel();

	private:
		char __pad0x0[0x108]; // oxygen offset
		float oxygenLevel;
	};

	class ACharacter
	{
	public:
		UDrowningComponent GetDrowningComponent();

	private:
		char __pad0x0[0x1000];
	};

	struct FCrew
	{
	public:
		TArray<class APlayerState*> GetPlayers();
		std::string GetShipType();
		FGuid GetCrewID();

	private:
		FGuid CrewID;
		char __pad0x10[0x10];
		TArray<class APlayerState*> Players;
		char __pad0x30[0x30];
		int maxPlayersOnShip;
		char __pad0x64[0x2C];
	};

	class ACrewService
	{
	public:
		TArray<struct FCrew> GetCrews();

	private:
		char __pad0x0[0x1000];
	};

	class AWindService
	{
	public:

	private:
		char __pax0x0[0x1000];
	};

	class UCrewOwnershipComponent
	{
	public:
		FGuid GetCrewId();
	private:
		char __pad0x0[0x100];
	};

	class AShip
	{
	public:
		UCrewOwnershipComponent GetCrewOwnershipComponent();
		uintptr_t GetOwningActor();
	private:
		char __pad0x0[0x1000];
	};

	class ULevel
	{
	public:
		TArray<Chunk*> GetActors() const;
	private:
		char __pad0xA0[0xA0];
		TArray<Chunk*> m_Actors;
	};

	class AFauna
	{
	public:
		std::wstring GetName();
	private:
		char __pad0x0[0x1000];
	};

	class APlayerCameraManager
	{
	public:
		Vector3 GetCameraPosition();
		Vector3 GetCameraRotation();
		float	GetCameraFOV();
	private:
		char __pad0x0[0x450];
		Vector3 position;
		Vector3 rotation;
		char __pad0x10[0x10];
		float fov;
	};

	struct FName
	{
		int nameId;
		char __pad0x4[0x4];
	};

	class APlayerController
	{
	public:
		AActor GetActor();
		ACharacter GetCharacter();
		APlayerCameraManager GetCameraManager();
		Vector3 GetPlayerAngles();
		APlayerState GetPlayerState();

	public:
		char __pad0x0[0x1000];
	};

	class ULocalPlayer
	{
	public:
		APlayerController GetPlayerController();
		void SetPlayerAngles(Vector3 angles);
	private:
		char __pad0x0[0x1000];
	};

	class UGameInstance
	{
	public:
		ULocalPlayer GetLocalPlayer();
	private:
		char __pad0x0[0x100];
	};

	class cUWorld
	{
	public:
		ULevel GetLevel()  const;
		UGameInstance GetGameInstance()  const;
	public:
		char __pad0x0[0x1000];
	};

	struct FXMarksTheSpotMapMark
	{
		struct Vector2 Position; // 0x0000(0x0008) (BlueprintVisible, ZeroConstructor, IsPlainOldData)
		float          Rotation; // 0x0008(0x0004) (BlueprintVisible, ZeroConstructor, IsPlainOldData)
	};

	class AXMarksTheSpotMap
	{
	public:
		TArray<struct FXMarksTheSpotMapMark> GetMarks();

	private:
		char __pad0x0[0x08];
		TArray<struct FXMarksTheSpotMapMark> Marks;
	};


	class ABootyItemInfo
	{
	public:
		UItemDesc GetItemDesc();
		int GetBootyType();
		int GetRareityId();
	private:
		char __pad0x0[0x1000];
	};

	class AItemProxy
	{
	public:
		ABootyItemInfo  GetBootyItemInfo();
	private:
		char __pad0x0[0x1000];
	};

	class UObject
	{
	public:
		char __pad0x0[0x18];
		int nameId;
	};


	class AllWorlds
	{
	public:
		TArray <class ULevel*> GetAllLevels();

	private:
		char __pad0x0[0x0150];
		TArray <class ULevel*> AllLevels;
	};

	struct Player
	{
		AActor actor;
		float health, maxhealth;
		int distance;
		FGuid crewID, allyID;
		std::string name, heldItem;
	};


	class cSOT
	{
	public:
		Player localPlayer;
		Player Pirates[24];
		Camera localCamera;
		Ships  Ships[6];
		std::vector<Vector2> XMarks;
	};


	extern cSOT* SOT;
}