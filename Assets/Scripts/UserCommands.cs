public static class UserCommands
{
    public static void StartPlaying() => SessionManager.I.IsPlaying = true;

    public static void ResetSession()
    {
        SessionManager.I.IsPlaying = false;
        SessionManager.I.Reset();
    }
}