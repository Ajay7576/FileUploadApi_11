using FileUploadApi_11.Models;

namespace FileUploadApi_11.Dto.Repository
{
    public interface IBatchUploadRepository
    {
        ICollection<BatchUploadDetail> GetBatchUploadsDetails();
        bool CreateBatchUploadFile(BatchUploadDetail batchUploadDetail);

        bool Save();
    }
}
