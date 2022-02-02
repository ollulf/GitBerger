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
            return new ActionResult("Fetching changes...");
    }
}

public class PushState : CommitStateBase
{
    public override ActionResult TryPush()
    {
        return new ActionResult(new PullState());
    }
}

public class PullState : CommitStateBase
{
    public override ActionResult TryPull()
    {
        return new ActionResult(new SubmitState());
    }
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
