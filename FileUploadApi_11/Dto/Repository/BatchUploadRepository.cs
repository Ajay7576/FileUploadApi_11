//using FileUploadApi_11.Data;
//using FileUploadApi_11.Models;
//using Microsoft.EntityFrameworkCore;

//namespace FileUploadApi_11.Dto.Repository
//{
//    public class BatchUploadRepository : IBatchUploadRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public BatchUploadRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public bool CreateBatchUploadFile(BatchUploadDetail batchUploadDetail)
//        {
//            _context.BatchUploadDetails.Add(batchUploadDetail);
//            return Save();
//        }

//        public ICollection<BatchUploadDetail> GetBatchUploadsDetails()
//        {
//            return _context.BatchUploadDetails.ToList();
//        }

//        public bool Save()
//        {
//            return _context.SaveChanges() == 1 ? true : false;
//        }
//    }
//}
