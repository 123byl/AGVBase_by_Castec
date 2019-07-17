using System.Speech.Synthesis;

namespace NetExtention
{
    /// <summary>
    /// 字串控衝功能應用
    /// </summary>
    public static class ExString
    {
        /// <summary>
        /// 語音合成器
        /// </summary>
        private static readonly SpeechSynthesizer mSynth = new SpeechSynthesizer();

        /// <summary>
        /// 使用合成方式發出聲音
        /// </summary>
        public static void SpeekEnglish(this string str, int volume = 80, int speed = 1)
        {
            mSynth.Volume = volume;
            mSynth.Rate = speed;
            mSynth.Speak(str);
        }
    }
}
