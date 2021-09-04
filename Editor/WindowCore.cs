using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace WinTween
{
    abstract public class WindowCore : MonoBehaviour
    {
#if UNITY_STANDALONE_WIN || !UNITY_EDITOR_LINUX
        #region WinAPI import
    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    private static extern IntPtr FindWindow(string className, string windowName);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(HandleRef hwnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
#endregion
    
        // Sets current application's window position
        // do not work when fullscreen
        #region SetLocation Function
    /// <summary>
    /// Changes Location of application
    /// </summary>
    /// <param name="xPos">x position of target location</param>
    /// <param name="yPos">y position of target location</param>
    /// <param name="xScale">not using</param>
    /// <param name="yScale">not using</param>
    public void SetLocation(int xPos, int yPos, int xScale = 1280, int yScale = 720)
    {
        SetWindowPos(activeHwnd, 0, xPos, yPos, xScale, yScale, 1);
    }
    /// <summary>
    /// Changes Location of application
    /// </summary>
    /// <param name="pos">Position of target location</param>
    /// <param name="xScale">not using</param>
    /// <param name="yScale">not using</param>
    public void SetLocation(Vector2Int pos, int xScale = 1280, int yScale = 720)
    {
        SetWindowPos(activeHwnd, 0, pos.x, pos.y, xScale, yScale, 1);
    }
    /// <summary>
    /// ## Do not use ## Changes Location of application
    /// </summary>
    public void SetLocation(Vector2Int pos, Vector2Int screen)
    {
        SetWindowPos(activeHwnd, 0, pos.x, pos.y, screen.x, screen.y, 1);
    }
        #endregion
    
        // Sets current application's window size
        #region SetWindowSize Function
    /// <summary>
    /// Changes size of application
    /// </summary>
    /// <param name="x">x size of application</param>
    /// <param name="y">y size of application</param>
    public void SetWindowSize(int x, int y)
    {
        Vector2Int curLot = GetLocation();
        MoveWindow(activeHwnd, curLot.x, curLot.y, x, y, true);
    }
    /// <summary>
    /// Changes size of application
    /// </summary>
    /// <param name="size">size of application</param>
    public void SetWindowSize(Vector2Int size)
    {
        Vector2Int location = GetLocation();
        MoveWindow(activeHwnd, location.x, location.y, size.x, size.y, true);
    }
    /// <summary>
    /// Chagnes size of application
    /// </summary>
    /// <param name="x">x size of application</param>
    /// <param name="y">y size of application</param>
    /// <param name="xPos">x location of application</param>
    /// <param name="yPos">y location of application</param>
    public void SetWindowSize(int x, int y, int xPos, int yPos)
    {
        MoveWindow(activeHwnd, xPos, yPos, x, y, true);
    }
    /// <summary>
    /// Changes size of application
    /// </summary>
    /// <param name="size">size of application</param>
    /// <param name="location">location of application</param>
    public void SetWindowSize(Vector2Int size, Vector2Int location)
    {
        MoveWindow(activeHwnd, location.x, location.y, size.x, size.y, true);
    }
        #endregion
    
        
    
        ///<summary>
        /// Returns currnet application's window postiion
        ///</summary>
        public Vector2Int GetLocation()
        {
            RECT rect;
            GetWindowRect(new HandleRef(this, this.activeHwnd), out rect);
    
            return new Vector2Int(rect.Left, rect.Top);
        }
    
        /// <summary>
        /// Returns current application's window size
        /// </summary>
        /// <returns>current window size</returns>
        public Vector2Int GetCurrentSize()
        {
            RECT rect;
            GetWindowRect(new HandleRef(this, this.activeHwnd), out rect);
    
            return new Vector2Int(rect.Right - rect.Left, rect.Bottom - rect.Top);
        }
    
        /// <summary>
        /// Resets screenpoint vars
        /// </summary>
        public void ResetPositionVar()
        {
            RECT rect;
            GetWindowRect(new HandleRef(this, this.activeHwnd), out rect);
    
            ResetPoints();
        }
    
        #region var
    // Has current window's handle
    // be carefull when working in unity editor.
    private IntPtr activeHwnd;

    // Han current window's ilocation.
    /// <summary>
    /// Left: x pos of upper-left corner.<br></br>
    /// Top: y pos of upper-left corner.<br></br>
    /// Right: x pos of lower-right coner.<br></br>
    /// Bottom: y pos of lower-right coner.<br></br>
    /// </summary>
    private RECT rc;
        #endregion
    
        // callback delegate
        public delegate void WindowCallBack();

        // static functions
        static private WindowCore inst; 
    
        #region Position var
    /// <summary>
    /// Left x position of your monitor
    /// </summary>
    public int LeftPosX   { get; private set; }
    /// <summary>
    /// Monitor x resolution - Application x resolution
    /// </summary>
    public int RightPosX  { get; private set; }

    /// <summary>
    /// Monitor x resolution / 2 - Application x resolution / 2
    /// </summary>
    public int MidPosX    { get; private set; }

    /// <summary>
    /// Left y position of your monitor
    /// </summary>
    public int TopPosY    { get; private set; }
    /// <summary>
    /// Monitor y resolution / 2 - Application y resolution / 2
    /// </summary>
    public int MidPosY    { get; private set; }

    /// <summary>
    /// Monitor y resolution - Application y resolution
    /// </summary>
    public int BottomPosY { get; private set; }
        #endregion
    
        #region Screen size var
    // screen size var
    private int sizeX;
    private int sizeY;

    // screen size vector
    /// <summary>
    /// Resolution of your game application.<br></br>
    /// # This is not monitor resolution #
    /// </summary>
    public Vector2Int ScreenSize   { get; private set; }

    // position vector
    /// <summary>
    /// Top center position of your monitor
    /// </summary>
    public Vector2Int TopCenter    { get; private set; }

    /// <summary>
    /// Top left position of your monitor
    /// </summary>
    public Vector2Int TopLeft      { get; private set; }

    /// <summary>
    /// Top right position of your monitior
    /// </summary>
    public Vector2Int TopRight     { get; private set; }

    /// <summary>
    /// Middle center position of your monitior
    /// </summary>
    public Vector2Int MiddleCenter { get; private set; }

    /// <summary>
    /// Middle left position of your monitior
    /// </summary>
    public Vector2Int MiddleLeft   { get; private set; }

    /// <summary>
    /// Middle right position of your monitior
    /// </summary>
    public Vector2Int MiddleRight  { get; private set; }

    /// <summary>
    /// Bottom center position of your monitior
    /// </summary>
    public Vector2Int BottomCenter { get; private set; }

    /// <summary>
    /// Bottom left position of your monitior
    /// </summary>
    public Vector2Int BottomLeft   { get; private set; }

    /// <summary>
    /// Bottom right position of your monitior
    /// </summary>
    public Vector2Int BottomRight  { get; private set; }
        #endregion

        #region ToMiddle

        /// <summary>
        /// â�� ȭ�� ���µ��� �̵���ŵ�ϴ�.
        /// </summary>
        /// <param name="speed">�̵� �ӵ�</param>
        /// <param name="callBack"></param>
        /// <param name="snap">���Ͽ��̼� ���� �̵� ����</param>
        /// <returns></returns>
        static public void Middle(float speed, bool snap = false, WindowCallBack callBack = null)
        {
            inst.StartCoroutine(inst.ToMiddle(speed, snap, callBack));
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

        #endregion

        protected virtual void Awake()
        {
        #region ## DO NOT EDIT ##
        // init core var
        activeHwnd = GetActiveWindow();
        ResetPoints();

        // set default res
        Screen.SetResolution(1920, 1080, false);
        SetWindowSize(1280, 720);

        #endregion

            inst = this;
        }
    
        private void ResetPoints()
        {
            // Gets current window's size
            RECT rect;
            GetWindowRect(new HandleRef(this, this.activeHwnd), out rect);
            rc = rect;
    
            // init size var
            sizeX = rect.Right  - rect.Left;
            sizeY = rect.Bottom - rect.Top;
    
            // init size vector
            ScreenSize = new Vector2Int(sizeX, sizeY);
    
            // init pos var
            LeftPosX  = -8; // 0 doesn't moves window to absolute coner
            MidPosX   = Screen.currentResolution.width / 2 - sizeX / 2;
            RightPosX = Screen.currentResolution.width     - sizeX;
    
            TopPosY    = -8; // 0 doesn't moves window to absolute coner
            MidPosY    = Screen.currentResolution.height / 2 - sizeY / 2;
            BottomPosY = Screen.currentResolution.height     - sizeY;
    
    
            // init pos vector
            TopCenter = new Vector2Int(MidPosX, TopPosY);
            TopLeft   = new Vector2Int(LeftPosX, TopPosY);
            TopRight  = new Vector2Int(RightPosX, TopPosY);
    
            MiddleCenter = new Vector2Int(MidPosX, MidPosY);
            MiddleLeft   = new Vector2Int(LeftPosX, MidPosY);
            MiddleRight  = new Vector2Int(RightPosX, MidPosY);
    
            BottomCenter = new Vector2Int(MidPosX, BottomPosY);
            BottomLeft   = new Vector2Int(LeftPosX, BottomPosY);
            BottomRight  = new Vector2Int(RightPosX, BottomPosY);
        }

        #endif
    }
    
}


