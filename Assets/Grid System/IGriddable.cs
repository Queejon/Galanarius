using GridSystem;

public interface IGriddable
{
    public int x
    {
        get;
        set;
    }
    public int y
    {
        get;
        set;
    }

    // Is called for each grid object
    public void Display(int x, int y);

    // Is called after an entire column of grid objects have been looped through
    public void LateDisplay(int x);

    // Is called after all grid objects have been looped through
    public void FinalDisplay();
}
