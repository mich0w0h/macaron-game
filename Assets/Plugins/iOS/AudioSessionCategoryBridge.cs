using System.Runtime.InteropServices;
using UnityEngine;

namespace iOSNative
{
    // iOSで、サイレントモードの時や、 アプリがバックグラウンドにある時に、音を再生し続けられるようにする。
    //
    // バックグラウンド再生を有効にするには、プレーヤー設定画面にて、
    // Enable Custom Background Behaviors > Audio にもチェックを入れること。
    // 注意: ただしチェックを入れると、OnApplicationPause() は呼ばれなくなる。
    public static class AudioSessionCategoryBridge
    {
        [DllImport("__Internal")]
        static extern void __setAudioSessionCategoryPlayback();

        [System.Diagnostics.Conditional("UNITY_IOS")]
        public static void SetAudioSessionCategoryPlayback()
        {
            __setAudioSessionCategoryPlayback();
        }

        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            SetAudioSessionCategoryPlayback();
        }
    }
}