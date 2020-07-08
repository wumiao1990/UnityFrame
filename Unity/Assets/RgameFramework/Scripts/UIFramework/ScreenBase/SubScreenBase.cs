public class SubScreenBase
{
    protected UISubCtrlBase mCtrlBase;

    public UISubCtrlBase CtrlBase { get { return mCtrlBase; } }

    public SubScreenBase(UISubCtrlBase ctrlBase)
    {
        mCtrlBase = ctrlBase;
        Init();
    }

    virtual protected void Init()
    {

    }

    virtual public void Dispose()
    {

    }
}
