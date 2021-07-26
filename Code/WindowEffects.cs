using System.Collections;
using UnityEngine;

using WinTween.Position;
using WinTween.Scale;

public class WindowEffects : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<PositionEffects>();
        gameObject.AddComponent<ScaleEffects>();
    }
}

namespace WinTween
{
    /*
    Pre-maded effects for your convenience

    Have fun :3
    */

    namespace Position
    { 
        public class PositionEffects : WindowCore
        {
            static private PositionEffects   inst = null; // static 접근 용도 
                   private WaitForEndOfFrame wait = new WaitForEndOfFrame();

            protected override void Awake()
            {
                base.Awake();

                inst = this;
            }



            #region Bounce Effects

            /// <summary>
            /// 창이 위로 바운스되는 효과를 재생합니다.
            /// </summary>
            /// <param name="duration">바운스 기간</param>
            /// <param name="amount">바운스 될 거리</param>
            /// <param name="callback"></param>
            /// <returns></returns>
            static public void BounceUp(float duration, float amount, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.UpBounce(duration, amount, callback));
            }
            private IEnumerator UpBounce(float duration, float amount, WindowCallBack callback = null)
            {
                Vector2Int pos = GetLocation(); // 본인 창 위치를 가져옴
                float degree   = 0.0f; // 삼각함수 용
                float add      = Mathf.PI / duration; // 시간 동안 움직임 위함

                while (degree < Mathf.PI)
                {
                    degree += add * Time.deltaTime;
                    SetLocation(pos.x, pos.y - (int)(Mathf.Sin(degree) * amount)); // TODO : 위치
                    yield return wait;
                }

                callback?.Invoke();
            }

            /// <summary>
            /// 창이 아레로 바운스되는 효과를 재생합니다.
            /// </summary>
            /// <param name="duration">바운스 기간</param>
            /// <param name="amount">바운스 될 거리</param>
            /// <param name="callback"></param>
            /// <returns></returns>
            static public void BounceDown(float duration, float amount, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.DownBounce(duration, amount, callback));
            }
            private IEnumerator DownBounce(float duration, float amount, WindowCallBack callback = null)
            {
                Vector2Int pos = GetLocation();
                float degree   = 0.0f; // 삼각함수 용
                float add      = Mathf.PI / duration; // 시간 동안 움직임 위함

                while (degree < Mathf.PI)
                {
                    degree += add * Time.deltaTime;
                    SetLocation(pos.x, pos.y + (int)(Mathf.Sin(degree) * amount));
                    yield return wait;
                }

                callback?.Invoke();
            }

            /// <summary>
            /// 창이 오른쪽으로 바운스되는 효과를 재생합니다.
            /// </summary>
            /// <param name="duration">바운스 기간</param>
            /// <param name="amount">바운스 될 거리</param>
            /// <param name="callback"></param>
            /// <returns></returns>
            static public void BounceRight(float duration, float amount, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.RightBounce(duration, amount, callback));
            }
            private IEnumerator RightBounce(float duration, float amount, WindowCallBack callback = null)
            {
                Vector2Int pos = GetLocation();
                float degree   = 0.0f; // 삼각함수 용
                float add      = Mathf.PI / duration; // 시간 동안 움직임 위함

                while (degree < Mathf.PI)
                {
                    degree += add * Time.deltaTime;
                    SetLocation(pos.x + (int)(Mathf.Sin(degree) * amount), pos.y);
                    yield return wait;
                }

                callback?.Invoke();
            }

            /// <summary>
            /// 창이 왼쪽으로 바운스되는 효과를 재생합니다.
            /// </summary>
            /// <param name="duration">바운스 기간</param>
            /// <param name="amount">바운스 될 거리</param>
            /// <param name="callback"></param>
            /// <returns></returns>
            static public void BounceLeft(float duration, float amount, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.LeftBounce(duration, amount, callback));
            }
            private IEnumerator LeftBounce(float duration, float amount, WindowCallBack callback = null)
            {
                Vector2Int pos = GetLocation();
                float degree   = 0.0f; // 삼각함수 용
                float add      = Mathf.PI / duration; // 시간 동안 움직임 위함
                
                while (degree < Mathf.PI)
                {
                    degree += add * Time.deltaTime;
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
            /// <param name="duration">흔들 기간</param>
            /// <param name="amount">흔들 거리</param>
            /// <param name="count">흔들 횟수</param>
            /// <param name="callBack"></param>
            static public void ShakeX(float duration, float amount, int count, WindowCallBack callBack = null)
            {
                if (count == 0) return;
                inst.StartCoroutine(inst.XShake(duration, amount, count, callBack));
            }
            private IEnumerator XShake(float duration, float amount, int count, WindowCallBack callBack = null)
            {
                count *= 2;

                Vector2Int pos = GetLocation();

                float degree = 0.0f; // 삼각함수 용
                float add = Mathf.PI / duration; // 시간 동안 움직임 위함

                for (int i = 0; i < count; ++i)
                {
                    degree = 0.0f;

                    while (degree < Mathf.PI)
                    {
                        degree += add;

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
            /// <param name="duration">흔들 기간</param>
            /// <param name="amount">흔들 거리</param>
            /// <param name="count">흔들 횟수</param>
            /// <param name="callBack"></param>
            static public void ShakeY(float duration, float amount, int count, WindowCallBack callBack = null)
            {
                if (count == 0) return;
                inst.StartCoroutine(inst.YShake(duration, amount, count, callBack));
            }
            private IEnumerator YShake(float duration, float amount, int count, WindowCallBack callBack = null)
            {
                count *= 2;

                Vector2Int pos = GetLocation();

                float degree = 0.0f; // 삼각함수 용
                float add = Mathf.PI / duration; // 시간 동안 움직임 위함

                for (int i = 0; i < count; ++i)
                {
                    degree = 0.0f;

                    while (degree < Mathf.PI)
                    {
                        degree += add;

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
        }
    }

    namespace Scale
    {
        public class ScaleEffects : WindowCore
        {
            static private ScaleEffects      inst = null; // static 접근 용도
                   private WaitForEndOfFrame wait = new WaitForEndOfFrame();

            #region Window Size effects

            /// <summary>
            /// 창을 점점 키워서 전체화면으로 만듭니다.
            /// </summary>
            /// <param name="duration">창 커지는 기간</param>
            /// <param name="snap">에니메이션 없이 이동 여부</param>
            /// <param name="callBack"></param>
            static public void ToFullScreen(float duration, bool snap = false, WindowCallBack callBack = null)
            {
                inst.StartCoroutine(inst.FullScreen(duration, snap, callBack));
            }
            private IEnumerator FullScreen(float duration, bool snap = false, WindowCallBack callBack = null)
            {
                Vector2Int targetScale = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
                Vector2Int curScale = GetCurrentSize();
                Vector2Int beginScale = curScale;

                float degree = 0.0f; // 삼각함수 용
                float add = Mathf.PI / duration; // 시간 동안 움직임 위함

                if (snap)
                {
                    Screen.SetResolution(targetScale.x, targetScale.y, true);
                    ResetPositionVar();
                    callBack?.Invoke();
                    yield break;
                }

                while (degree <= Mathf.PI / 2.0f)
                {
                    degree += add;
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
            /// <param name="duration">창 줄어드는 기간</param>
            /// <param name="snap">에니메이션 없이 이동 여부</param>
            /// <param name="callBack"></param>
            public void ToWindowed(Vector2Int targetScale, float duration, bool snap = false, WindowCallBack callBack = null)
            {
                StartCoroutine(Windowed(targetScale.x, targetScale.y, duration, snap, callBack));
            }
            /// <summary>
            /// 전체화면을 해제한 후 창을 점점 줄입니다.
            /// </summary>
            /// <param name="targetX">목표 X 사이즈</param>
            /// <param name="targetY">목포 Y 사이즈</param>
            /// <param name="duration">창 줄어드는 기간</param>
            /// <param name="snap">에니메이션 없이 이동 여부</param>
            /// <param name="callBack"></param>
            public void ToWindowed(int targetX, int targetY, float duration, bool snap = false, WindowCallBack callBack = null)
            {
                StartCoroutine(Windowed(targetX, targetY, duration, snap, callBack));
            }
            #endregion

            private IEnumerator Windowed(int targetX, int targetY, float duration, bool snap = false, WindowCallBack callBack = null)
            {
                Vector2Int curScale = GetCurrentSize();
                Resolution curRes = Screen.currentResolution;

                float degree = 0.0f; // 삼각함수 용
                float add = Mathf.PI / duration; // 시간 동안 움직임 위함

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
                    degree += add;
                    curScale.x = targetX;
                    curScale.y = targetY;

                    float xTemp = Mathf.Cos(degree) * (curRes.width - targetX);
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
            /// <param name="duration">이동 기간</param>
            /// <param name="snap">에니에이션 없이 이동 여부</param>
            /// <returns></returns>
            public IEnumerator MoveWindow(Vector2Int pos, float duration, bool snap = false)
            {
                if (snap)
                {
                    SetLocation(pos);
                    yield break;
                }

                float degree = 0.0f; // 삼각함수 용
                float add = Mathf.PI / duration; // 시간 동안 움직임 위함

                while (degree < Mathf.PI / 2.0f)
                {
                    degree += add;
                    SetLocation(pos.x, pos.y); // TODO : snap 이랑 다른게 없다
                    yield return wait;
                }
            }

            // TODO : Dotween 처럼 뒤에 SetEase 붙이고 싶음
        }
    }
}