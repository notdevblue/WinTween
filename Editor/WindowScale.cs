using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinTween
{

    namespace Scale
    {
        public class WindowScale : WindowCore
        {
            static private WindowScale       inst = null; // static 접근 용도
                   private WaitForEndOfFrame wait = new WaitForEndOfFrame();
    
            protected override void Awake()
            {
                base.Awake();
                inst = this;
            }
    
            #region Window Size effects
    
            /// <summary>
            /// 창을 점점 키워서 전체화면으로 만듭니다.
            /// </summary>
            /// <param name="duration">창 커지는 기간</param>
            /// <param name="snap">에니메이션 없이 이동 여부</param>
            /// <param name="callBack"></param>
            static public void FullScreen(float duration, bool snap = false, WindowCallBack callBack = null)
            {
                inst.StartCoroutine(inst.ToFullScreen(duration, snap, callBack));
            }

            private IEnumerator ToFullScreen(float duration, bool snap = false, WindowCallBack callBack = null)
            {
                Vector2Int targetScale = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
                Vector2Int curScale    = GetCurrentSize();
                Vector2Int vect        = targetScale - curScale;

                float degree = 0.0f; // 삼각함수 용
                float add = Mathf.PI / duration / 2.0f; // 시간 동안 움직임 위함
    
                if (snap)
                {
                    Screen.SetResolution(targetScale.x, targetScale.y, true);
                    ResetPositionVar();
                    callBack?.Invoke();
                    yield break;
                }
    
                while (degree <= Mathf.PI / 2.0f)
                {
                    degree += add * Time.deltaTime;

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
            static public void Windowed(Vector2Int targetScale, float duration, bool snap = false, WindowCallBack callBack = null)
            {
                inst.StartCoroutine(inst.ToWindowed(targetScale.x, targetScale.y, duration, snap, callBack));
            }
            /// <summary>
            /// 전체화면을 해제한 후 창을 점점 줄입니다.
            /// </summary>
            /// <param name="targetX">목표 X 사이즈</param>
            /// <param name="targetY">목포 Y 사이즈</param>
            /// <param name="duration">창 줄어드는 기간</param>
            /// <param name="snap">에니메이션 없이 이동 여부</param>
            /// <param name="callBack"></param>
            static public void Windowed(int targetX, int targetY, float duration, bool snap = false, WindowCallBack callBack = null)
            {
                inst.StartCoroutine(inst.ToWindowed(targetX, targetY, duration, snap, callBack));
            }
            #endregion
    
            private IEnumerator ToWindowed(int targetX, int targetY, float duration, bool snap = false, WindowCallBack callBack = null)
            {
                Vector2Int curScale = GetCurrentSize();
                Resolution curRes = Screen.currentResolution;
    
                float degree = 0.0f; // 삼각함수 용
                float add = Mathf.PI / duration / 2.0f; // 시간 동안 움직임 위함
    
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
                    degree += add * Time.deltaTime;
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
        }
    }
}