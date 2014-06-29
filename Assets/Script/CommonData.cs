using UnityEngine;
using System.Collections;

namespace CommonData
{
	public enum EnumAliasType
	{
		ALIAS_TYPE_NONE = -1,
		ALIAS_TYPE_ANIMAL,
		ALIAS_TYPE_HUMAN,
		ALIAS_TYPE_MAX
	};
	
	public enum EnumCharacterType
	{
		CHARACTER_TYPE_NONE = -1,
		CHARACTER_TYPE_UNITTOWER,
		CHARACTER_TYPE_ENEMYTOWER,
		CHARACTER_TYPE_RAT,
		CHARACTER_TYPE_HORSE,
		CHARACTER_TYPE_RAT1,
		CHARACTER_TYPE_ELEPHANT,
		CHARACTER_TYPE_ENEMY1,
		CHARACTER_TYPE_ENEMY2,
		CHARACTER_TYPE_MAX,
	};

	public enum EnumAnimationState
	{
		ANIMATION_STATE_NONE = -1,
		ANIMATION_STATE_MOVE,
		ANIMATION_STATE_LOAD,
		ANIMATION_STATE_ATTACK,
		ANIMATION_STATE_DEAD
	};

	public struct CharType
	{
		public EnumAliasType AliasType;
		public EnumCharacterType CharacterType;
	};

	public struct BulletContext
	{
		public int delay;			// 불릿 생성 딜레이
		public EnumCharacterType unitType;	// 불릿 생성한 유닛
		public EnumAliasType aliasType; // 불릿 생성한 유닛의 타입
		public int damage;			// 불릿의 데미지
		public int range;			// 불릿 사정거리
		public int speed;			// 불릿의 스피드
		public Vector3 position;    // 불릿 생성 위치
	};

	public struct UnitData
	{
		public int unitDelay; //생성 딜레이
		public int bulletDelay; //장전 시간
		public int power;  //파워
		public int range;  //공격 범위
		public int price;  //유닛 가격
		public float speed; //유닛 이동 속도 
		public int bulletSpeed; //불릿 발사 속도
		public float maxHp; //최대 에너지
	};



};