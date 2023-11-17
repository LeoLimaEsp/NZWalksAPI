using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
