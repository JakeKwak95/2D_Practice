using System;

public static class EventsBroadcaster
{
    public static Action OnFail;
    public static Action OnSuccess;
    public static Action OnRetry;
} 
