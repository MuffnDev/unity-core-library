namespace MuffinDev.Core
{

    #region Inputs

    public enum EInputAxisType
    {
        Normal,
        Raw
    }

    public enum EInputType
    {
        KeyCode,
        Button,
        MouseButton,
        Axis
    }

    public enum EMouseInput
    {
        Left = 0,
        Right = 1,
        Middle = 2
    }

    #endregion


    #region Others

    public enum ELogType
    {
        None,
        Log,
        Warning,
        Error
    }

    public enum EUpdateType
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    #endregion

}