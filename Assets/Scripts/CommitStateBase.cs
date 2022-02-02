using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitState : CommitStateBase
{
    float enterStateTime;

    public SubmitState()
    {
        enterStateTime = Time.time;
    }

    public override ActionResult TrySubmit()
    {
        if (Time.time > enterStateTime + 3)
            return new ActionResult(new PushState());
        else
            return new ActionResult("Commit failed, still fetching incomming changes...");
    }

    public override ActionResult TryPush() { return new ActionResult("No commits to push... Try commiting changes first."); }
    public override ActionResult TryPull() { return new ActionResult("No changes detected..."); }
}

public class PushState : CommitStateBase
{
    public override ActionResult TryPush()
    {
        return new ActionResult(new PullState());
    }
    public override ActionResult TrySubmit() { return new ActionResult(this); }
    public override ActionResult TryPull() { return new ActionResult("No changes detected..."); }
}

public class PullState : CommitStateBase
{
    public override ActionResult TryPull()
    {
        return new ActionResult(new SubmitState());
    }

    public override ActionResult TrySubmit() { return new ActionResult("Origin has changes, please pull first."); }
    public override ActionResult TryPush() { return new ActionResult("Origin has changes, please pull first."); }
}

public class CommitStateBase
{
    public virtual ActionResult TrySubmit() { return new ActionResult("Commit Error"); }
    public virtual ActionResult TryPush() { return new ActionResult("Push Error"); }
    public virtual ActionResult TryPull() { return new ActionResult("Pull Error"); }
}

public class ActionResult
{
    public enum Types
    {
        Success,
        Error
    }

    public Types Type;
    public string ErrorMessage;
    public CommitStateBase SuccessState;
    private PullState pullState;

    public ActionResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Type = Types.Error;
    }

    public ActionResult(CommitStateBase newState)
    {
        SuccessState = newState;
        Type = Types.Success;
    }
}
