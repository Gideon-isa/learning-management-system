namespace Lms.Shared.Abstractions.FileStorage
{
    public interface IMimeTypeService
    {
        string GetMimeType(string filePath);
    }
}
