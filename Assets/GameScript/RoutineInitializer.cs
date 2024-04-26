// GENERATED CODE - DO NOT EDIT BY HAND


namespace GameScript
{
    public static class RoutineInitializer
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize()
        {
            RoutineDirectory.Directory = new System.Action<RunnerContext>[2];
            RoutineDirectory.Directory[0] = (RunnerContext ctx) =>
            {
            };
            RoutineDirectory.Directory[1] = (RunnerContext ctx) =>
            {
                ctx.SetConditionResult(true);
            };
        }
    }
}
