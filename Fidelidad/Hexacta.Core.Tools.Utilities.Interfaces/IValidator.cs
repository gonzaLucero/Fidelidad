namespace Hexacta.Core.Tools.Utilities.Interfaces
{
    public interface IValidator
    {
        bool Validate<T>(T validable);
    }
}
