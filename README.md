# WinTween

리듬닥터의 창 움직이는 연출을 아마도 간단하게 만들 수 있는 코드 덩어리에요.</br>
아직 개발중이라 조금 아쉬우거나 사용하기 애매할 수 있긴 합니다.</br>
주 모니터 기준으로 움직입니다.</br>
</br>
WinTween makes Window-moving-effects more easier then raw WinAPI.</br>
It's still developing, so this could be little disappointing or bit awkward to use.</br>
Now, WinTween only works with your main monitor.</br>
</br>
### 버그는 Issues 에 남겨주면 고쳐드리겟슴
### Bug, (Issues) => { fixed; }

* * *

### 사용법 / How to use

1. WindowCore 와 WindowEffects 를 유니티 프로젝트에 추가합니다.
2. WindowEffects 를 게임오브젝트 어딘가에다가 붙여둡니다.
3. 네임스페이스를 사용할 스크립트에게 추가합니다.

* * *
1. Add WindowCore and WindowEffects to your unity project
2. Add WindowEffects to random GameObject
3. Add namespace to your script

<br></br>
#### 윈도우의 창 위치 기준점은 창의 정 중앙이 아닌 왼쪽 위를 기준으로 잡습니다.
#### Window origin position works same with WinAPI
*(960,540) 으로 위치를 설정하면 모니터 중앙에 위치하지 않는다는 의미.*

* * *

### Namesapce
```cs
// Base
using WinTween;

// Position effect
using WinTween.Position;

// Scale effect
using WinTween.Scale;

```


### 변수들 / variables
*English comments at code*

```cs
int LeftPosX;
// 모니터의 왼쪽 x 좌표

int RightPosX;
// 창이 오른쪽에 딱 붙을때의 x 좌표

int MidPosX;
// 창이 정 중앙에 있을때의 x 좌표

int MidPosY;
// 창이 정 중앙에 있을때의 y 좌표

int TopPosY;
// 모니터의 꼭대기 y 좌표

int BottomPosY;
// 창이 아레에 딱 붙을때의 y 좌표

Vector2Int ScreenSize;
// 현재 게임 화면의 크기

Vector2Int TopCenter;
Vector2Int TopLeft;
Vector2Int TopRight;
// 창이 모니터 맨 위쪽에 있을 수 있는 기준 좌표들

Vector2Int MiddleCenter;
Vector2Int MiddleLeft;
Vector2Int MiddleRight;
// 창이 모니터 정 중앙에 있을 수 있는 기준 좌표들

Vector2Int BottomCenter;
Vector2Int BottomLeft;
Vector2int BottomRight;
// 창이 모니터 맨 아레에 있을 수 있는 기준 좌표들

```

### 함수들 / Functions
*English comments at code*

```cs

public Vector2Int GetLocation();
// 현재 윈도우의 위치를 반환합니다.

public Vector2Int GetCurrnetSize();
// 현재 윈도우의 크기를 반환합니다.
// x = 가로 크기
// y = 세로 크기

public void SetLocation(int xPos, int yPos);
public void SetLocation(Vector2Int pos);
// 창의 위치를 바꿔주는 함수

public void SetWindowSize(int x, int y);
public void SetWindowSize(Vector2Int size);
// 창의 크기를 바꿔주는 함수

public void ResetPositionVar();
// 위에 있는 변수들을 다시 설정합니다
// 창의 크기가 바뀌었을때 호출해줘야 해요.

```
