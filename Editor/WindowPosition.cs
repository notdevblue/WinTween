using System.Collections;
using UnityEngine;

/// <summary>
/// WinTween.<br></br>
/// For easy window movements
/// </summary>
namespace WinTween
{
    namespace Position
    { 
        /// <summary>
        /// Can control application's window position
        /// </summary>
        public class Position : WindowCore
        {
            static private Position          inst = null; // for static functions
                   private WaitForEndOfFrame wait = new WaitForEndOfFrame();

            protected override void Awake()
            {
                base.Awake();
                inst = this;
            }

            /// <summary>
            /// Current application's window location
            /// </summary>
            static public Vector2Int Location
            {
                get
                {
                    return inst.GetLocation();
                }
            }

            /// <summary>
            /// 특정한 위치로 창을 움직입니다.<br></br>
            /// Moves window to specific location
            /// </summary>
            /// <param name="pos">이동시킬 위치<br></br>specific location</param>
            /// <param name="duration">이동 기간<br></br>move duration</param>
            /// <param name="snap">에니에이션 없이 이동 여부<br></br>snap</param>
            /// <param name="callback">callback</param>
            /// <returns></returns>
            static public void Move(Vector2Int pos, float duration, bool snap = false, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.MoveWindow(pos, duration, snap, callback));
            }
            /// <summary>
            /// 특정한 위치로 창을 움직입니다.<br></br>
            /// Moves window to specific location
            /// </summary>
            /// <param name="x">이동시킬 x 좌표<br></br>x coord of specific location</param>
            /// <param name="y">이동시킬 y 좌표<br></br>y coord of specific location</param>
            /// <param name="duration">이동 기간<br></br>move duration</param>
            /// <param name="snap">에니에이션 없이 이동 여부<br></br>snap</param>
            /// <param name="callback">callback</param>
            /// <returns></returns>
            static public void Move(int x, int y, float duration, bool snap = false, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.MoveWindow(x, y, duration, snap, callback));
            }


            private IEnumerator MoveWindow(Vector2Int pos, float duration, bool snap = false, WindowCallBack callback = null)
            {
                if (snap)
                {
                    SetLocation(pos);
                    callback?.Invoke();
                    yield break;
                }

                Vector2Int curPos = GetLocation();
                Vector2Int lerp   = new Vector2Int(); // to save interpolated position
                Vector2    vect   = pos - curPos;     // because of Mathf.Sin(x);

                float degree = 0.0f;                    // for circular function
                float add = Mathf.PI / duration / 2.0f; // move duration

                while (degree <= Mathf.PI / 2.0f)
                {
                    degree += add * Time.deltaTime;
                    lerp = curPos + Vector2Int.CeilToInt(vect * Mathf.Sin(degree));

                    SetLocation(lerp);
                    yield return wait;
                }

                callback?.Invoke();
            }

            private IEnumerator MoveWindow(int x, int y, float duration, bool snap = false, WindowCallBack callback = null)
            {
                if (snap)
                {
                    SetLocation(x, y);
                    callback?.Invoke();
                    yield break;
                }

                Vector2Int curPos = GetLocation();
                Vector2Int lerp   = new Vector2Int();              // to save interpolated position
                Vector2    vect   = new Vector2Int(x, y) - curPos; // because of Mathf.Sin(x);

                float degree = 0.0f;                    // for circular function
                float add = Mathf.PI / duration / 2.0f; // move duration
                
                while (degree <= Mathf.PI / 2.0f)
                {
                    degree += add * Time.deltaTime;
                    lerp = curPos + Vector2Int.CeilToInt(vect * Mathf.Sin(degree));

                    SetLocation(lerp);
                    yield return wait;
                }

                callback?.Invoke();
            }
        }

        /// <summary>
        /// Pre-made effects for your convenience
        /// </summary>
        public class WindowPosition : WindowCore
        {
            static private WindowPosition   inst  = null; // for static functions
                   private WaitForEndOfFrame wait = new WaitForEndOfFrame();

            protected override void Awake()
            {
                base.Awake();

                inst = this;
            }

            #region Bounce Effects

            /// <summary>
            /// 창이 위로 바운스되는 효과를 재생합니다.<br></br>
            /// Bounces window to up-direction
            /// </summary>
            /// <param name="duration">바운스 기간<br></br>bounce duration</param>
            /// <param name="amount">바운스 될 거리<br></br>bounce amount</param>
            /// <param name="callback">callback</param>
            static public void BounceUp(float duration, float amount, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.UpBounce(duration, amount, callback));
            }
            private IEnumerator UpBounce(float duration, float amount, WindowCallBack callback = null)
            {
                Vector2Int pos = GetLocation();       // Gets window location
                float degree   = 0.0f;                // for circular function
                float add      = Mathf.PI / duration; // move duration

                while (degree <= Mathf.PI)
                {
                    degree += add * Time.deltaTime;
                    SetLocation(pos.x, pos.y - (int)(Mathf.Sin(degree) * amount));
                    yield return wait;
                }

                callback?.Invoke();
            }

            /// <summary>
            /// 창이 아레로 바운스되는 효과를 재생합니다.<br></br>
            /// Bounces window to down-direction
            /// </summary>
            /// <param name="duration">바운스 기간<br></br>bounce duration</param>
            /// <param name="amount">바운스 될 거리<br></br>bounce amount</param>
            /// <param name="callback">callback</param>
            static public void BounceDown(float duration, float amount, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.DownBounce(duration, amount, callback));
            }
            private IEnumerator DownBounce(float duration, float amount, WindowCallBack callback = null)
            {
                Vector2Int pos = GetLocation();
                float degree   = 0.0f;                // for circular functions
                float add      = Mathf.PI / duration; // move duration

                while (degree <= Mathf.PI)
                {
                    degree += add * Time.deltaTime;
                    SetLocation(pos.x, pos.y + (int)(Mathf.Sin(degree) * amount));
                    yield return wait;
                }

                callback?.Invoke();
            }

            /// <summary>
            /// 창이 오른쪽으로 바운스되는 효과를 재생합니다.<br></br>
            /// Bounces window to right-direction
            /// </summary>
            /// <param name="duration">바운스 기간<br></br>bounce duration</param>
            /// <param name="amount">바운스 될 거리<br></br>bounce amount</param>
            /// <param name="callback">callback</param>
            static public void BounceRight(float duration, float amount, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.RightBounce(duration, amount, callback));
            }
            private IEnumerator RightBounce(float duration, float amount, WindowCallBack callback = null)
            {
                Vector2Int pos = GetLocation();
                float degree   = 0.0f;                // for circular functions
                float add      = Mathf.PI / duration; // move duration

                while (degree <= Mathf.PI)
                {
                    degree += add * Time.deltaTime;
                    SetLocation(pos.x + (int)(Mathf.Sin(degree) * amount), pos.y);
                    yield return wait;
                }

                callback?.Invoke();
            }

            /// <summary>
            /// 창이 왼쪽으로 바운스되는 효과를 재생합니다.<br></br>
            /// Bounces window to left-direction
            /// </summary>
            /// <param name="duration">바운스 기간<br></br>bounce duration</param>
            /// <param name="amount">바운스 될 거리<br></br>bounce amount</param>
            /// <param name="callback">callback</param>
            static public void BounceLeft(float duration, float amount, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.LeftBounce(duration, amount, callback));
            }
            private IEnumerator LeftBounce(float duration, float amount, WindowCallBack callback = null)
            {
                Vector2Int pos = GetLocation();
                float degree   = 0.0f;                // for circular functions
                float add      = Mathf.PI / duration; // move duration
                
                while (degree <= Mathf.PI)
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
            /// 창을 좌우로 흔듭니다.<br></br>
            /// Shakes window to left and right
            /// </summary>
            /// <param name="duration">흔들 기간<br></br>shake duration</param>
            /// <param name="amount">흔들 거리<br></br>shake amount</param>
            /// <param name="count">흔들 횟수<br></br>shake count</param>
            /// <param name="callBack">callback</param>
            static public void ShakeX(float duration, float amount, int count, WindowCallBack callBack = null)
            {
                if (count == 0) return;
                inst.StartCoroutine(inst.XShake(duration, amount, count, callBack));
            }
            private IEnumerator XShake(float duration, float amount, int count, WindowCallBack callBack = null)
            {
                count *= 2;

                Vector2Int pos = GetLocation();

                float degree = 0.0f;             // for circular functions
                float add = Mathf.PI / duration; // move duration

                for (int i = 0; i < count; ++i)
                {
                    degree = 0.0f;

                    while (degree <= Mathf.PI)
                    {
                        degree += add * Time.deltaTime;

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
            /// 창을 상하로 흔듭니다.<br></br>
            /// Shakes window to up and down
            /// </summary>
            /// <param name="duration">흔들 기간<br></br>shake duration</param>
            /// <param name="amount">흔들 거리<br></br>shake amount</param>
            /// <param name="count">흔들 횟수<br></br>shake count</param>
            /// <param name="callBack">callback</param>
            static public void ShakeY(float duration, float amount, int count, WindowCallBack callBack = null)
            {
                if (count == 0) return;
                inst.StartCoroutine(inst.YShake(duration, amount, count, callBack));
            }
            private IEnumerator YShake(float duration, float amount, int count, WindowCallBack callBack = null)
            {
                count *= 2;

                Vector2Int pos = GetLocation();

                float degree = 0.0f;             // for circular functions
                float add = Mathf.PI / duration; // move duration

                for (int i = 0; i < count; ++i)
                {
                    degree = 0.0f;

                    while (degree <= Mathf.PI)
                    {
                        degree += add * Time.deltaTime;

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
}