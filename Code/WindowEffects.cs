using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowEffects : WindowCore
{
    private WaitForEndOfFrame wait = new WaitForEndOfFrame();


    /*
    Pre-maded effects for your convenience

    Have fun :3
    */


    // TODO : Coroutine 으로 호출해야 한다는 매우 귀찮은 단점이 존재함

    /// <summary>
    /// 창을 화면 가온데로 이동시킵니다.
    /// </summary>
    /// <param name="speed">이동 속도</param>
    /// <param name="callBack"></param>
    /// <param name="snap">에니에이션 없이 이동 여부</param>
    /// <returns></returns>
    public void Middle(float speed, bool snap = false, WindowCallBack callBack = null)
    {
        StartCoroutine(ToMiddle(speed, snap, callBack));
    }
    private IEnumerator ToMiddle(float speed, bool snap = false, WindowCallBack callBack = null)
    {
        // Midpos 잘못잡힘
        if (snap)
        {
            SetLocation(MiddleCenter);
            yield break;
        }

        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI / 2.0f)
        {
            degree += speed; // TODO : 끝내지 않음

            //SetLocation();
        }


        callBack?.Invoke();
    }

    #region Bounce Effects

    /// <summary>
    /// 창이 위로 바운스되는 효과를 재생합니다.
    /// </summary>
    /// <param name="speed">바운스 속도</param>
    /// <param name="amount">바운스 될 거리</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public void BounceUp(float speed, float amount, WindowCallBack callback = null)
    {
        StartCoroutine(UpBounce(speed, amount, callback));
    }
    private IEnumerator UpBounce(float speed, float amount, WindowCallBack callback = null)
    {
        Vector2Int pos = GetLocation();

        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI)
        {
            degree += speed;
            SetLocation(pos.x, pos.y - (int)(Mathf.Sin(degree) * amount)); // TODO : 위치
            yield return wait;
        }

        callback?.Invoke();
    }

    /// <summary>
    /// 창이 아레로 바운스되는 효과를 재생합니다.
    /// </summary>
    /// <param name="speed">바운스 속도</param>
    /// <param name="amount">바운스 될 거리</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public void BounceDown(float speed, float amount, WindowCallBack callback = null)
    {
        StartCoroutine(DownBounce(speed, amount, callback));
    }
    private IEnumerator DownBounce(float speed, float amount, WindowCallBack callback = null)
    {
        Vector2Int pos = GetLocation();

        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI)
        {
            degree += speed;
            SetLocation(pos.x, pos.y + (int)(Mathf.Sin(degree) * amount));
            yield return wait;
        }

        callback?.Invoke();
    }

    /// <summary>
    /// 창이 오른쪽으로 바운스되는 효과를 재생합니다.
    /// </summary>
    /// <param name="speed">바운스 속도</param>
    /// <param name="amount">바운스 될 거리</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public void BounceRight(float speed, float amount, WindowCallBack callback = null)
    {
        StartCoroutine(RightBounce(speed, amount, callback));
    }
    private IEnumerator RightBounce(float speed, float amount, WindowCallBack callback = null)
    {
        Vector2Int pos = GetLocation();

        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI)
        {
            degree += speed;
            SetLocation(pos.x + (int)(Mathf.Sin(degree) * amount), pos.y);
            yield return wait;
        }

        callback?.Invoke();
    }

    /// <summary>
    /// 창이 왼쪽으로 바운스되는 효과를 재생합니다.
    /// </summary>
    /// <param name="speed">바운스 속도</param>
    /// <param name="amount">바운스 될 거리</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public void BounceLeft(float speed, float amount, WindowCallBack callback = null)
    {
        StartCoroutine(LeftBounce(speed, amount, callback));
    }
    private IEnumerator LeftBounce(float speed, float amount, WindowCallBack callback = null)
    {
        Vector2Int pos = GetLocation();

        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI)
        {
            degree += speed;
            SetLocation(pos.x - (int)(Mathf.Sin(degree) * amount), pos.y);
            yield return wait;
        }

        callback?.Invoke();
    }

    #endregion // Bounce Effects

    #region Shake Effects

    /// <summary>
    /// 창을 좌우로 흔듭니다.
    /// </summary>
    /// <param name="speed">흔들 속도</param>
    /// <param name="amount">흔들 거리</param>
    /// <param name="count">흔들 횟수</param>
    /// <param name="callBack"></param>
    public void ShakeX(float speed, float amount, int count, WindowCallBack callBack = null)
    {
        if (count == 0) return;
        StartCoroutine(XShake(speed, amount, count, callBack));
    }
    private IEnumerator XShake(float speed, float amount, int count, WindowCallBack callBack = null)
    {
        count *= 2;

        Vector2Int pos = GetLocation();

        float degree = 0.0f;
        speed /= 100.0f;

        for (int i = 0; i < count; ++i)
        {
            degree = 0.0f;

            while (degree < Mathf.PI)
            {
                degree += speed;

                if (i % 2 == 0)
                {
                    SetLocation(pos.x + (int)(Mathf.Sin(degree) * amount), pos.y);
                }
                else
                {
                    SetLocation(pos.x - (int)(Mathf.Sin(degree) * amount), pos.y);
                }
                yield return wait;
            }
        }

        Middle(0, true);
    }

    /// <summary>
    /// 창을 상하로 흔듭니다.
    /// </summary>
    /// <param name="speed">흔들 속도</param>
    /// <param name="amount">흔들 거리</param>
    /// <param name="count">흔들 횟수</param>
    /// <param name="callBack"></param>
    public void ShakeY(float speed, float amount, int count, WindowCallBack callBack = null)
    {
        if (count == 0) return;
        StartCoroutine(YShake(speed, amount, count, callBack));
    }
    private IEnumerator YShake(float speed, float amount, int count, WindowCallBack callBack = null)
    {
        count *= 2;

        Vector2Int pos = GetLocation();

        float degree = 0.0f;
        speed /= 100.0f;

        for (int i = 0; i < count; ++i)
        {
            degree = 0.0f;

            while (degree < Mathf.PI)
            {
                degree += speed;

                if (i % 2 == 0)
                {
                    SetLocation(pos.x, pos.y + (int)(Mathf.Sin(degree) * amount));
                }
                else
                {
                    SetLocation(pos.x, pos.y - (int)(Mathf.Sin(degree) * amount));
                }
                yield return wait;
            }
        }

        Middle(0, true);
    }


    #endregion // Shake Effects


    #region Window Size effects

    /// <summary>
    /// 창을 점점 키워서 전체화면으로 만듭니다.
    /// </summary>
    /// <param name="speed">창 커지는 속도</param>
    /// <param name="snap">에니메이션 없이 이동 여부</param>
    /// <param name="callBack"></param>
    public void ToFullScreen(float speed, bool snap = false, WindowCallBack callBack = null)
    {
        StartCoroutine(FullScreen(speed, snap, callBack));
    }
    private IEnumerator FullScreen(float speed, bool snap = false, WindowCallBack callBack = null)
    {
        Vector2Int targetScale = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
        Vector2Int curScale    = GetCurrentSize();
        Vector2Int beginScale  = curScale;
        float degree           = 0.0f;
              speed           /= 100.0f;

        if (snap)
        {
            Screen.SetResolution(targetScale.x, targetScale.y, true);
            ResetPositionVar();
            callBack?.Invoke();
            yield break;
        }

        while (degree <= Mathf.PI / 2.0f) 
        {
            degree    += speed;
            curScale.x = beginScale.x;
            curScale.y = beginScale.y;

            float xTemp = Mathf.Sin(degree) * (targetScale.x - beginScale.x);
            float yTemp = Mathf.Sin(degree) * (targetScale.y - beginScale.y);

            curScale.x += (int)xTemp;
            curScale.y += (int)yTemp;

            SetWindowSize(curScale);
            ResetPositionVar();
            Middle(0, true);
            yield return wait;
        }

        Screen.SetResolution(targetScale.x, targetScale.y, true);
        callBack?.Invoke();
    }

    #region Windowed Caller Function
    /// <summary>
    /// 전체화면을 해제한 후 창을 점점 줄입니다.
    /// </summary>
    /// <param name="targetScale">목표 사이즈</param>
    /// <param name="speed">창 줄어드는 속도</param>
    /// <param name="snap">에니메이션 없이 이동 여부</param>
    /// <param name="callBack"></param>
    public void ToWindowed(Vector2Int targetScale, float speed, bool snap = false, WindowCallBack callBack = null)
    {
        StartCoroutine(Windowed(targetScale.x, targetScale.y, speed, snap, callBack));
    }
    /// <summary>
    /// 전체화면을 해제한 후 창을 점점 줄입니다.
    /// </summary>
    /// <param name="targetX">목표 X 사이즈</param>
    /// <param name="targetY">목포 Y 사이즈</param>
    /// <param name="speed">창 줄어드는 속도</param>
    /// <param name="snap">에니메이션 없이 이동 여부</param>
    /// <param name="callBack"></param>
    public void ToWindowed(int targetX, int targetY, float speed, bool snap = false, WindowCallBack callBack = null)
    {
        StartCoroutine(Windowed(targetX, targetY, speed, snap, callBack));
    }
    #endregion

    private IEnumerator Windowed(int targetX, int targetY, float speed, bool snap = false, WindowCallBack callBack = null)
    {
        Vector2Int curScale = GetCurrentSize();
        Resolution curRes   = Screen.currentResolution;
        float      degree   = 0.0f;
                   speed   /= 100.0f;

        Screen.SetResolution(curScale.x, curScale.y, false);
        if (snap)
        {
            SetWindowSize(targetX, targetY);
            ResetPositionVar();
            Middle(0, true);
            callBack?.Invoke();
            yield break;
        }


        while (degree <= Mathf.PI / 2.0f)
        {
            degree += speed;
            curScale.x = targetX;
            curScale.y = targetY;

            float xTemp = Mathf.Cos(degree) * (curRes.width  - targetX);
            float yTemp = Mathf.Cos(degree) * (curRes.height - targetY);

            curScale.x += (int)xTemp;
            curScale.y += (int)yTemp;

            SetWindowSize(curScale);
            ResetPositionVar();
            Middle(0, true);
            yield return wait;
        }


        SetWindowSize(targetX, targetY);
        ResetPositionVar();
        Middle(0, true);
        callBack?.Invoke();
    }

    #endregion // Window Size effects



    /// <summary>
    /// 특정한 위치로 창을 움직입니다.
    /// </summary>
    /// <param name="pos">이동시킬 위치</param>
    /// <param name="speed">이동 속도</param>
    /// <param name="snap">에니에이션 없이 이동 여부</param>
    /// <returns></returns>
    public IEnumerator MoveWindow(Vector2Int pos, float speed, bool snap = false)
    {
        if (snap)
        {
            SetLocation(pos);
            yield break;
        }

        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI / 2.0f)
        {
            degree += speed;
            SetLocation(pos.x, pos.y); // TODO : snap 이랑 다른게 없다
            yield return wait;
        }
    }

    // TODO : Dotween 처럼 뒤에 SetEase 붙이고 싶음
}
