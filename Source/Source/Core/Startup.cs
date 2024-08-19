using Verse;

namespace Foundation
{
    [StaticConstructorOnStartup]
    public class Foundation_Startup

    {
        public static string version = "v0.0.1";

        static Foundation_Startup()
        {
            Log.Message("Level 5 security credentials accepted. Welcome back to the Foundation, [REDACTED]\nSecure Contain Protect [" + Foundation_Startup.version + "] loaded.");
        }
    }
}