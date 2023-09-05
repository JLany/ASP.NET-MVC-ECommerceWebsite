namespace ITIECommerce.Data.Models;

internal interface ISoftDeletable
{
    public bool IsDeleted { get; set; }

    public void Undo()
    {
        IsDeleted = false;
    }
}
