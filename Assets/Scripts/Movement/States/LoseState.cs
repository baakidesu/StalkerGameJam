using UnityEngine;
using UnityHFSM;

public class LoseState : StateBase
{
    private StalkerHFSM _stalkerHFSM;

    public LoseState(StalkerHFSM context,bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
    {
        _stalkerHFSM = context;
    }
}
