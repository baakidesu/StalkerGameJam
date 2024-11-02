using UnityEngine;
using UnityHFSM;

public class HideState : StateBase
{
    private StalkerHFSM _stalkerHFSM;

    public HideState(StalkerHFSM context,bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
    {
        _stalkerHFSM = context;
    }
    
    
}
