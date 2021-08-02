using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinTween
{

    namespace Scale
    {
        public class WindowScale : WindowCore
        {
            static private WindowScale       inst = null; // static ���� �뵵
                   private WaitForEndOfFrame wait = new WaitForEndOfFrame();
    
            protected override void Awake()
            {
                base.Awake();
                inst = this;
            }
    
            #region Window Size effects
    
            /// <summary>
            /// â�� ���� Ű���� ��üȭ������ ����ϴ�.
            /// </summary>
            /// <param name="duration">â Ŀ���� �Ⱓ</param>
            /// <param name="snap">���ϸ��̼� ���� �̵� ����</param>
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

                float degree = 0.0f; // �ﰢ�Լ� ��
                float add = Mathf.PI / duration / 2.0f; // �ð� ���� ������ ����
    
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
            /// ��üȭ���� ������ �� â�� ���� ���Դϴ�.
            /// </summary>
            /// <param name="targetScale">��ǥ ������</param>
            /// <param name="duration">â �پ��� �Ⱓ</param>
            /// <param name="snap">���ϸ��̼� ���� �̵� ����</param>
            /// <param name="callBack"></param>
            static public void Windowed(Vector2Int targetScale, float duration, bool snap = false, WindowCallBack callBack = null)
            {
                inst.StartCoroutine(inst.ToWindowed(targetScale.x, targetScale.y, duration, snap, callBack));
            }
            /// <summary>
            /// ��üȭ���� ������ �� â�� ���� ���Դϴ�.
            /// </summary>
            /// <param name="targetX">��ǥ X ������</param>
            /// <param name="targetY">���� Y ������</param>
            /// <param name="duration">â �پ��� �Ⱓ</param>
            /// <param name="snap">���ϸ��̼� ���� �̵� ����</param>
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
    
                float degree = 0.0f; // �ﰢ�Լ� ��
                float add = Mathf.PI / duration / 2.0f; // �ð� ���� ������ ����
    
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