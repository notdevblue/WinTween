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


    // TODO : Coroutine ���� ȣ���ؾ� �Ѵٴ� �ſ� ������ ������ ������

    /// <summary>
    /// â�� ȭ�� ���µ��� �̵���ŵ�ϴ�.
    /// </summary>
    /// <param name="speed">�̵� �ӵ�</param>
    /// <param name="callBack"></param>
    /// <param name="snap">���Ͽ��̼� ���� �̵� ����</param>
    /// <returns></returns>
    public void Middle(float speed, bool snap = false, WindowCallBack callBack = null)
    {
        StartCoroutine(ToMiddle(speed, snap, callBack));
    }
    private IEnumerator ToMiddle(float speed, bool snap = false, WindowCallBack callBack = null)
    {
        // Midpos �߸�����
        if (snap)
        {
            SetLocation(MiddleCenter);
            yield break;
        }

        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI / 2.0f)
        {
            degree += speed; // TODO : ������ ����

            //SetLocation();
        }


        callBack?.Invoke();
    }

    #region Bounce Effects

    /// <summary>
    /// â�� ���� �ٿ�Ǵ� ȿ���� ����մϴ�.
    /// </summary>
    /// <param name="speed">�ٿ �ӵ�</param>
    /// <param name="amount">�ٿ �� �Ÿ�</param>
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
            SetLocation(pos.x, pos.y - (int)(Mathf.Sin(degree) * amount)); // TODO : ��ġ
            yield return wait;
        }

        callback?.Invoke();
    }

    /// <summary>
    /// â�� �Ʒ��� �ٿ�Ǵ� ȿ���� ����մϴ�.
    /// </summary>
    /// <param name="speed">�ٿ �ӵ�</param>
    /// <param name="amount">�ٿ �� �Ÿ�</param>
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
    /// â�� ���������� �ٿ�Ǵ� ȿ���� ����մϴ�.
    /// </summary>
    /// <param name="speed">�ٿ �ӵ�</param>
    /// <param name="amount">�ٿ �� �Ÿ�</param>
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
    /// â�� �������� �ٿ�Ǵ� ȿ���� ����մϴ�.
    /// </summary>
    /// <param name="speed">�ٿ �ӵ�</param>
    /// <param name="amount">�ٿ �� �Ÿ�</param>
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
    /// â�� �¿�� ���ϴ�.
    /// </summary>
    /// <param name="speed">��� �ӵ�</param>
    /// <param name="amount">��� �Ÿ�</param>
    /// <param name="count">��� Ƚ��</param>
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
    /// â�� ���Ϸ� ���ϴ�.
    /// </summary>
    /// <param name="speed">��� �ӵ�</param>
    /// <param name="amount">��� �Ÿ�</param>
    /// <param name="count">��� Ƚ��</param>
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
    /// â�� ���� Ű���� ��üȭ������ ����ϴ�.
    /// </summary>
    /// <param name="speed">â Ŀ���� �ӵ�</param>
    /// <param name="snap">���ϸ��̼� ���� �̵� ����</param>
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
    /// ��üȭ���� ������ �� â�� ���� ���Դϴ�.
    /// </summary>
    /// <param name="targetScale">��ǥ ������</param>
    /// <param name="speed">â �پ��� �ӵ�</param>
    /// <param name="snap">���ϸ��̼� ���� �̵� ����</param>
    /// <param name="callBack"></param>
    public void ToWindowed(Vector2Int targetScale, float speed, bool snap = false, WindowCallBack callBack = null)
    {
        StartCoroutine(Windowed(targetScale.x, targetScale.y, speed, snap, callBack));
    }
    /// <summary>
    /// ��üȭ���� ������ �� â�� ���� ���Դϴ�.
    /// </summary>
    /// <param name="targetX">��ǥ X ������</param>
    /// <param name="targetY">���� Y ������</param>
    /// <param name="speed">â �پ��� �ӵ�</param>
    /// <param name="snap">���ϸ��̼� ���� �̵� ����</param>
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
    /// Ư���� ��ġ�� â�� �����Դϴ�.
    /// </summary>
    /// <param name="pos">�̵���ų ��ġ</param>
    /// <param name="speed">�̵� �ӵ�</param>
    /// <param name="snap">���Ͽ��̼� ���� �̵� ����</param>
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
            SetLocation(pos.x, pos.y); // TODO : snap �̶� �ٸ��� ����
            yield return wait;
        }
    }

    // TODO : Dotween ó�� �ڿ� SetEase ���̰� ����
}
