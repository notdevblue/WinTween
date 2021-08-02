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
            /// Ư���� ��ġ�� â�� �����Դϴ�.<br></br>
            /// Moves window to specific location
            /// </summary>
            /// <param name="pos">�̵���ų ��ġ<br></br>specific location</param>
            /// <param name="duration">�̵� �Ⱓ<br></br>move duration</param>
            /// <param name="snap">���Ͽ��̼� ���� �̵� ����<br></br>snap</param>
            /// <param name="callback">callback</param>
            /// <returns></returns>
            static public void Move(Vector2Int pos, float duration, bool snap = false, WindowCallBack callback = null)
            {
                inst.StartCoroutine(inst.MoveWindow(pos, duration, snap, callback));
            }
            /// <summary>
            /// Ư���� ��ġ�� â�� �����Դϴ�.<br></br>
            /// Moves window to specific location
            /// </summary>
            /// <param name="x">�̵���ų x ��ǥ<br></br>x coord of specific location</param>
            /// <param name="y">�̵���ų y ��ǥ<br></br>y coord of specific location</param>
            /// <param name="duration">�̵� �Ⱓ<br></br>move duration</param>
            /// <param name="snap">���Ͽ��̼� ���� �̵� ����<br></br>snap</param>
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
            /// â�� ���� �ٿ�Ǵ� ȿ���� ����մϴ�.<br></br>
            /// Bounces window to up-direction
            /// </summary>
            /// <param name="duration">�ٿ �Ⱓ<br></br>bounce duration</param>
            /// <param name="amount">�ٿ �� �Ÿ�<br></br>bounce amount</param>
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
            /// â�� �Ʒ��� �ٿ�Ǵ� ȿ���� ����մϴ�.<br></br>
            /// Bounces window to down-direction
            /// </summary>
            /// <param name="duration">�ٿ �Ⱓ<br></br>bounce duration</param>
            /// <param name="amount">�ٿ �� �Ÿ�<br></br>bounce amount</param>
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
            /// â�� ���������� �ٿ�Ǵ� ȿ���� ����մϴ�.<br></br>
            /// Bounces window to right-direction
            /// </summary>
            /// <param name="duration">�ٿ �Ⱓ<br></br>bounce duration</param>
            /// <param name="amount">�ٿ �� �Ÿ�<br></br>bounce amount</param>
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
            /// â�� �������� �ٿ�Ǵ� ȿ���� ����մϴ�.<br></br>
            /// Bounces window to left-direction
            /// </summary>
            /// <param name="duration">�ٿ �Ⱓ<br></br>bounce duration</param>
            /// <param name="amount">�ٿ �� �Ÿ�<br></br>bounce amount</param>
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
            /// â�� �¿�� ���ϴ�.<br></br>
            /// Shakes window to left and right
            /// </summary>
            /// <param name="duration">��� �Ⱓ<br></br>shake duration</param>
            /// <param name="amount">��� �Ÿ�<br></br>shake amount</param>
            /// <param name="count">��� Ƚ��<br></br>shake count</param>
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
            /// â�� ���Ϸ� ���ϴ�.<br></br>
            /// Shakes window to up and down
            /// </summary>
            /// <param name="duration">��� �Ⱓ<br></br>shake duration</param>
            /// <param name="amount">��� �Ÿ�<br></br>shake amount</param>
            /// <param name="count">��� Ƚ��<br></br>shake count</param>
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