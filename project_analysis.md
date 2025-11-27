# OneWeapon 프로젝트 분석 보고서

## 1. 전체 아키텍처 (Overall Architecture)

이 프로젝트는 매우 체계적이고 확장 가능하며 데이터 기반 아키텍처를 기반으로 구축되었습니다. 각 핵심 게임 플레이 시스템(이동, 스탯, 스킬)은 서로 다른 시스템에 최소한의 영향을 미치면서 독립적으로 작동하고 수정될 수 있도록 명확하게 분리되어 있습니다.

주요 디자인 패턴과 원칙은 다음과 같습니다.

*   **컴포넌트 기반 아키텍처 (Component-based Architecture):** 각 주요 기능은 `MonoBehaviour` "Helper" 컴포넌트(`MoveHelper`, `StatHelper`, `SkillHelper`)로 캡슐화됩니다.
*   **데이터 기반 디자인 (Data-Driven Design):** 게임의 핵심 파라미터(캐릭터 스탯, 스킬 속성, 이동 방식 등)는 코드에서 분리되어 `ScriptableObject` "Asset" (`MoveAsset`, `StatAsset`, `SkillAsset`)에 저장됩니다. 이를 통해 디자이너는 코드를 수정하지 않고도 게임의 밸런스를 쉽게 조정하고 콘텐츠를 제작할 수 있습니다.
*   **명령 패턴 (Command Pattern):** 이동 및 스킬 사용과 같은 액션은 `Command` 객체(`MovePositionCommand`, `SkillUseCommand`)로 래핑되어 중앙 `CommandManager`에 의해 실행됩니다. 이 패턴은 액션의 실행을 분리하여 향후 리플레이, 실행 취소 또는 로깅 기능을 추가할 수 있는 유연성을 제공합니다.
*   **상태 패턴 (State Pattern):** 스킬 시스템은 스킬의 라이프사이클(예: `Enable`, `InProgress`, `CoolTime`)을 관리하기 위해 상태 패턴을 광범위하게 사용합니다. 각 상태는 자체 로직을 캡슐화하여 복잡한 행동을 체계적으로 관리합니다.
*   **싱글톤 패턴 (Singleton Pattern):** `InputManager` 및 `CommandManager`와 같은 전역 시스템은 싱글톤으로 구현되어 프로젝트의 모든 곳에서 쉽게 접근할 수 있습니다.
*   **Helper/Context/Controller 패턴:** 각 시스템은 유사한 내부 구조를 따릅니다.
    *   **Helper:** `MonoBehaviour` 컴포넌트로, 시스템의 공용 진입점 역할을 합니다.
    *   **Controller:** 단일 인스턴스(예: 특정 스킬 또는 스탯)의 런타임 로직을 관리하는 일반 C# 클래스입니다.
    *   **Context:** 특정 인스턴스의 데이터와 상태를 보유하는 일반 C# 클래스입니다.

## 2. 핵심 시스템 (Core Systems)

### 입력 시스템 (Input System)

*   Unity의 새로운 입력 시스템 (`UnityEngine.InputSystem`)을 사용합니다.
*   `InputManager` 싱글톤은 모든 원시 입력을 중앙에서 처리합니다.
*   입력은 C# 이벤트 (`OnActMove`, `OnActClick` 등)를 통해 다른 시스템에 브로드캐스트됩니다.
*   `SkillInputAdapter`는 마우스 클릭과 같은 특정 입력을 스킬 시스템이 이해할 수 있는 "슬롯" 인덱스로 변환하는 어댑터 역할을 합니다.

### 이동 시스템 (Movement System)

*   물리 기반 이동을 위해 `Rigidbody2D`를 사용합니다.
*   `MoveHelper`는 캐릭터의 이동 로직을 관리하는 메인 컴포넌트입니다.
*   `MoveAsset` ScriptableObject는 이동 속도, 점프력, 지면 레이어 등과 같은 모든 이동 파라미터를 정의합니다.
*   `MoveContext`는 `InputManager`의 이벤트를 구독하고 `MoveHelper`에 이동 또는 점프를 요청합니다.
*   실제 이동 및 점프 액션은 `MovePositionCommand` 및 `MoveJumpCommand`를 통해 실행됩니다.

### 스탯 시스템 (Stat System)

*   `StatHelper` 컴포넌트는 캐릭터의 모든 스탯을 관리합니다.
*   각 스탯(예: "MoveSpeed", "Hp")은 자체 `StatAsset` ScriptableObject에 의해 정의되며, 기본값과 유형(예: 백분율)을 포함합니다.
*   `StatHelper`는 런타임에 각 `StatAsset`에 대해 `StatController`를 생성합니다.
*   다른 시스템은 `StatHelper.GetValue("StatCodename", out value)`를 호출하여 스탯 값을 쿼리할 수 있습니다.
*   **참고:** 현재 시스템은 스탯 수정자(버프/디버프)를 구현하지 않지만, `StatController` 클래스의 존재는 향후 이러한 기능을 추가할 수 있도록 설계되었음을 시사합니다.

### 스킬 시스템 (Skill System)

*   이 프로젝트에서 가장 복잡하고 잘 설계된 시스템입니다.
*   `SkillHelper`는 캐릭터에 할당된 모든 스킬을 관리합니다.
*   `SkillAsset`은 스킬의 속성(입력 슬롯, 제어 유형, 상호 작용 유형 등)을 정의합니다.
*   `SkillController`는 상태 패턴을 사용하여 개별 스킬의 라이프사이클을 관리합니다.
*   `ISkillState` 인터페이스와 그 구현(`SkillEnable`, `SkillInProgress`, `SkillCoolTime` 등)은 스킬의 각 상태에 대한 행동을 정의합니다.
*   스킬 사용은 `SkillUseCommand`를 통해 처리되어 명령 패턴의 일관성을 유지합니다.

## 3. 데이터 관리 (Data Management)

프로젝트는 ScriptableObjects를 광범위하게 사용하여 코드와 데이터를 명확하게 분리합니다.

*   모든 핵심 게임 플레이 데이터는 `Assets/__OneWeapon/3.So` 폴더에 ScriptableObject 에셋으로 저장됩니다.
*   폴더 구조는 시스템별(`Move`, `Skill`, `Stat`)로 깔끔하게 정리되어 있습니다.
*   이 아키텍처는 빠른 프로토타이핑과 반복을 용이하게 하며, 디자이너가 프로그래머의 개입 없이 게임을 수정하고 밸런스를 맞출 수 있도록 지원합니다.

## 4. 코드 품질 및 패턴 (Code Quality and Patterns)

*   **일관성 (Consistency):** 코드는 매우 일관된 명명 규칙과 구조를 따릅니다. `Helper`, `Context`, `Controller`, `Asset` 패턴은 모든 핵심 시스템에서 일관되게 사용됩니다.
*   **분리 (Decoupling):** 시스템들은 잘 분리되어 있습니다. 예를 들어, 이동 시스템은 스탯 시스템에 대해 거의 알지 못하며, 단지 `StatHelper`를 통해 값을 요청할 뿐입니다. 이벤트 기반 통신은 이러한 분리를 더욱 강화합니다.
*   **확장성 (Extensibility):** 현재 아키텍처는 새로운 기능 추가를 용이하게 합니다. 예를 들어, 새로운 스킬을 추가하는 것은 새로운 `SkillAsset`을 만들고, 필요한 경우 새로운 `ISkillState` 구현을 추가하는 것만으로 가능합니다. 스탯 수정자 시스템도 기존 구조 위에 쉽게 구축할 수 있습니다.
*   **가독성 (Readability):** 코드는 명확하고 잘 구성되어 있으며, `[cskim]` 태그와 함께 주석이 달려 있어 특정 결정에 대한 컨텍스트를 제공합니다.

## 5. 잠재적인 개선 영역 (Potential Improvements)

현재 아키텍처는 매우 견고하지만, 다음과 같은 몇 가지 영역을 고려해 볼 수 있습니다.

*   **스탯 수정자 시스템 (Stat Modifier System):** `StatController`를 확장하여 버프, 디버프 및 장비로 인한 스탯 변경을 처리하는 수정자 시스템을 구현합니다. 이는 모든 RPG 또는 액션 게임의 핵심 기능입니다.
*   **`IdentifiedObject`의 자동 코드 생성 (Automatic CodeName for `IdentifiedObject`):** `IdentifiedObject`의 `CodeName`이 현재 수동으로 설정되는 것으로 보입니다. `OnValidate`에서 에셋의 파일 이름을 기반으로 `CodeName`을 자동으로 설정하는 에디터 스크립트를 만들면 오류를 줄이고 일관성을 보장할 수 있습니다.
*   **의존성 주입 (Dependency Injection):** 현재 프로젝트는 싱글톤(`InputManager`, `CommandManager`)을 통해 의존성을 해결하고 `GetComponent`를 암시적으로 사용합니다. 더 큰 프로젝트에서는 Zenject나 VContainer와 같은 의존성 주입 프레임워크를 도입하여 컴포넌트 간의 결합을 더욱 줄이고 테스트 용이성을 향상시킬 수 있습니다.
*   **오브젝트 풀링 (Object Pooling):** 스킬 효과나 발사체와 같이 자주 생성되고 파괴되는 `GameObject`가 있다면, 성능 향상을 위해 오브젝트 풀링 시스템을 구현하는 것이 좋습니다.
*   **`Unit` 클래스의 재도입 (Re-introduction of `Unit` class):** 초기 `codebase_investigator` 분석에서 제안된 `Unit` 클래스는 여전히 유효한 개념입니다. 모든 `Helper` 컴포넌트(`MoveHelper`, `StatHelper`, `SkillHelper`)를 보유하고 초기화하는 중앙 `Unit` 클래스를 만들면 캐릭터 프리팹 설정이 단순화되고 다양한 컴포넌트 간의 상호 작용을 조정하는 중앙 지점을 제공할 수 있습니다. 현재 `UnitController.cs`는 비어 있지만, 이러한 역할을 수행하도록 리팩토링할 수 있습니다.
